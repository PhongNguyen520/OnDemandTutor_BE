using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorAdController : ControllerBase
    {
        private readonly ITutorAdService iTutorService;


        public TutorAdController()
        {
            iTutorService = new TutorAdService();
        }

        [HttpGet("{id}")]
        public IActionResult GetAds(string id) 
        {

            var tbAds = iTutorService.GetTutorAds().Where(s => s.TutorId == id && s.IsActived == true);

            return Ok(tbAds);
        }

    }
}
