using API.Services;
using BusinessObjects;
using BusinessObjects.Models.RequestFormModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormRequestTutorController : ControllerBase
    {
        private readonly IRequestTutorFormService _formService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISubjectService _subjectService;
        private readonly IStudentService _studentService;

        public FormRequestTutorController(ICurrentUserService currentUserService)
        {
            _formService = new RequestTutorFormService();
            _currentUserService = currentUserService;
            _subjectService = new SubjectService();
            _studentService = new StudentService();
        }

        // Create Form Request Tutor + Handle To Avoid Conflict With Tutor Calender
        [HttpPost]
        public IActionResult CreateRequest(RequestCreateForm form)
        {
            var userId = _currentUserService.GetUserId().ToString();

            var subject = _subjectService.GetSubjects()
                .Where(s => s.SubjectGroupId == form.SubjectGroupId && s.GradeId == form.GradeId)
                .FirstOrDefault();

            var student = _studentService.GetStudents()
                                        .Where(s => s.AccountId == userId)
                                        .FirstOrDefault();

            var newRequestForm = new RequestTutorForm()
            {
                FormId = Guid.NewGuid().ToString(),
                CreateDay = DateTime.Now,
                DayStart = form.DayStart,
                DayEnd = form.DayEnd,
                DayOfWeek = form.DayOfWeek,
                TimeStart = form.TimeStart,
                TimeEnd = form.TimeEnd,
                Description = form.Description,
                Status = null,
                IsActive = null,
                SubjectId = subject.SubjectId,
                TutorId = form.TutorId,
                StudentId = student.StudentId,
            };

            _formService.AddRequestTutorForm(newRequestForm);

            return Ok(newRequestForm.FormId);
        }
    }
}
