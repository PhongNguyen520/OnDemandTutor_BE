using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DAOs;
using Services;
using Microsoft.AspNetCore.Authorization;
using BusinessObjects.Constrant;
using BusinessObjects.Models;
using API.Services;
using System.Threading.Tasks.Dataflow;
using NuGet.Protocol;
using System.Globalization;
using BusinessObjects.Models.FindFormModel;

namespace API.Controllers
{
    [Route("api/class")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _classService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ITutorService _tutorService;
        private readonly ISubjectService _subjectService;
        private readonly IStudentService _studentService;
        private readonly IAccountService _accountService;
        private readonly IClassCalenderService _classCalenderService;
        private readonly IPagingListService<ClassVM> _pagingListService;

        public ClassesController(ICurrentUserService currentUserService, IAccountService accountService, IClassService classService)
        {
            _classService = classService;
            _currentUserService = currentUserService;
            _tutorService = new TutorService();
            _subjectService = new SubjectService();
            _studentService = new StudentService();
            _accountService = accountService;
            _classCalenderService = new ClassCalenderService();
            _pagingListService = new PagingListService<ClassVM>();
        }

        // Class tự động tạo sau khi Student duyệt Tutor
        [Authorize(Roles = AppRole.Tutor)]
        [HttpPost("create_class")]
        public async Task<IActionResult> CreateClass(CreateClassVM request)
        {
            var form = _classService.CheckTypeForm(request.FormId);

            var userId = _currentUserService.GetUserId().ToString();

            var tutor = _tutorService.GetTutors().Where(s => s.AccountId == userId).First();

            var check = await _classCalenderService.HandleAvoidConflictCalendar(form.DayOfWeek, form.DayStart, form.DayEnd, form.TimeStart, form.TimeEnd, tutor.TutorId, 1);

            if (check == false)
            {
                return Ok("You can't create new class! You have class that coincide with this schedual!");
            }

            List<DayOfWeek> desiredDays = _classCalenderService.ParseDaysOfWeek(form.DayOfWeek);

            List<DateTime> filteredDates = _classCalenderService.GetDatesByDaysOfWeek(form.DayStart, form.DayEnd, desiredDays);

            var newClass = new Class()
            {
                ClassId = Guid.NewGuid().ToString(),
                ClassName = request.ClassName,
                Price = (float)(tutor.HourlyRate * (form.TimeEnd - form.TimeStart) * filteredDates.Count),
                Description = request.Description,
                CreateDay = DateTime.Now,
                DayStart = form.DayStart,
                DayEnd = form.DayEnd,
                Status = null,
                IsApprove = null,
                StudentId = form.StudentId,
                TutorId = tutor.TutorId,
                SubjectId = form.SubjectId,
                UrlClass = request.UrlClass,
                IsCancel = false,
            };

            _classService.AddClass(newClass);

            var newClassCalender = new ClassCalender();
            foreach (var day in filteredDates)
            {
                newClassCalender.CalenderId = Guid.NewGuid().ToString();
                newClassCalender.DayOfWeek = day;
                newClassCalender.TimeStart = form.TimeStart;
                newClassCalender.TimeEnd = form.TimeEnd;
                newClassCalender.IsActive = false;
                newClassCalender.ClassId = newClass.ClassId;

                _classCalenderService.AddClassCalender(newClassCalender);
            }

            return Ok();
        }

        [HttpGet("get_tutor-calenders")]
        public IActionResult GetTutorCalender(string tutorId)
        {
            var calenders = _classCalenderService.GetClassCalenders();
            var classes = _classService.GetClasses().Where(s => s.TutorId == tutorId && s.Status == null && s.IsApprove == true);

            var result = from calender in calenders
                         join classL in classes
                         on calender.ClassId equals classL.ClassId
                         select new CalenderVM()
                         {
                             BookDay = calender.DayOfWeek.ToString("yyyy-MM-dd"),
                             Time = calender.TimeStart.ToString() + "h - " + calender.TimeEnd.ToString() + "h",
                             ClassId = calender.ClassId,
                         };

            return Ok(result);
        }

        // Tutor view class List
        [HttpGet("get_tutor-classes")]
        public IActionResult TutorViewClass(bool? status, bool? isApprove, int pageIndex)
        {
            var user = _currentUserService.GetUserId().ToString();
            var tutor = _tutorService.GetTutors().First(s => s.AccountId == user);

            var classList = _classService.GetClasses()
                                           .Where(c => c.Status == status && c.IsApprove == isApprove && c.TutorId == tutor.TutorId);

            PagingResult<ClassVM> result = new PagingResult<ClassVM>();
            if (!classList.Any())
            {
                return Ok(result);
            }

            var query = from c in classList
                        orderby c.CreateDay descending
                        select new ClassVM()
                        {
                            Classid = c.ClassId,
                            ClassName = c.ClassName,
                            Createday = c.CreateDay.ToString("yyyy-MM-dd"),
                            Description = c.Description,
                            Price = c.Price,
                            DayStart = c.DayStart.ToString("yyyy-MM-dd"),
                            DayEnd = c.DayEnd.ToString("yyyy-MM-dd"),
                            SubjectName = _subjectService.GetSubjects()
                                          .Where(s => s.SubjectId == c.SubjectId)
                                          .Select(s => s.Description)
                                          .FirstOrDefault(),
                            UserId = (from s in _studentService.GetStudents()
                                      join a in _accountService.GetAccounts()
                                      on s.AccountId equals a.Id
                                      where s.StudentId == c.StudentId
                                      select a.Id).FirstOrDefault(),
                            FullName = (from s in _studentService.GetStudents()
                                        join a in _accountService.GetAccounts()
                                        on s.AccountId equals a.Id
                                        where s.StudentId == c.StudentId
                                        select a.FullName).FirstOrDefault(),
                            Avatar = (from s in _studentService.GetStudents()
                                      join a in _accountService.GetAccounts()
                                    on s.AccountId equals a.Id
                                      where s.StudentId == c.StudentId
                                      select a.Avatar).FirstOrDefault(),
                            Status = c.Status,
                            IsApprove = c.IsApprove,
                            UrlClass = c.UrlClass,
                            IsCancel = c.IsCancel,
                            CancelDay = c.CancelDay,
                        };

            result = _pagingListService.Paging(query.ToList(), pageIndex, 7);

            return Ok(result);
        }

        // Student view class list
        [HttpGet("get_student-classes")]
        public IActionResult StudentViewClass(bool? status, bool? isApprove, int pageIndex)
        {
            var user = _currentUserService.GetUserId().ToString();
            var student = _studentService.GetStudents().First(s => s.AccountId == user);


            var classList = _classService.GetClasses()
                                           .Where(c => c.Status == status && c.IsApprove == isApprove && c.StudentId == student.StudentId);

            PagingResult<ClassVM> result = new PagingResult<ClassVM>();
            if (!classList.Any())
            {
                return Ok(result);
            }

            var query = from c in classList
                        orderby c.CreateDay descending
                        select new ClassVM()
                        {
                            Classid = c.ClassId,
                            ClassName = c.ClassName,
                            Createday = c.CreateDay.ToString("yyyy-MM-dd"),
                            Description = c.Description,
                            Price = c.Price,
                            DayStart = c.DayStart.ToString("yyyy-MM-dd"),
                            DayEnd = c.DayEnd.ToString("yyyy-MM-dd"),
                            SubjectName = _subjectService.GetSubjects()
                                         .Where(s => s.SubjectId == c.SubjectId)
                                         .Select(s => s.Description)
                                         .FirstOrDefault(),
                            UserId = (from t in _tutorService.GetTutors()
                                      join a in _accountService.GetAccounts()
                                      on t.AccountId equals a.Id
                                      where t.TutorId == c.TutorId
                                      select a.Id).FirstOrDefault(),
                            FullName = (from t in _tutorService.GetTutors()
                                        join a in _accountService.GetAccounts()
                                        on t.AccountId equals a.Id
                                        where t.TutorId == c.TutorId
                                        select a.FullName).FirstOrDefault(),
                            Avatar = (from t in _tutorService.GetTutors()
                                      join a in _accountService.GetAccounts()
                                      on t.AccountId equals a.Id
                                      where t.TutorId == c.TutorId
                                      select a.Avatar).FirstOrDefault(),
                            Status = c.Status,
                            IsApprove = c.IsApprove,
                            UrlClass = c.UrlClass,
                            IsCancel = c.IsCancel,
                            CancelDay = c.CancelDay,
                        };

            result = _pagingListService.Paging(query.ToList(), pageIndex, 7);

            return Ok(result);
        }

        [HttpGet("get_class-detail")]
        public IActionResult ClassDetail(string classid)
        {
            var classDetail = _classService.GetClasses().Where(s => s.ClassId == classid).First();
            var calenders = from calender in _classCalenderService.GetClassCalenders().Where(s => s.ClassId == classid)
                            select new CalenderVM()
                            {
                                Id = calender.CalenderId,
                                BookDay = calender.DayOfWeek.ToString("yyyy-MM-dd"),
                                Time = calender.TimeStart.ToString() + "h - " + calender.TimeEnd.ToString() + "h",
                                ClassId = calender.ClassId,
                                IsChecked = calender.IsActive,
                            };
            var result = new ClassDetail()
            {
                ClassId = classid,
                ClassName = classDetail.ClassName,
                Description = classDetail.Description,
                SubjectName = _subjectService.GetSubjects()
                                         .Where(s => s.SubjectId == classDetail.SubjectId)
                                         .Select(s => s.Description)
                                         .FirstOrDefault(),
                Createday = classDetail.CreateDay.ToString("yyyy-MM-dd"),
                Price = classDetail.Price,
                DayStart = classDetail.DayStart.ToString("yyyy-MM-dd"),
                DayEnd = classDetail.DayEnd.ToString("yyyy-MM-dd"),
                TutorId = classDetail.TutorId,
                StudentId = classDetail.StudentId,
                UrlClass = classDetail.UrlClass,
                Calenders = calenders.ToList(),
            };

            return Ok(result);
        }

        //Student browse class
        [HttpPut("student_browse-class")]
        public IActionResult BrowseClass(string classId, bool action)
        {
            if (classId is not null)
            {
                var getClass = _classService.GetClasses().Where(s => s.ClassId == classId).First();
                getClass.IsApprove = action;
                _classService.UpdateClasses(getClass);
            }
            else
            {
                return NoContent();
            }
            return Ok();
        }

        [Authorize(Roles = AppRole.Student)]
        //Student checking teaching day
        [HttpPut("student_checking-day")]
        public IActionResult Checking(string calenderId)
        {
            var calender = _classCalenderService.GetClassCalenders().Where(s => s.CalenderId == calenderId).FirstOrDefault();
            calender.IsActive = true;

            return Ok("Checked!");
        }

        [HttpGet("get_class-by-month-or-day")]
        public async Task<IActionResult> ShowClassByMonthOrDay(string timeChoice)
        {
            if (timeChoice.ToUpper() == "DAY")
            {
                var list = await _classService.GetClassByDay();
                return Ok(list);
            }
            if(timeChoice.ToUpper() == "MONTH")
            {
                var list2 = await _classService.GetClassByMonth();
                return Ok(list2);
            }
            return BadRequest("NoNo!!!");
        }

        [Authorize(Roles = AppRole.Moderator)]
        [HttpDelete("delete_class/{id}")]
        public IActionResult CancelClass(string id)
        {
            var classRoom = _classService.GetClasses().Where(s => s.ClassId == id).FirstOrDefault();
            classRoom.IsCancel = true;
            _classService.UpdateClasses(classRoom);
            return Ok("Cancel successful");
        }

        
        [HttpPut("submit_class/{id}")]
        public IActionResult SubmitClass(string id)
        {
            var classRoom = _classService.GetClasses().Where(s => s.ClassId == id).FirstOrDefault();
            classRoom.Status = true;
            _classService.UpdateClasses(classRoom);
            return Ok("Submit successful");
        }

        [Authorize(Roles = AppRole.Tutor)]
        [HttpPut("update_class-url/{id}")]
        public IActionResult ChangeClassUrl(string id, string newUrl)
        {
            var classRoom = _classService.GetClasses().Where(s => s.ClassId == id).FirstOrDefault();
            classRoom.UrlClass = newUrl;
            _classService.UpdateClasses(classRoom);
            return Ok("Change class url successful");
        }
    }
}
