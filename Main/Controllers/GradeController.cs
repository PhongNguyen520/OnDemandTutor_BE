using BusinessObjects;
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
        private readonly ISubjectService _subjectService;

        public GradeController()
        {
            _gradeService = new GradeService();
            _subjectService = new SubjectService();
        }

        [HttpGet]
        public IActionResult Get(string? subjectGroupId)
        {
            var result = _gradeService.GetGrades();

            if (!result.Any())
            {
                return NotFound("No subject groups available");
            }

            if (!string.IsNullOrEmpty(subjectGroupId))
            {
                var subjects = _subjectService.GetSubjects().Where(s => s.SubjectGroupId == subjectGroupId);
                result = (from grade in result
                         join subject in subjects
                         on grade.GradeId equals subject.GradeId
                         select grade).ToList();
            }

            return Ok(result);
        }
    }
}