using API.Services;
using BusinessObjects.Models.RequestFormModel;
using BusinessObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Humanizer;
using Repositories;
using Microsoft.AspNetCore.Components.Forms;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Microsoft.Extensions.Hosting;
using BusinessObjects.Models.FindFormModel;
using BusinessObjects.Models;

namespace API.Controllers
{
    [Route("api/formrequesttutor")]
    [ApiController]
    public class FormRequestTutorController : ControllerBase
    {
        private readonly IRequestTutorFormService _formService;
        private readonly IFindTutorFormService _formFindService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISubjectService _subjectService;
        private readonly IStudentService _studentService;
        private readonly IClassCalenderService _classCalenderService;
        private readonly IClassService _classService;
        private readonly ITutorService _tutorService;
        private readonly IPagingListService<FormRequestTutorVM> _pagingListService;
        private readonly ITutorApplyService _tutorApplyService;

        public FormRequestTutorController(ICurrentUserService currentUserService, IClassService classService)
        {
            _formService = new RequestTutorFormService();
            _currentUserService = currentUserService;
            _subjectService = new SubjectService();
            _studentService = new StudentService();
            _classCalenderService = new ClassCalenderService();

            _tutorService = new TutorService();
            _pagingListService = new PagingListService<FormRequestTutorVM>();
            _formFindService = new FindTutorFormService();
            _tutorApplyService = new TutorApplyService();

            _classService = classService;
        }

        // Create Form Request Tutor + Handle To Avoid Conflict With Tutor Calender
        [HttpPost("create_form")]
        public async Task<IActionResult> CreateRequest(RequestCreateFormRequestTutor form)
        {
            var userId = _currentUserService.GetUserId().ToString();

            var subject = _subjectService.GetSubjects()
                .Where(s => s.SubjectGroupId == form.SubjectGroupId && s.GradeId == form.GradeId)
                .First();

            var student = _studentService.GetStudents()
                                        .Where(s => s.AccountId == userId)
                                        .First();

            //Handle To Avoid Conflict With Tutor Calender
            var check = await _classCalenderService.HandleAvoidConflictCalendar(form.DayOfWeek,
                                                                          form.DayStart,
                                                                          form.DayEnd,
                                                                          form.TimeStart,
                                                                          form.TimeEnd,
                                                                          form.TutorId,
                                                                          1);

            if (check == false)
            {
                return Ok("The registration schedule coincides with the tutor's teaching schedule!");
            }



            // Create form
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
                IsActive = true,
                SubjectId = subject.SubjectId,
                TutorId = form.TutorId,
                StudentId = student.StudentId,
            };


            return Ok(_formService.AddRequestTutorForm(newRequestForm));
        }

        // Tutor/Student view their request tutor form
        [HttpGet("get_form")]
        public IActionResult viewForm(bool? status, int pageIndex)
        {
            var userId = _currentUserService.GetUserId().ToString();

            var memberForm = _formService.GetFormMember(status, userId);

            PagingResult<FormRequestTutorVM> result = new PagingResult<FormRequestTutorVM>();

            if (!memberForm.List.Any())
            {
                return Ok(result);
            }

            var query = from form in memberForm.List
                        orderby form.CreateDay descending
                        select new FormRequestTutorVM()
                        {
                            FormId = form.FormId,
                            CreateDay = form.CreateDay.ToString("yyyy-MM-dd HH:mm"),
                            DayStart = form.DayStart.ToString("yyyy-MM-dd"),
                            DayEnd = form.DayEnd.ToString("yyyy-MM-dd"),
                            DayOfWeek = _classCalenderService.ConvertToDaysOfWeeks(form.DayOfWeek),
                            TimeStart = form.TimeStart.ToString() + "h",
                            TimeEnd = form.TimeEnd.ToString() + "h",
                            SubjectName = form.Subject.Description,
                            FullName = _formService.GetUser(form, userId).FullName,
                            Avatar = _formService.GetUser(form, userId).Avatar,
                            Description = form.Description,
                            Status = form.Status,
                            StudentId = form.StudentId,
                            UserIdStudent = form.Student.AccountId,
                            TutorId = form.TutorId,
                            UserIdTutor = form.Tutor.AccountId,
                        };

            result = _pagingListService.Paging(query.ToList(), pageIndex, 7);

            return Ok(result);
        }

