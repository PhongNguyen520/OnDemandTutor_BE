using BusinessObjects;
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
        private readonly ISubjectService _subjectService;

        public SubjectGroupController()
        {
            _subjectGroupService = new SubjectGroupService();
            _subjectService = new SubjectService();
        }

        [HttpGet]
        public IActionResult Get(string? gradeId)
        {
            var result = _subjectGroupService.GetSubjectGroups();

            if (!result.Any())
            {
                return NotFound("No subject groups available");
            }

            if (!string.IsNullOrEmpty(gradeId))
            {
                var subjects = _subjectService.GetSubjects().Where(s => s.GradeId == gradeId);
                result = (from subjectGroup in result
                          join subject in subjects
                          on subjectGroup.SubjectGroupId equals subject.SubjectGroupId
                          select subjectGroup).ToList();
            }

            return Ok(result);
        }

        [HttpGet("suggest")]
        public IActionResult SuggestSubject(string? search)
        {
            List<string> result = new List<string>();
            if (search == null)
            {
                return Ok(result);
            }
            result = _subjectGroupService.GetSubjectGroups().Where(s => s.SubjectName.ToLower().Contains(search.ToLower())).Select(s => s.SubjectName).ToList();
            return Ok(result);
        }
    }
}
