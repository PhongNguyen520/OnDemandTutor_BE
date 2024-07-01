using API.Services;
using BusinessObjects;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly ICurrentUserService _currentUserService;

        public NotificationController(ICurrentUserService currentUserService)
        {
            _notificationService = new NotificationService();
            _currentUserService = currentUserService;
        }

        [HttpGet("viewNotification")]
        public IActionResult Get()
        {
            var user = _currentUserService.GetUserId().ToString();
            var resultList = _notificationService.GetNotifications()
                                                 .Where(s => s.AccountId == user && s.IsActive == true)
                                                 .OrderByDescending(s => s.CreateDay);
            return Ok(resultList);
        }

        [HttpPost("createNotification")]
        public IActionResult CreateNotification(NotificationVM request)
        {
            var result = new Notification()
            {
                NotificationId = Guid.NewGuid().ToString(),
                CreateDay = DateTime.Now,
                Title = request.Title,
                Description = request.Description,
                Url = request.Url,
                IsActive = true,
                AccountId = request.AccountId,
            };
            _notificationService.AddNotification(result);

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
