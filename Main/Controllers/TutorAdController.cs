using API.Services;
using BusinessObjects;
using BusinessObjects.Models.TutorModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorAdController : ControllerBase
    {
        private readonly ITutorAdService _tutorAdService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ITutorService _tutorService;

        public TutorAdController(ICurrentUserService currentUserService)
        {
            _tutorAdService = new TutorAdService();
            _currentUserService = currentUserService;
            _tutorService = new TutorService();
        }

        [HttpGet("{id}")]
        public IActionResult GetAds(string id) 
        {

            var tbAds = _tutorAdService.GetTutorAds().Where(s => s.TutorId == id && s.IsActived == true);

            return Ok(tbAds);
        }

        [HttpPost("tutor/CreateAds")]
        public IActionResult PostAds(TutorAdsModel tutorAds)
        {
            var userId = _currentUserService.GetUserId().ToString();
            var tutor = _tutorService.GetTutors().Where(s => s.AccountId == userId).First();

            var result = new TutorAd()
            {
                AdsId = Guid.NewGuid().ToString(),
                CreateDay = DateTime.Now,
                Title = tutorAds.Title,
                Image = tutorAds.ImageUrl,
                Video = tutorAds.VideoUrl,
                IsActived = true,
                TutorId = tutor.TutorId,
            };

            _tutorAdService.AddTutorAd(result);

            return Ok(result);
        }
    }
}
