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
using BusinessObjects.Models.TutorModel;
using API.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbacksController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IStudentService _studentService;
        private readonly IAccountService _accountService;
        private readonly IClassService _classService;
        private readonly ISubjectService _subjectService;
        private readonly ICurrentUserService _currentUserService;
        private readonly IPagingListService<FeedbackVM> _pagingListService;

        public FeedbacksController(IAccountService accountService, ICurrentUserService currentUserService, IClassService classService)
        {
            _feedbackService = new FeedbackService();
            _studentService = new StudentService();
            _accountService = accountService;
            _classService = classService;
            _subjectService = new SubjectService();
            _currentUserService = currentUserService;
            _pagingListService = new PagingListService<FeedbackVM>();
        }

        // GET: api/Feedbacks
        [HttpGet("{id}")]
        public IActionResult GetFeedbackList(string id, int pageIndex)
        {
            var tbFB = _feedbackService.GetFeedbacks(id);
            var tbStudent = _studentService.GetStudents();
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
                            CreateDay = fb.CreateDay.ToString("dd/MM/yyyy HH:mm"),
                            Description = fb.Description,
                            SubjectName = tbSubjects
                                .Join(tbClasses, s => s.SubjectId, c => c.SubjectId, (s, c) => new { s.Description, c.StudentId })
                                .Where(sc => sc.StudentId == st.StudentId)
                                .Select(sc => sc.Description)
                                .FirstOrDefault(), // Lấy tên môn học đầu tiên khớp với StudentId
                            Start = fb.Rate,
                            Title = fb.Title,
                        };

            var result = _pagingListService.Paging(query.ToList(), pageIndex, 5);

            return Ok(result);
        }

        //Student Create FeedBack
        [HttpPost("createFeedback")]
        public IActionResult createFeedback(CreateFeedback request)
        {
            var userId = _currentUserService.GetUserId().ToString();
            var student = _studentService.GetStudents().Where(s => s.AccountId ==  userId).First();
            var result = new Feedback
            {
                FeedbackId = Guid.NewGuid().ToString(),
                CreateDay = DateTime.Now,
                Description = request.Description,
                Rate = request.Star,
                IsActive = true,
                StudentId = student.StudentId,
                TutorId = request.TutorId,
                ClassId = request.ClassId,
                Title = request.Title,
            };
            _feedbackService.AddFeedback(result);
            return Ok(result);
        }
    }
}
