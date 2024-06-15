using API.Services;
using AutoMapper;
using BusinessObjects;
using BusinessObjects.Models;
using DAOs;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace API.Hubs
{
    public class ChatHub : Hub
    {
        public readonly static List<UserChatVM> _Connection = new List<UserChatVM>();

        //private readonly static Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();

        private readonly DbContext _dbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly ShareDBService _shareDBService;

        public ChatHub(DbContext dbContext, ICurrentUserService currentUserService, ShareDBService shareDBService)
        {
            _dbContext = dbContext;
            _currentUserService = currentUserService;
            _shareDBService = shareDBService;

        }


        public async Task JoinChat(UserConnection conn)
        {
            await Clients.All.SendAsync("ReceiveMessage", "Admin", $"{conn.UserName} has join");
        }

        public async Task JoinSpecificChatRoom(UserChatVM conn)
        {
            try
            {
                var user = _Connection.Where(u => u.Id == _currentUserService.GetUserId().ToString()).FirstOrDefault();

                await Groups.AddToGroupAsync(Context.ConnectionId, conn.Id);

                _shareDBService.connection[Context.ConnectionId] = conn;

                //Tell others to update the list of users
                await Clients.All.SendAsync("JoinSpecificChatRoom", "admin", $"{user.FullName} has join");

            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "You failed to join the chat room!" + ex.Message);
            }
        }



        // send message
        public async Task SendMessage(string message)
        {
            if (_shareDBService.connection.TryGetValue(Context.ConnectionId, out UserChatVM conn))
            {
                var sender = _Connection.Where(u => u.Id == _currentUserService.GetUserId().ToString()).First();

                if (!string.IsNullOrEmpty(message.Trim()))
                {
                    var messageViewModel = new MessageVM()
                    {
                        From = sender.FullName,
                        Content = Regex.Replace(message, @"<.*?>", string.Empty),
                        Avatar = sender.Avatar,
                        //RoomId = "",
                        //Time = DateTime.Now.ToLongTimeString(),
                    };

                    //await Clients.All.SendAsync("ReceiveSpecificMessage", messageViewModel);
                    //await Clients.Caller.SendAsync("ReceiveMessage", message);
                    // Show message in chat box
                    await Clients.Group(conn.Id).SendAsync("ReceiveSpecificMessage", messageViewModel);
                }
            }
        }

        private string IdentityName
        {
            get { return _currentUserService.GetUserId().ToString(); }
        }

        //public override Task OnConnectedAsync()
        //{
        //    try
        //    {
        //        var user = _dbContext.Users.Where(u => u.UserName == IdentityName).FirstOrDefault();
        //        var userViewModel = _mapper.Map<Account, UserChatVM>(user);
        //        userViewModel.CurrentRoom = "";

        //        if (!_Connection.Any(u => u.Id == IdentityName))
        //        {
        //            _Connection.Add(userViewModel);
        //            _ConnectionsMap.Add(IdentityName, Context.ConnectionId);
        //        }

        //        Clients.Caller.SendAsync("getProfileInfo", user.FullName, user.Avatar);
        //    }
        //    catch (Exception ex)
        //    {
        //        Clients.Caller.SendAsync("onError", "OnConnected" + ex.Message);
        //    }
        //    return base.OnConnectedAsync();
        //}

        public IEnumerable<UserChatVM> GetUsers(string roomName)
        {
            return _Connection.Where(u => u.CurrentRoom == roomName).ToList();
        }
    }
}
