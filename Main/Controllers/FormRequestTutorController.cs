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
        private readonly IPagingListService<FormRequestTutorVM> _pagingListService;

        public FormRequestTutorController(ICurrentUserService currentUserService)
        {
            _formService = new RequestTutorFormService();
            _currentUserService = currentUserService;
            _subjectService = new SubjectService();
            _studentService = new StudentService();
            _classCalenderService = new ClassCalenderService();
            _classService = new ClassService();
            _tutorService = new TutorService();
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
                IsActive = true,
                SubjectId = subject.SubjectId,
                TutorId = form.TutorId,
                StudentId = student.StudentId,
            };

            _formService.AddRequestTutorForm(newRequestForm);

            return Ok(newRequestForm.FormId);
        }

        // Tutor/Student view their request tutor form
        [HttpGet("viewForm")]
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
                            SubjectName = _subjectService.GetSubjects().Where(s => s.SubjectId == form.SubjectId).Select(s => s.Description).FirstOrDefault(),
                            FullName = memberForm.FullName,
                            Avatar = memberForm.Avatar,
                            Description = form.Description,
                            Status = form.Status,
                            StudentId = form.StudentId,
                            TutorId = form.TutorId,
                        };

            result = _pagingListService.Paging(query.ToList(), pageIndex, 7);

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
