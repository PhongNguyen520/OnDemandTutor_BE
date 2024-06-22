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
        public async Task<ActionResult<UserChatVM>> GetMessages(string roomID)
        {
            var listMessages = messageService.GetMessages()
                .Where(r => r.ConversationId == roomID)
                .OrderByDescending(m => m.Time)
                .Take(20)
                .AsEnumerable()
                .Reverse()
                .ToList();

            //var messageVM = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageVM>>(room);

            var listAccount = _accountService.GetAccounts();

            var query = from account in listAccount
                        join message in listMessages
                        on account.Id equals message.AccountId
                        select new UserChatVM
                        {
                            FullName = account.FullName,
                            Content = message.Description,
                            Time = DateTime.Now,
                            UserId = message.AccountId,
                            RoomId = message.ConversationId,
                            Avatar = account.Avatar,
                        };

            return Ok(query);
        }

        [HttpPost]
        // Send message
        public async Task<ActionResult> SendMessage(string roomId, string content)
        {
            var user = _currentUserService.GetUserId().ToString();
            var room = roomId;
            var msg = new Message()
            {
                MessageId = "C0012",
                Description = content,
                AccountId = user,
                ConversationId = room,
                Time = DateOnly.MaxValue,
                IsActive = true,
            };

            _dbContext.Add(msg);
            await _dbContext.SaveChangesAsync();
            
            //var createdMessage = _mapper.Map<Message, MessageVM>(msg);
            await _hubContext.Clients.All.SendAsync("ReceiveSpecificMessage", content);

            return Ok();
        }
    }
}
