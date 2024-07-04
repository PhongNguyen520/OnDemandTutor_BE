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

namespace API.Controllers
{
    [Route("api/[controller]")]
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
        private readonly IRequestTutorFormService _requestTutorFormService;
        private readonly IFindTutorFormService _findTutorFormService;

        public ClassesController(ICurrentUserService currentUserService, IAccountService accountService)
        {
            _classService = new ClassService();
            _currentUserService = currentUserService;
            _tutorService = new TutorService();
            _subjectService = new SubjectService();
            _studentService = new StudentService();
            _accountService = accountService;
            _classCalenderService = new ClassCalenderService();
            _requestTutorFormService = new RequestTutorFormService();
            _findTutorFormService = new FindTutorFormService();
        }

        // Class tự động tạo sau khi Tutor duyệt form
        [HttpPost("createClassByFormRequest")]
        public IActionResult CreateByRequest(CreateClassByRequestVM request)
        {
            var form = _requestTutorFormService.GetRequestTutorForms().Where(s => s.FormId == request.FormId).FirstOrDefault();

            var tutor = _tutorService.GetTutors().Where(s => s.TutorId == form.TutorId).FirstOrDefault();

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
                Status = true,
                IsApprove = null,
                StudentId = form.StudentId,
                TutorId = tutor.TutorId,
                SubjectId = form.SubjectId,
            };

            _classService.AddClass(newClass);

            var newClassCalender = new ClassCalender();
            foreach (var day in filteredDates)
            {
                newClassCalender.CalenderId = Guid.NewGuid().ToString();
                newClassCalender.DayOfWeek = day;
                newClassCalender.TimeStart = form.TimeStart;
                newClassCalender.TimeEnd = form.TimeEnd;
                newClassCalender.IsActive = true;
                newClassCalender.ClassId = newClass.ClassId;

                _classCalenderService.AddClassCalender(newClassCalender);
            }

