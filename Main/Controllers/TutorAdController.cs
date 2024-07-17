using API.Services;
using BusinessObjects;
using BusinessObjects.Models;
using BusinessObjects.Models.TutorModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [Route("api/tutor-ad")]
    [ApiController]
    public class TutorAdController : ControllerBase
    {
        private readonly ITutorAdService _tutorAdService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ITutorService _tutorService;
        private readonly DAOs.DbContext _dbContext;

        public TutorAdController(ICurrentUserService currentUserService, ITutorAdService tutorAdService, ITutorService tutorService, DAOs.DbContext dbContext)
        {
            _tutorAdService = tutorAdService;
            _currentUserService = currentUserService;
            _tutorService = tutorService;
            _dbContext = dbContext;
        }

        [HttpGet("{id}")]
        public IActionResult GetAds(string id) 
        {

            var tbAds = _tutorAdService.GetTutorAds().Where(s => s.TutorId == id && s.IsActived == true);

            return Ok(tbAds);
        }

        [HttpPost("create_ads")]
        public IActionResult PostAds(TutorAdsModel tutorAds)
        {
            var userId = _currentUserService.GetUserId().ToString();
            var tutor = _dbContext.TutorAds.FirstOrDefault(t => t.Tutor.AccountId == userId);

            if (tutor != null)
            {
                var result = new TutorAd()
                {
                    AdsId = Guid.NewGuid().ToString(),
                    CreateDay = DateTime.Now,
                    Title = tutorAds.Title,
                    Image = tutorAds.ImageUrl,
                    Video = tutorAds.VideoUrl,
                    IsActived = null,
                    TutorId = tutor.TutorId,
                };

                _tutorAdService.CeateAd(result);

                return Ok(result);
            }
            return BadRequest();
        }
    }
}
