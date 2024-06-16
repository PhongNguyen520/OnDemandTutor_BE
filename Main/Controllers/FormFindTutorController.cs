using API.Services;
using BusinessObjects;
using BusinessObjects.Models.FormModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormFindTutorController : ControllerBase
    {
        private readonly IFindTutorFormService _findTutorFormService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISubjectService _subjectService;
        private readonly IStudentService _studentService;

        public FormFindTutorController(ICurrentUserService currentUserService)
        {
            _findTutorFormService = new FindTutorFormService();
            _currentUserService = currentUserService;
            _subjectService = new SubjectService();
            _studentService = new StudentService();
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        
        public IActionResult CreateForm(RequestCreateForm form)
        {
            if (form == null)
            {
                return BadRequest("Form data is required.");
            }

            var userId = _currentUserService.GetUserId().ToString();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not logged in.");
            }

            var stId = _studentService.GetStudents().FirstOrDefault(s => s.AccountId == userId);

            var subject = _subjectService.GetSubjects()
                .FirstOrDefault(s => s.SubjectGroupId == form.SubjectGroupId && s.GradeId == form.GradeId);
            if (subject == null)
            {
                return NotFound("Subject not found for the given SubjectGroupId and GradeId.");
            }

            var result = new FindTutorForm
            {
                FormId = Guid.NewGuid().ToString(),
                CreateDay = DateTime.Now,
                SubjectName = subject.Description,
                TutorGender = form.TutorGender,
                TypeOfDegree = form.TypeOfDegree,
                DescribeTutor = form.DescribeTutor,
                Status = false,
                StudentId = stId.StudentId,
                SubjectId = subject.SubjectId,
            };

            try
            {
                _findTutorFormService.AddFindTutorForm(result);
                return Ok(new { message = "Form created successfully", formId = result.FormId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the form.");
            }
        }

    }
}
