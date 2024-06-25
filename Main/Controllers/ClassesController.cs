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

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AppRole.Admin)]
    public class ClassesController : ControllerBase
    {
        private readonly IClassService _iClassService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ITutorService _tutorService;
        private readonly ISubjectService _subjectService;

        public ClassesController(ICurrentUserService currentUserService)
        {
            _iClassService = new ClassService();
            _currentUserService = currentUserService;
            _tutorService = new TutorService();
            _subjectService = new SubjectService();
        }

        // Tutor tạo class
        [HttpPost("tutor/createClass")]
        public IActionResult Create(ClassVM request)
        {
            var user = _currentUserService.GetUserId().ToString();
            var tutor = _tutorService.GetTutors().Where(s => s.AccountId == user).FirstOrDefault();

            var newClass = new Class()
            {
                ClassId = Guid.NewGuid().ToString(),
                ClassName = request.ClassName,
                Price = tutor.HourlyRate,
                Description = request.Description,
                CreateDay = DateTime.Now,
                HourPerDay = request.HourPerDay,
                DayPerWeek = request.DayPerWeek,
                Status = null,
                IsApprove = null,
                StudentId = request.StudentId,
                TutorId = tutor.TutorId,
                SubjectId = _subjectService.GetSubjects().Where(s => s.GradeId == request.GradeId && s.SubjectGroupId == request.SubjectGroupId).Select(s => s.SubjectId).FirstOrDefault(),
            };

            _iClassService.AddClass(newClass);

            return Ok();
        }

        // Tutor view class
        [HttpGet("tutor/viewClass")]
        public IActionResult TutorViewClass()
        {
            return Ok();
        }

        // Student view class
        [HttpGet("student/viewClass")]
        public IActionResult StudentViewClass()
        {
            return Ok();
        }

        [HttpPut("student/browseClass")]
        public IActionResult BrowseClass(string classId, bool action)
        {
            if (classId is not null)
            {
                var getClass = _iClassService.GetClasses().Where(s => s.ClassId == classId).FirstOrDefault();
                getClass.Status = action;
            } else
            {
                return NoContent();
            }

            return Ok();
        }
    }
}
