using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradeController()
        {
            _gradeService = new GradeService();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _gradeService.GetGrades();

            if (!result.Any())
            {
                return NotFound("No subject groups available");
            }

            return Ok(result);

        }
    }
}