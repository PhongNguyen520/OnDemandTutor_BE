using AutoMapper;
using BusinessObjects;
using BusinessObjects.Models;
using DAOs;
using Microsoft.AspNetCore.SignalR;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace API.Hubs
{
    public class ChatHub : Hub
    {
        public readonly static List<UserChatVM> _Connection = new List<UserChatVM>();

        private readonly static Dictionary<string, string> _ConnectionsMap = new Dictionary<string, string>();

        private readonly DbContext _dbContext;
        private readonly IMapper _mapper;

        private ChatHub(DbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task SendPrivate(string receiverName, string message)
        {
            if (_ConnectionsMap.TryGetValue(receiverName, out string userId))
            {
                var sender = _Connection.Where(u => u.Id == IdentityName).First();

                if (!string.IsNullOrEmpty(message.Trim()))
                {
                    var messageViewModel = new MessageVM()
                    {
                        Content = Regex.Replace(message, @"<.*?>", string.Empty),
                        From = sender.FullName,
                        Avatar = sender.Avatar,
                        Room = "",
                        Time = DateTime.Now.ToLongTimeString(),
                    };

                    await Clients.Client(userId).SendAsync("newMessage", messageViewModel);
                    await Clients.Caller.SendAsync("newMessage", messageViewModel);
                }
            }
        }

        public async Task Join(string roomName)
        {
            try
            {
                var user = _Connection.Where(u => u.Id == IdentityName).FirstOrDefault();

                //Remove user from others list
                if (user != null && user.CurrentRoom != roomName)
                {
                    if (!string.IsNullOrEmpty(user.CurrentRoom))

                        await Clients.OthersInGroup(user.CurrentRoom).SendAsync("removeUser", user);

                    //Join to new chat room
                    await Leave(user.CurrentRoom);
                    await Groups.AddToGroupAsync(Context.ConnectionId, roomName);
                    user.CurrentRoom = roomName;

                    //Tell others to update the list of users
                    await Clients.OthersInGroup(roomName).SendAsync("addUser", user);


                }
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "You failed to join the chat room!" + ex.Message);
            }
        }

        public async Task Leave(string roomName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomName);
        }

        private string IdentityName
        {
            get { return Context.User.Identity.Name; }
        }

        public override Task OnConnectedAsync()
        {
            try
            {
                var user = _dbContext.Users.Where(u => u.UserName == IdentityName).FirstOrDefault();
                var userViewModel = _mapper.Map<Account, UserChatVM>(user);
                userViewModel.Device = GetDevice();
                userViewModel.CurrentRoom = "";

                if (!_Connection.Any(u => u.Id == IdentityName))
                {
                    _Connection.Add(userViewModel);
                    _ConnectionsMap.Add(IdentityName, Context.ConnectionId);
                }

                Clients.Caller.SendAsync("getProfileInfo", user.FullName, user.Avatar);
            }
            catch (Exception ex)
            {
                Clients.Caller.SendAsync("onError", "OnConnected" + ex.Message);
            }
            return base.OnConnectedAsync();
        }

        public IEnumerable<UserChatVM> GetUsers(string roomName)
        {
            return _Connection.Where(u => u.CurrentRoom == roomName).ToList();
        }

        private string GetDevice()
        {
            var device = Context.GetHttpContext().Request.Headers["Device"].ToString();
            if (!string.IsNullOrEmpty(device) && (device.Equals("Desktop") || device.Equals("Mobile")))
                return device;
            return "Web";
        }
    }
}
