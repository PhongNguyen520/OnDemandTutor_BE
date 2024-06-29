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

        public ClassesController(ICurrentUserService currentUserService, IAccountService accountService)
        {
            _classService = new ClassService();
            _currentUserService = currentUserService;
            _tutorService = new TutorService();
            _subjectService = new SubjectService();
            _studentService = new StudentService();
            _accountService = accountService;
            _classCalenderService = new ClassCalenderService();
        }

        // Tutor tạo class
        [HttpPost("createClass")]
        public IActionResult Create(CreateRequestTutor request)
        {
            var user = _currentUserService.GetUserId().ToString();
            var tutor = _tutorService.GetTutors().Where(s => s.AccountId == user).FirstOrDefault();
            var student = _studentService.GetStudents().Where(s => s.AccountId == request.StudentId).FirstOrDefault();

            List<DayOfWeek> desiredDays = _classCalenderService.ParseDaysOfWeek(request.DayOfWeek);

            List<DateTime> filteredDates = _classCalenderService.GetDatesByDaysOfWeek(request.DayStart, request.DayEnd, desiredDays);

            var newClass = new Class()
            {
                ClassId = Guid.NewGuid().ToString(),
                ClassName = request.ClassName,
                Price = (float)(tutor.HourlyRate * (request.TimeEnd - request.TimeStart)),
                Description = request.Description,
                CreateDay = DateTime.Now,
                DayStart = request.DayStart,
                DayEnd = request.DayEnd,
                Status = null,
                IsApprove = null,
                StudentId = student.StudentId,
                TutorId = tutor.TutorId,
                SubjectId = _subjectService.GetSubjects().Where(s => s.GradeId == request.GradeId && s.SubjectGroupId == request.SubjectGroupId).Select(s => s.SubjectId).FirstOrDefault(),
            };

            var newClassCalender = new ClassCalender();
            foreach (var day in filteredDates)
            {
                newClassCalender.CalenderId = Guid.NewGuid().ToString();
                newClassCalender.DayOfWeek = day;
                newClassCalender.TimeStart = request.TimeStart;
                newClassCalender.TimeEnd = request.TimeEnd;
                newClassCalender.IsActive = true;
                newClassCalender.ClassId = newClass.ClassId;

                _classCalenderService.AddClassCalender(newClassCalender);
            }

            _classService.AddClass(newClass);

            return Ok();
        }

        [HttpGet("showTutorCalender")]
        public IActionResult GetTutorCalender(string tutorId)
        {
            var calenders = _classCalenderService.GetClassCalenders();
            var classes = _classService.GetClasses().Where(s => s.TutorId == tutorId && s.Status != true);

            var result = from calender in calenders
                         join classL in classes
                         on calender.ClassId equals classL.ClassId
                         select new CalenderVM()
                         {
                             BookDay = DateTime.Now,
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

            if (tutor == null)
            {
                return NotFound("Tutor not found.");
            }

            var classList = _classService.GetClasses()
                                           .Where(c => c.IsApprove == true && c.Status == action && c.TutorId == tutor.TutorId);

            var classVMs = classList.Select(c => new ClassVM()
            {
                ClassName = c.ClassName,
                Createday = c.CreateDay,
                Description = c.Description,
                Price = c.Price,
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
            }).ToList();

            return Ok(classVMs);
        }

        // Student view class
        [HttpGet("student/viewClass")]
        public IActionResult StudentViewClass(bool action)
        {
            var user = _currentUserService.GetUserId().ToString();
            var student = _studentService.GetStudents().FirstOrDefault(s => s.AccountId == user);

            if (student == null)
            {
                return NotFound("Student not found.");
            }

            var classList = _classService.GetClasses()
                                           .Where(c => c.IsApprove == true && c.Status == action && c.StudentId == student.StudentId);

            var classVMs = classList.Select(c => new ClassVM()
            {
                ClassName = c.ClassName,
                Createday = c.CreateDay,
                Description = c.Description,
                Price = c.Price,
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
            }).ToList();

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
            } else
            {
                return NoContent();
            }

            

            return Ok();
        }

        //Student complete class
        [HttpPut("student/completeClass")]
        public IActionResult CompleteClass(string classId, bool action)
        {
            if (classId is not null)
            {
                var getClass = _classService.GetClasses().Where(s => s.ClassId == classId).FirstOrDefault();
                getClass.Status = action;
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
