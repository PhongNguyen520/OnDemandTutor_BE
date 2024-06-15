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
        public IActionResult GetFeedbackList(string id, int pageIndex, int pageSize)
        {
            var tbFB = iFeedbackService.GetFeedbacks(id).OrderBy(s => s.CreateDay);
            var tbStudent = iStudentService.GetStudents();
            var tbUser = _accountService.GetAccounts();

            var query = from st in tbStudent
                         join us in tbUser
                         on st.AccountId equals us.Id
                         join fb in tbFB
                         on st.StudentId equals fb.StudentId
                         select new FeedbackVM
                         {
                             FeedbackId = fb.FeedbackId,
                             FullName = us.FullName,
                             CreateDay = fb.CreateDay,
                             Description = fb.Description,
                             SubjectName = fb.ClassId,// NOICE
                             Start = fb.Rate,
                         };

            //---------------------PAGING-------------------------
            int validPageIndex = pageIndex > 0 ? pageIndex - 1 : 0;
            int validPageSize = pageSize > 0 ? pageSize : 10;

            query = query.Skip(validPageIndex * validPageSize).Take(validPageSize);

            return Ok(query);
        }

        // GET: api/Feedbacks/5
        //[HttpGet]
        //public async Task<ActionResult<Feedback>> GetFeedback(string id)
        //{
        //    var feedback = await _context.Feedbacks.FindAsync(id);

        //    if (feedback == null)
        //    {
        //        return NotFound();
        //    }

        //    return feedback;
        //}

        //// PUT: api/Feedbacks/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutFeedback(string id, Feedback feedback)
        //{
        //    if (id != feedback.FeedbackId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(feedback).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!FeedbackExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Feedbacks
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Feedback>> PostFeedback(Feedback feedback)
        //{
        //    _context.Feedbacks.Add(feedback);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (FeedbackExists(feedback.FeedbackId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetFeedback", new { id = feedback.FeedbackId }, feedback);
        //}

        //// DELETE: api/Feedbacks/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteFeedback(string id)
        //{
        //    var feedback = await _context.Feedbacks.FindAsync(id);
        //    if (feedback == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Feedbacks.Remove(feedback);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool FeedbackExists(string id)
        //{
        //    return _context.Feedbacks.Any(e => e.FeedbackId == id);
        //}
    }
}
