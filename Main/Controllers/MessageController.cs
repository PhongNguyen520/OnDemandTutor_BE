using Microsoft.AspNetCore.Http;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services;
using AutoMapper;
using BusinessObjects.Models;
using System.Text.RegularExpressions;
using API.Hubs;
using Microsoft.AspNetCore.SignalR;
using API.Services;
using DAOs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly IAccountService _accountService;
        private readonly DbContext _dbContext;

        public MessageController(IHubContext<ChatHub> hubContext, IMapper mapper, ICurrentUserService currentUserService, IAccountService accountService, DbContext dbContext)
        {
            messageService = new MessageService();
            _mapper = mapper;
            _hubContext = hubContext;
            _currentUserService = currentUserService;
            _accountService = accountService;
            _dbContext = dbContext;
        }



        [HttpGet("{roomID}")]
        public async Task<ActionResult<MessageVM>> GetMessages(string roomID)
        {
            var listMessages = messageService.GetMessages()
                .Where(r => r.ConversationId == roomID)
                .Take(20)
                .AsEnumerable()
                .Reverse()
                .ToList();

            if (listMessages.Any())
            {
                foreach(var message in listMessages)
                {
                    if (message.IsRead == false)
                    {
                        message.IsRead = true;
                        messageService.UpdateMessages(message);
                    }
                }
            }

            //var messageVM = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageVM>>(listMessages);

            var listAccount = _accountService.GetAccounts();

            var query = from account in listAccount
                        join message in listMessages
                        on account.Id equals message.AccountId
                        orderby message.Time ascending
                        select new MessageVM
                        {
                            MessageId = message.MessageId,
                            FullName = account.FullName,
                            Content = message.Description,
                            Time = message.Time.ToString("dd/MM/yyyy HH:mm:ss"),
                            UserId = message.AccountId,
                            RoomId = message.ConversationId,
                            Avatar = account.Avatar,
                        };

            return Ok(query);
        }

        [HttpPost]
        // Send message
        public async Task<ActionResult> SendMessage(CreateMessage message)
        {
            var user = _currentUserService.GetUserId().ToString();
            var room = message.RoomId;
            var msg = new Message()
            {
                MessageId = Guid.NewGuid().ToString(),
                Description = message.Content,
                AccountId = user,
                ConversationId = room,
                Time = DateTime.Now,
                IsActive = true,
                IsRead = false,
            };

            _dbContext.Add(msg);
            await _dbContext.SaveChangesAsync();

            var showMessage = new MessageVM()
            {
                MessageId = msg.MessageId,
                FullName = _accountService.GetAccounts().Where(s => s.Id == msg.AccountId).Select(s => s.FullName).FirstOrDefault(),
                Content = msg.Description,
                Time = msg.Time.ToString("dd/MM/yyyy HH:mm:ss"),
                UserId = msg.AccountId,
                RoomId = msg.ConversationId,
                Avatar = _accountService.GetAccounts().Where(s => s.Id == msg.AccountId).Select(s => s.Avatar).FirstOrDefault(),
            };
            //var createdMessage = _mapper.Map<Message, MessageVM>(msg);
            await _hubContext.Clients.Group(message.RoomId).SendAsync("ReceiveSpecificMessage", message.RoomId, showMessage);

            return Ok(msg);
        }
    }
}