            return Ok();
        }

        // Class tự động tạo sau khi Student duyệt Tutor
        [HttpPost("createClassByFormFind")]
        public IActionResult CreateByFind(CreateClassByFindVM request)
        {
            var form = _findTutorFormService.GetFindTutorForms().Where(s => s.FormId == request.FormId).FirstOrDefault();

            var tutor = _tutorService.GetTutors().Where(s => s.TutorId == request.TutorId).FirstOrDefault();

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
                Status = true,
                IsApprove = null,
                StudentId = form.StudentId,
                TutorId = tutor.TutorId,
                SubjectId = form.SubjectId,
            };

            _classService.AddClass(newClass);

            var newClassCalender = new ClassCalender();
            foreach (var day in filteredDates)
            {
                newClassCalender.CalenderId = Guid.NewGuid().ToString();
                newClassCalender.DayOfWeek = day;
                newClassCalender.TimeStart = form.TimeStart;
                newClassCalender.TimeEnd = form.TimeEnd;
                newClassCalender.IsActive = true;
                newClassCalender.ClassId = newClass.ClassId;

                _classCalenderService.AddClassCalender(newClassCalender);
            }

            return Ok();
        }

        [HttpGet("showTutorCalender")]
        public IActionResult GetTutorCalender(string tutorId)
        {
            var calenders = _classCalenderService.GetClassCalenders();
            var classes = _classService.GetClasses().Where(s => s.TutorId == tutorId && s.Status == null && s.IsApprove == true);

            var result = from calender in calenders
                         join classL in classes
                         on calender.ClassId equals classL.ClassId
                         select new CalenderVM()
                         {
                             BookDay = calender.DayOfWeek,
                             TimeStart = calender.TimeStart,
                             TimeEnd = calender.TimeEnd,
                             ClassId = calender.ClassId,
                         };

            return Ok(result);
        }

        // Tutor view class
        [HttpGet("tutor/viewClass")]
        public IActionResult TutorViewClass(bool action)
        {
            var user = _currentUserService.GetUserId().ToString();
            var tutor = _tutorService.GetTutors().FirstOrDefault(s => s.AccountId == user);

            var classList = _classService.GetClasses()
                                           .Where(c => c.Status == action && c.TutorId == tutor.TutorId);

            var classVMs = from c in classList
                           select new ClassVM()
                           {
                               ClassName = c.ClassName,
                               Createday = c.CreateDay,
                               Description = c.Description,
                               Price = c.Price,
                               DayStart = c.DayStart,
                               DayEnd = c.DayEnd,
                               SubjectName = _subjectService.GetSubjects()
                                             .Where(s => s.SubjectId == c.SubjectId)
                                             .Select(s => s.Description)
                                             .FirstOrDefault(),
                               StudentName = (from s in _studentService.GetStudents()
                                              join a in _accountService.GetAccounts()
                                              on s.AccountId equals a.Id
                                              where s.StudentId == c.StudentId
                                              select a.FullName).FirstOrDefault(),
                               StudentAvatar = (from s in _studentService.GetStudents()
                                                join a in _accountService.GetAccounts()
                                              on s.AccountId equals a.Id
                                                where s.StudentId == c.StudentId
                                                select a.Avatar).FirstOrDefault(),
                               TutorName = _accountService.GetAccounts()
                                             .Where(a => a.Id == tutor.AccountId)
                                             .Select(a => a.FullName)
                                             .FirstOrDefault(),
                               TutorAvatar = _accountService.GetAccounts()
                                               .Where(a => a.Id == tutor.AccountId)
                                               .Select(a => a.Avatar)
                                               .FirstOrDefault(),
                               ClassCalenders = _classCalenderService.GetClassCalenders().Where(s => s.ClassId == c.ClassId).ToList(),
                           };

            return Ok(classVMs);
        }

        // Student view class
        [HttpGet("student/viewClass")]
        public IActionResult StudentViewClass(bool action)
        {
            var user = _currentUserService.GetUserId().ToString();
            var student = _studentService.GetStudents().FirstOrDefault(s => s.AccountId == user);


            var classList = _classService.GetClasses()
                                           .Where(c => c.Status == action && c.StudentId == student.StudentId);

            var classVMs = from c in classList
                           select new ClassVM()
                           {
                               ClassName = c.ClassName,
                               Createday = c.CreateDay,
                               Description = c.Description,
                               Price = c.Price,
                               DayStart = c.DayStart,
                               DayEnd = c.DayEnd,
                               SubjectName = _subjectService.GetSubjects()
                                            .Where(s => s.SubjectId == c.SubjectId)
                                            .Select(s => s.Description)
                                            .FirstOrDefault(),
                               StudentName = _accountService.GetAccounts()
                                            .Where(a => a.Id == student.AccountId)
                                            .Select(a => a.FullName)
                                            .FirstOrDefault(),
                               StudentAvatar = _accountService.GetAccounts()
                                              .Where(a => a.Id == student.AccountId)
                                              .Select(a => a.Avatar)
                                              .FirstOrDefault(),
                               TutorName = (from t in _tutorService.GetTutors()
                                            join a in _accountService.GetAccounts()
                                            on t.AccountId equals a.Id
                                            where t.TutorId == c.TutorId
                                            select a.FullName).FirstOrDefault(),
                               TutorAvatar = (from t in _tutorService.GetTutors()
                                              join a in _accountService.GetAccounts()
                                              on t.AccountId equals a.Id
                                              where t.TutorId == c.TutorId
                                              select a.Avatar).FirstOrDefault(),
                               ClassCalenders = _classCalenderService.GetClassCalenders().Where(s => s.ClassId == c.ClassId).ToList(),

                           };

            return Ok(classVMs);
        }


        //Student browse class
        [HttpPut("student/browseClass")]
        public IActionResult BrowseClass(string classId, bool action)
        {
            if (classId is not null)
            {
                var getClass = _classService.GetClasses().Where(s => s.ClassId == classId).FirstOrDefault();
                getClass.IsApprove = action;
                _classService.UpdateClasses(getClass);
            }
            else
            {
                return NoContent();
            }
            return Ok();
        }
    }
}
