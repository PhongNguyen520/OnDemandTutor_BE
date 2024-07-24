using API.Services;
using AutoMapper;
using BusinessObjects;
using BusinessObjects.Models;
using DAOs;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Protocol;
using Services;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace API.Hubs
{
    public class ChatHub : Hub
    {
        public readonly static List<UserConnection> _Connection = new List<UserConnection>();
        private readonly ShareDBService _shareDBService;
        private readonly IMessageService _messageService;

        public ChatHub(DbContext dbContext, ICurrentUserService currentUserService, ShareDBService shareDBService)
        {
            _shareDBService = shareDBService;
            _messageService = new MessageService();
        }


        public async Task JoinChat(UserConnection conn)
        {
            await Clients.All.SendAsync("ReceiveMessage", "Admin", $"{conn.UserName} has join");
        }

        public async Task JoinSpecificChatRoom(UserConnection conn)
        {
            try
            {

                await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);

                await Clients.Group(conn.ChatRoom).SendAsync("JoinSpecificChatRoom", "admin", $"{conn.UserName} has joined");
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "You failed to join the chat room!" + ex.Message);
            }
        }



        // send message
        public async Task SendMessage(string messageId)
        {
            if (_shareDBService.connection.TryGetValue(Context.ConnectionId, out UserConnection conn))
            {
                var message = _messageService.GetMessages().Where(s => s.MessageId == messageId);
                await Clients.Group(conn.ChatRoom).SendAsync("ReceiveSpecificMessage", conn.UserName, message);
            }
        }

        public async Task JoinSpecificNotification(UserConnection conn)
        {
            try
            {

                await Groups.AddToGroupAsync(Context.ConnectionId, conn.ChatRoom);

                await Clients.Group(conn.ChatRoom).SendAsync("JoinSpecificNotification", "admin", $"{conn.UserName} has joined");
            }
            catch (Exception ex)
            {
                await Clients.Caller.SendAsync("onError", "You failed to join the chat room!" + ex.Message);
            }
        }

        public async Task SendNotification(string userId)
        {
            if (_shareDBService.connection.TryGetValue(Context.ConnectionId, out UserConnection conn))
            {
                await Clients.Group(userId).SendAsync("ReceiveNotification", conn.UserName, "You have new notification");
            }
        }
    }
}
