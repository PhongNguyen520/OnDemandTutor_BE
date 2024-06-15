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
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationAccountController : ControllerBase
    {
        private readonly IConversationAccountService _conversationAccountService;
        private readonly IConversationService _conversationService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;
        private readonly ICurrentUserService _currentUserService;

        public ConversationAccountController(IMapper mapper, IHubContext<ChatHub> hubContext, ICurrentUserService currentUserService, IAccountService accountService)
        {
            _conversationAccountService = new ConversationAccountService();
            _mapper = mapper;
            _hubContext = hubContext;
            _currentUserService = currentUserService;
            _accountService = accountService;
            _conversationService = new ConversationService();
        }

        [HttpGet]
        // Lấy danh sách chat box của người dùng
        public IActionResult Get()
        {
            var rawRooms =  _conversationAccountService.GetConversationAccounts().Where(x => x.AccountId == _currentUserService.GetUserId().ToString());

            var rooms = _conversationAccountService.GetConversationAccounts();

            var resultRooms = from s in rawRooms
                              join x in rooms
                              on s.ConversationId equals x.ConversationId
                              where s.AccountId != x.AccountId
                              select x;

            var users = _accountService.GetAccounts();

            var query = from r in resultRooms
                        join u in users
                        on r.AccountId equals u.Id
                        select new RoomVM
                        {
                            ConversationId = r.ConversationId,
                            AccountId = r.AccountId,
                            Name = u.FullName,
                            Avatar = u.Avatar,
                        };


            return Ok(query);
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
                return BadRequest();
            }

            // Get user logging
            

            // Chỉnh Id
            var room = new Conversation()
            {
                ConversationId = "C0005",
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

            return CreatedAtAction(nameof(Get), new { id = room.ConversationId }, new { id = room.ConversationId, date =  room.CreateDay});
        }
    }
}
