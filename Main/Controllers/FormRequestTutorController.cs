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
        private readonly IClassCalenderService _classCalenderService;
        private readonly IClassService _classService;
        private readonly ITutorService _tutorService;
        private readonly IAccountService _accountService;
        private readonly IPagingListService<FormRequestTutorVM> _pagingListService;

        public FormRequestTutorController(ICurrentUserService currentUserService, IAccountService accountService)
        {
            _formService = new RequestTutorFormService();
            _currentUserService = currentUserService;
            _subjectService = new SubjectService();
            _studentService = new StudentService();
            _classCalenderService = new ClassCalenderService();
            _classService = new ClassService();
            _tutorService = new TutorService();
            _accountService = accountService;
            _pagingListService = new PagingListService<FormRequestTutorVM>();
        }

        // Create Form Request Tutor + Handle To Avoid Conflict With Tutor Calender
        [HttpPost("createForm")]
        public IActionResult CreateRequest(RequestCreateFormRequestTutor form)
        {
            var userId = _currentUserService.GetUserId().ToString();

            var subject = _subjectService.GetSubjects()
                .Where(s => s.SubjectGroupId == form.SubjectGroupId && s.GradeId == form.GradeId)
                .First();

            var student = _studentService.GetStudents()
                                        .Where(s => s.AccountId == userId)
                                        .First();

            //Handle To Avoid Conflict With Tutor Calender
            //1.Get all booking day
            List<DayOfWeek> desiredDays = _classCalenderService.ParseDaysOfWeek(form.DayOfWeek);

            var filteredDates = _classCalenderService.GetDatesByDaysOfWeek(form.DayStart, form.DayEnd, desiredDays);

            //2.Select tutor's calender
            var tutorClass = _classService.GetClasses().Where(s => s.Status == null && s.IsApprove == true);

            if (tutorClass.Any())
            {
                var calender = from a in tutorClass
                               join b in _classCalenderService.GetClassCalenders()
                               on a.ClassId equals b.ClassId
                               select b;

                // 3. Checking
                foreach (var day in calender)
                {
                    for (int i = 0; i < filteredDates.Count; i++)
                    {
                        if (day.DayOfWeek == filteredDates[i])
                        {
                            if (form.TimeStart <= day.TimeStart && form.TimeEnd >= day.TimeEnd
                             || form.TimeStart >= day.TimeStart && form.TimeStart < day.TimeEnd
                             || form.TimeEnd > day.TimeStart && form.TimeEnd <= day.TimeEnd)
                            {
                                return BadRequest("The calender is not suiable");
                            }
                        }
                    }
                }
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
                IsActive = null,
                SubjectId = subject.SubjectId,
                TutorId = form.TutorId,
                StudentId = student.StudentId,
            };

            _formService.AddRequestTutorForm(newRequestForm);

            return Ok(newRequestForm.FormId);
        }

        // Student view their request tutor form
        [HttpGet("studentViewForm")]
        public IActionResult StudentViewForm(int pageIndex)
        {
            var userId = _currentUserService.GetUserId().ToString();
            var studentId = _studentService.GetStudents().Where(s => s.AccountId == userId).Select(s => s.StudentId).FirstOrDefault();
            var tutors = _tutorService.GetTutors();
            var accounts = _accountService.GetAccounts();

            var formList = _formService.GetRequestTutorForms().Where(s => s.StudentId == studentId);
            var query = from form in formList
                        join tutor in tutors
                        on form.TutorId equals tutor.TutorId
                        join account in accounts
                        on tutor.AccountId equals account.Id
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
                            SubjectName = _subjectService.GetSubjects().Where(s => s.SubjectId == form.SubjectId).Select(s => s.Description).FirstOrDefault(),
                            FullName = account.FullName,
                            Avatar = account.Avatar,
                            Description = form.Description,
                            StudentId = form.StudentId,
                            TutorId = form.TutorId,
                        };

            var result = _pagingListService.Paging(query.ToList(), pageIndex, 7);

            return Ok(result);
        }

        // Tutor view their request tutor form
        [HttpGet("tutorViewForm")]
        public IActionResult TutorViewForm(int pageIndex)
        {
            var userId = _currentUserService.GetUserId().ToString();
            var tutorId = _tutorService.GetTutors().Where(s => s.AccountId == userId).Select(s => s.TutorId).FirstOrDefault();
            var students = _studentService.GetStudents();
            var accounts = _accountService.GetAccounts();

            var formList = _formService.GetRequestTutorForms().Where(s => s.TutorId == tutorId);
            var query = from form in formList
                        join student in students
                        on form.StudentId equals student.StudentId
                        join account in accounts
                        on student.AccountId equals account.Id
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
                            SubjectName = _subjectService.GetSubjects().Where(s => s.SubjectId == form.SubjectId).Select(s => s.Description).FirstOrDefault(),
                            FullName = account.FullName,
                            Avatar = account.Avatar,
                            Description = form.Description,
                            StudentId = form.StudentId,
                            TutorId = form.TutorId,
                        };

            var result = _pagingListService.Paging(query.ToList(), pageIndex, 7);

            return Ok(result);
        }

        // Tutor browser form
        [HttpPut("tutorBrowserForm")]
        public IActionResult TutorBrowserForm(string formId, bool action)
        {
            var form = _formService.GetRequestTutorForms().Where(s => s.FormId == formId).FirstOrDefault();
            if (form == null)
            {
                return BadRequest();
            }
            if (action == false)
            {
                form.Status = action;
            }
            else
            {
                var list = _classCalenderService.HandleCalendar(formId).ToList();
                foreach (var item in list)
                {
                    item.Status = false;
                    _formService.UpdateRequestTutorForms(item);
                }
                form.Status = true;
            }

            _formService.UpdateRequestTutorForms(form);

            return Ok(form.Status);
        }

        [HttpGet("handleBrowserForm")]
        public IActionResult HandleBrowserForm(string formId, bool action)
        {
            if (action == false)
            {
                return Ok();
            }

            var list = _classCalenderService.HandleCalendar(formId);

            return Ok(list.Count);

        }
    }
}
