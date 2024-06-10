using Microsoft.AspNetCore.Http;
using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Services;
using AutoMapper;
using BusinessObjects.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService messageService;
        private readonly IMapper _mapper;

        public MessageController(IMapper mapper)
        {
            messageService = new MessageService();
            _mapper = mapper;
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
    }
}
