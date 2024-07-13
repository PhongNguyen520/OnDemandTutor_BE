using API.Hubs;
using API.Services;
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
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IHubContext<ChatHub> _hubContext;

        public NotificationController(ICurrentUserService currentUserService, IHubContext<ChatHub> hubContext)
        {
            _notificationService = new NotificationService();
            _currentUserService = currentUserService;
            _hubContext = hubContext;
        }

        [HttpGet("viewNotificationList")]
        public IActionResult Get()
        {
            var user = _currentUserService.GetUserId().ToString();
            var resultList = _notificationService.GetNotifications()
                                                 .Where(s => s.AccountId == user && s.IsActive == true);
            var result = from item in resultList
                         select new NotiListVM()
                         {
                             NotificationId = item.NotificationId,
                             FullName = item.FullName,
                             Avatar = item.Avatar,
                             Title = item.Title,
                             CreateDay = item.CreateDay.ToString("yyyy-MM-dd HH:mm"),
                         };

            return Ok(result);
        }

        [HttpGet("viewNotificationDetail")]
        public IActionResult GetDetail(string id)
        {
            var noti = _notificationService.GetNotifications()
                                                 .Where(s => s.NotificationId == id).First();
            var result = new NotificationVM()
                         {
                             Url = noti.Url,
                             FullName = noti.FullName,
                             Avatar = noti.Avatar,
                             Title = noti.Title,
                             Description = noti.Description,
                         };

            return Ok(result);
        }

        [HttpPost("createNotification")]
        public async Task<ActionResult> CreateNotification(CreateNotiVM request)
        {
            var user = _currentUserService.GetUser();

            var result = new Notification()
            {
                NotificationId = Guid.NewGuid().ToString(),
                CreateDay = DateTime.Now,
                Avatar = user.Result.Avatar,
                FullName = user.Result.FullName,
                Title = request.Title,
                Description = request.Description,
                Url = request.Url,
                IsActive = true,
                AccountId = request.AccountId,
            };
            _notificationService.AddNotification(result);
            await _hubContext.Clients.Group(request.AccountId).SendAsync("ReceiveNotification", request.AccountId, "New notification");
            return Ok(result);
        }

        [HttpPut("deleteNotification")]
        public IActionResult DeleteNotification(string id)
        {
            var result = _notificationService.GetNotifications().Where(s => s.NotificationId == id).FirstOrDefault();
            result.IsActive = false;
            _notificationService.UpdateNotifications(result);

            return Ok(result);
        }
    }
}
