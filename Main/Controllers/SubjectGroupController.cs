using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectGroupController : ControllerBase
    {
        private readonly ISubjectGroupService _subjectGroupService;

        public SubjectGroupController()
        {
            _subjectGroupService = new SubjectGroupService();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _subjectGroupService.GetSubjectGroups();

            if (!result.Any())
            {
                return NotFound("No subject groups available");
            }

            return Ok(result);
        }
    }
}
