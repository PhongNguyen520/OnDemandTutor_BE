using API.Services;
using BusinessObjects;
using BusinessObjects.Models.FormModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly IAccountService _accountService;

        public FormFindTutorController(ICurrentUserService currentUserService, IAccountService accountService)
        {
            _findTutorFormService = new FindTutorFormService();
            _currentUserService = currentUserService;
            _subjectService = new SubjectService();
            _studentService = new StudentService();
            _accountService = accountService;
        }

        [HttpGet("tutor/searchpost")]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("student/form/")]
        // Show Student's post list
        public IActionResult GetList()
        {
            // Lấy userId từ _currentUserService
            var userId = _currentUserService.GetUserId().ToString();

            // Tìm thông tin người dùng từ bảng tài khoản
            var user = _accountService.GetAccounts().FirstOrDefault(s => s.Id == userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Tìm thông tin học sinh từ bảng học sinh bằng AccountId
            var student = _studentService.GetStudents().FirstOrDefault(s => s.AccountId == userId);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            // Lấy danh sách các form tìm gia sư của học sinh
            var forms = _findTutorFormService.GetFindTutorForms().Where(s => s.StudentId == student.StudentId);

            // Tạo danh sách các FormVM để trả về
            var query = from post in forms
                        select new FormVM
                        {
                            FormId = post.FormId,
                            CreateDay = post.CreateDay,
                            FullName = user.FullName,
                            Tittle = post.Tittle,
                            MinHourlyRate = post.MinHourlyRate,
                            MaxHourlyRate = post.MaxHourlyRate,
                            SubjectName = post.SubjectName,
                            Description = post.DescribeTutor,
                            TutorGender = post.TutorGender,
                            SubjectId = post.SubjectId,
                            StudentId = student.StudentId,
                        };

            return Ok(query);
        }

        [HttpPost("student/createform/")]
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
                MaxHourlyRate = form.MaxHourlyRate,
                MinHourlyRate = form.MinHourlyRate,
                TutorGender = form.TutorGender,
                TypeOfDegree = form.TypeOfDegree,
                Tittle = form.Tittle,
                DescribeTutor = form.DescribeTutor,
                Status = false,
                StudentId = stId.StudentId,
                SubjectId = subject.SubjectId,
                IsActive = false,
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