        // Tutor browser form
        [HttpPut("tutor_browserform")]
        public IActionResult TutorBrowserForm(string formId, bool action)
        {
            var form = _formService.GetRequestTutorForms().Where(s => s.FormId == formId).FirstOrDefault();
            if (form == null)
            {
                return BadRequest();
            }

            form.Status = action;
            _formService.UpdateRequestTutorForms(form);

            if (action)
            {
                Form formContainer = new Form()
                {
                    FormId = formId,
                    DayOfWeek = form.DayOfWeek,
                    DayStart = form.DayStart,
                    DayEnd = form.DayEnd,
                    TimeStart = form.TimeStart,
                    TimeEnd = form.TimeEnd,
                };

                _classCalenderService.HandleTutorBrowserForm(formContainer, form.TutorId);
            }

            return Ok(form.Status);
        }

        // Handle form request and form class
        [HttpGet("handle_browserform")]
        public async Task<IActionResult> HandleBrowserForm(string formId, bool action)
        {
            var tutor = _tutorService.GetTutors().Where(s => s.AccountId == _currentUserService.GetUserId().ToString());
            var form = _formService.GetRequestTutorForms().Where(s => s.FormId == formId).FirstOrDefault();
            if (action == false)
            {
                return Ok("Are you sure you want to reject this form?");
            }
            bool checkFormFind = true;
            bool checkFormRequest = true;
            Form formContainer = new Form()
            {
                FormId = formId,
                DayOfWeek = form.DayOfWeek,
                DayStart = form.DayStart,
                DayEnd = form.DayEnd,
                TimeStart = form.TimeStart,
                TimeEnd = form.TimeEnd,
            };

            var formRequestList = _formService.GetRequestTutorForms().Where(s => s.TutorId == tutor.First().TutorId && s.Status == null);
            if (formRequestList.Any())
            {
                checkFormFind = await _classCalenderService.HandleAvoidConflictFormRequest(formRequestList, formContainer);
            }
            var formFindList = _tutorApplyService.GetTutorApplies().Where(s => s.TutorId == tutor.First().TutorId && s.IsApprove == null);
            if (formFindList.Any())
            {
                checkFormRequest = await _classCalenderService.HandleAvoidConflictFormFind(formFindList, formContainer);
            }

            if (checkFormFind == false && checkFormRequest == false)
            {
                return Ok("You have applied for find forms previously, and there are other request forms with schedules that coincide with this form. Accepting this form will result in all other forms with overlapping schedules being rejected or canceled.\r\n\r\nAre you sure you want to accept this form?");
            }

            if (checkFormFind == false)
            {
                return Ok("You have applied for find forms previously with schedules that coincide with this form. Accepting this form will result in all other forms with overlapping schedules being rejected or canceled.\r\n\r\nAre you sure you want to accept this form?");
            }

            if (checkFormRequest == false)
            {
                return Ok("You have request forms with schedules that coincide with this form. Accepting this form will result in all other forms with overlapping schedules being rejected or canceled.\r\n\r\nAre you sure you want to accept this form?");
            }            

            return Ok("Are you sure you want to accept this form?");

        }

        // Handle Create Form 
        [HttpPost("handle_createform")]
        public async Task<IActionResult> HandleCreateForm(HandleCreateForm form)
        {
            var userId = _currentUserService.GetUserId();
            var student = _studentService.GetStudents().FirstOrDefault(s => s.AccountId == userId.ToString());

            var checkForm = await _classCalenderService.HandleStudentCreateForm(form.DayOfWeek, form.DayStart, form.DayEnd, form.TimeStart, form.TimeEnd, student.StudentId);
            var checkClass = await _classCalenderService.HandleAvoidConflictCalendar(form.DayOfWeek, form.DayStart, form.DayEnd, form.TimeStart, form.TimeEnd, student.StudentId, 2);

            if (checkForm == false && checkClass == false)
            {
                return Ok("You have forms and classes that coincide with your registration schedule. Are you sure to create this form?");
            }
            if (checkForm == false)
            {
                return Ok("You have forms that coincide with your registration schedule. Are you sure to create this form?");
            }
            if (checkClass == false)
            {
                return Ok("You have classes that coincide with your registration schedule. Are you sure to create this form?");
            }

            return Ok("Are you sure to create this form?");
        }
    }
}
