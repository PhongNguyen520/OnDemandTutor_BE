using API.Hubs;
using AutoMapper;
using BusinessObjects;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConversationAccountController : ControllerBase
    {
        private readonly IConversationAccountService _conversationAccountService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;

        public ConversationAccountController(AccountService accountService, IMapper mapper, IHubContext<ChatHub> hubContext)
        {
            _conversationAccountService = new ConversationAccountService();
            _accountService = accountService;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomVM>>> Get(string Id)
        {
            var rooms = _conversationAccountService.GetConversationAccounts(Id).ToList();

            var roomsVM = _mapper.Map<IEnumerable<ConversationAccount>, IEnumerable<RoomVM>>(rooms);
            return Ok(roomsVM);
        }

        [HttpPost]
        public async Task<ActionResult<ConversationAccount>> Create(RoomVM roomVM)
        {
            if (_conversationAccountService.GetConversationAccounts(roomVM.Id) != null)
            {
                return BadRequest();
            }

            // Get user logging
            var user = _accountService.GetAccounts().Where(u => u.Id == User.Identity.Name).FirstOrDefault();
            var room = new Conversation()
            {
                ConversationId = roomVM.Id,
                CreateDay = DateOnly.MinValue,
            };

            var roomMember = new ConversationAccount()
            {
                ConversationId = roomVM.Id,
                AccountId = roomVM.Id,
                IsActive = true,
            };

            _conversationAccountService.AddConversationAccount(roomMember);
            //Send events
            await _hubContext.Clients.All.SendAsync("addChatRoom", new { id = room.ConversationId, name = room.CreateDay });

            return CreatedAtAction(nameof(Get), new { id = room.ConversationId, roomMember = roomMember }, new { id = room.ConversationId, name = room.CreateDay});
        }
    }
}
