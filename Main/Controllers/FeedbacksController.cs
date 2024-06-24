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
using BusinessObjects.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService iFeedbackService;
        private readonly IStudentService iStudentService;
        private readonly IAccountService _accountService;
        private readonly IClassService _classService;
        private readonly ISubjectService _subjectService;

        public FeedbacksController(IAccountService accountService)
        {
            iFeedbackService = new FeedbackService();
            iStudentService = new StudentService();
            _accountService = accountService;
            _classService = new ClassService();
            _subjectService = new SubjectService();
        }

        // GET: api/Feedbacks
        [HttpGet("{id}")]
        public IActionResult GetFeedbackList(string id, int pageIndex)
        {
            var tbFB = iFeedbackService.GetFeedbacks(id);
            var tbStudent = iStudentService.GetStudents();
            var tbUser = _accountService.GetAccounts();
            var tbSubjects = _subjectService.GetSubjects();
            var tbClasses = _classService.GetClasses();

            var query = from st in tbStudent
                        join us in tbUser on st.AccountId equals us.Id
                        join fb in tbFB on st.StudentId equals fb.StudentId
                        orderby fb.CreateDay descending
                        select new FeedbackVM
                        {
                            FeedbackId = fb.FeedbackId,
                            StudentId = st.StudentId,
                            FullName = us.FullName,
                            CreateDay = fb.CreateDay,
                            Description = fb.Description,
                            SubjectName = tbSubjects
                                .Join(tbClasses, s => s.SubjectId, c => c.SubjectId, (s, c) => new { s.Description, c.StudentId })
                                .Where(sc => sc.StudentId == st.StudentId)
                                .Select(sc => sc.Description)
                                .FirstOrDefault(), // Lấy tên môn học đầu tiên khớp với StudentId
                            Start = fb.Rate,
                            Title = fb.Title,
                        };

            // --------------------- PAGING -------------------------
            int validPageIndex = pageIndex > 0 ? pageIndex - 1 : 0;
            int validPageSize = 5;

            if (query.Count() < 5)
            {
                validPageSize = query.Count();
            }

            query = query.Skip(validPageIndex * validPageSize).Take(validPageSize).ToList();

            return Ok(query);
        }
    }
}
