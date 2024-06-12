using Microsoft.AspNetCore.Http;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services;
using AutoMapper;
using BusinessObjects.Models;
using System.Text.RegularExpressions;
using API.Hubs;
using Microsoft.AspNet.SignalR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;

        public MessageController(IMapper mapper, IHubContext<ChatHub> hubContext)
        {
            messageService = new MessageService();
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Message>> Get(string id)
        {
            var message = messageService.GetMessages().Where(x => x.MessageId == id);
            if (message == null) 
                return NotFound();

            var messageVm = _mapper.Map<Message, MessageVM>((Message)message);

            return Ok(messageVm);
        }

        [HttpGet("Room/{roomID}")]
        public IActionResult GetMessages(string rooomID)
        {
            var room = messageService.GetMessages()
                .Where(r => r.ConversationId == rooomID)
                .OrderByDescending(m => m.Time)
                .Take(20)
                .AsEnumerable()
                .Reverse()
                .ToList();

            var messageVM = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageVM>>(room);
            return Ok(room);
        }

        [HttpPost]
        // Send message
        public async Task<ActionResult<Message>> Create (MessageVM messageVM)
        {
            var user = User.Identity.Name;
            var room = messageVM.Room;
            var msg = new Message()
            {
                Description = Regex.Replace(messageVM.Content, @"<.*?", string.Empty),
                AccountId = user,
                ConversationId = room,
                //Time = DateTime.Now
            };

            messageService.AddMessage(msg);
            
            var createdMessage = _mapper.Map<Message, MessageVM>(msg);
            //await _hubContext.Clients.Group(room.Id).SendAsync("newMessage", createdMessage);

            return CreatedAtAction(nameof(Get), new {id = msg.AccountId}, createdMessage);
        }
    }
}
