using API.Hubs;
using API.Services;
using AutoMapper;
using BusinessObjects;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using Services;

namespace API.Controllers
{
    [Route("api/conversation-account")]
    [ApiController]
    public class ConversationAccountController : ControllerBase
    {
        private readonly IConversationAccountService _conversationAccountService;
        private readonly IConversationService _conversationService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMessageService _messageService;

        public ConversationAccountController(IMapper mapper, IHubContext<ChatHub> hubContext, ICurrentUserService currentUserService, IAccountService accountService)
        {
            _conversationAccountService = new ConversationAccountService();
            _mapper = mapper;
            _hubContext = hubContext;
            _currentUserService = currentUserService;
            _accountService = accountService;
            _conversationService = new ConversationService();
            _messageService = new MessageService();
        }

        [HttpGet]
        // Lấy danh sách chat box của người dùng
        public async Task<ActionResult<IEnumerable<RoomVM>>> Get()
        {
            var rawRooms = _conversationAccountService.GetConversationAccounts().Where(x => x.AccountId == _currentUserService.GetUserId().ToString());

            var rooms = _conversationAccountService.GetConversationAccounts();

            // Lấy ConversationAccount của người khác trong hộp chat 
            var resultRooms = from s in rawRooms
                              join x in rooms
                              on s.ConversationId equals x.ConversationId
                              where s.AccountId != x.AccountId
                              select x;

            var users = _accountService.GetAccounts();

            // Lấy thông tin cần show của danh sách chat
            var query = from r in resultRooms
                        join u in users
                        on r.AccountId equals u.Id
                        select new RoomVM
                        {
                            ConversationId = r.ConversationId,
                            AccountId = r.AccountId,
                            Name = u.FullName,
                            Avatar = u.Avatar,
                            LastMessage = _currentUserService.GetUserId().ToString() == _messageService.GetMessages()
                                                                                        .Where(s => s.ConversationId == r.ConversationId)
                                                                                        .Select(s => s.Account.Id).LastOrDefault()
                                          ? $"You: {_messageService.GetMessages()
                                                    .Where(s => s.ConversationId == r.ConversationId)
                                                    .Select(s => s.Description).LastOrDefault()}"
                                          : $"{_messageService.GetMessages()
                                                .Where(s => s.ConversationId == r.ConversationId)
                                                .Select(s => s.Account.FullName).LastOrDefault()} : {_messageService.GetMessages()
                                                                                                    .Where(s => s.ConversationId == r.ConversationId)
                                                                                                    .Select(s => s.Description).LastOrDefault()}",
                        };


            return Ok(query.OrderBy(s => s.LastMessage));
        }

        [HttpPost]
        // Tạo chat box
        public async Task<IActionResult> CreateAsync(string userId)
        {
            var currentUser = _conversationAccountService.GetConversationAccounts().Where(x => x.AccountId == _currentUserService.GetUserId().ToString());

            var check = _conversationAccountService.GetConversationAccounts().Where(x => x.AccountId == userId);

            var list = from r in currentUser
                       join u in check
                       on r.ConversationId equals u.ConversationId
                       select r;

            if (!list.IsNullOrEmpty())
            {
                return Ok();
            }

            // Get user logging


            // Chỉnh Id
            var room = new Conversation()
            {
                ConversationId = Guid.NewGuid().ToString(),
                CreateDay = DateTime.Now,
            };

            var roomMember1 = new ConversationAccount()
            {
                ConversationId = room.ConversationId,
                AccountId = _currentUserService.GetUserId().ToString(),
                IsActive = true,
            };
            var roomMember2 = new ConversationAccount()
            {
                ConversationId = room.ConversationId,
                AccountId = userId,
                IsActive = true,
            };

            _conversationService.AddConversation(room);
            _conversationAccountService.AddConversationAccount(roomMember1);
            _conversationAccountService.AddConversationAccount(roomMember2);
            //Send events
            //await _hubContext.Clients.All.SendAsync("JoinSpecific");

            return CreatedAtAction(nameof(Get), new { id = room.ConversationId }, new { id = room.ConversationId, date = room.CreateDay });
        }
    }
}
