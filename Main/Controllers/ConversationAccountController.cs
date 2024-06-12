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
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;

        public ConversationAccountController(IMapper mapper, IHubContext<ChatHub> hubContext)
        {
            _conversationAccountService = new ConversationAccountService();
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [HttpGet]
        // Lấy danh sách chat box của người dùng
        public async Task<ActionResult<IEnumerable<RoomVM>>> Get(string Id)
        {
            var rooms = _conversationAccountService.GetConversationAccounts().Where(x => x.AccountId == Id);

            var roomsVM = _conversationAccountService.GetConversationAccounts();

            var query = from r in rooms
                        join rvm in roomsVM
                        on r.ConversationId equals rvm.ConversationId
                        where r.AccountId != rvm.AccountId
                        select rvm;

            return Ok(query);
        }

        //[HttpPost]
        //// Tạo chat box
        //public async Task<ActionResult<ConversationAccount>> Create(RoomVM roomVM)
        //{
        //    if (_conversationAccountService.GetConversationAccounts(roomVM.Id) != null)
        //    {
        //        return BadRequest();
        //    }

        //    // Get user logging
        //    var user = _accountService.GetAccounts().FirstOrDefault(u => u.Id == User.Identity.Name);
        //    var room = new Conversation()
        //    {
        //        ConversationId = roomVM.Id,
        //        CreateDay = DateOnly.MinValue,
        //    };

        //    var roomMember = new ConversationAccount()
        //    {
        //        ConversationId = roomVM.Id,
        //        AccountId = roomVM.Id,
        //        IsActive = true,
        //    };

        //    _conversationAccountService.AddConversationAccount(roomMember);
        //    //Send events
        //    await _hubContext.Clients.All.SendAsync("addChatRoom", new { id = room.ConversationId, date = room.CreateDay });

        //    return CreatedAtAction(nameof(Get), new { id = room.ConversationId}, new { id = room.ConversationId, date = room.CreateDay});
        //}
    }
}
