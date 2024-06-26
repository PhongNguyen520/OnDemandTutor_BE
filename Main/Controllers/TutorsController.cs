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
using NuGet.Protocol;
using Repositories;
using BusinessObjects.Models.TutorModel;
using System.Net;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Identity;
using API.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TutorsController : ControllerBase
    {
        private readonly ITutorService iTutorService;
        private readonly IAccountService iAccountService;
        private readonly IFeedbackService iFeedbackService;
        private readonly IGradeService iGradeService;
        private readonly ISubjectGroupService iSubjectGroupService;
        private readonly ISubjectService iSubjectService;
        private readonly ISubjectTutorService iSubjectTutorService;
        private readonly ICurrentUserService currentUserService;


        //public TutorsController(IAccountService accountService)
        //{
        //    iTutorService = new TutorService();
        //    iAccountService = accountService;
        //    iFeedbackService = new FeedbackService();
        //    iGradeService = new GradeService();
        //    iSubjectGroupService = new SubjectGroupService();
        //    iSubjectService = new SubjectService();
        //    iSubjectTutorService = new SubjectTutorService();
        //}
        public TutorsController(IAccountService accountService, ITutorService _iTutorService, ICurrentUserService _currentUserServiece)
        {
            iTutorService = _iTutorService;
            currentUserService = _currentUserServiece;

            iAccountService = accountService;
            iFeedbackService = new FeedbackService();
            iGradeService = new GradeService();
            iSubjectGroupService = new SubjectGroupService();
            iSubjectService = new SubjectService();
            iSubjectTutorService = new SubjectTutorService();
        }

        // GET: api/Tutors
        [HttpGet]
        public IActionResult FilterTutor([FromQuery] RequestSearchTutorModel requestSearchTutorModel)
        {
            var sortBy = requestSearchTutorModel.SortContent != null ? requestSearchTutorModel.SortContent?.sortTutorBy.ToString() : null;
            var sortType = requestSearchTutorModel.SortContent != null ? requestSearchTutorModel.SortContent?.sortTutorType.ToString() : null;
            var searchQuery = requestSearchTutorModel.Search.ToLower();

            //List tutors active 
            var allTutor = iTutorService.Filter(requestSearchTutorModel);

            // TÌM KIẾM THEO TÊN GIẢNG VIÊN
            var allAccount = iAccountService.GetAccounts()
                .Where(ac => ac.FullName.ToLower().Contains(searchQuery) && ac.IsActive == true);


            // TÌM KIẾM THEO TÊN NHÓM MÔN HỌC
            var allSubjectGroup = iSubjectGroupService.GetSubjectGroups().Where(su => su.SubjectName.ToLower().Contains(searchQuery));

            if (!allSubjectGroup.Any())
            {
                allSubjectGroup = iSubjectGroupService.GetSubjectGroups();
            }
            else if (!allAccount.Any())
            {
                allAccount = iAccountService.GetAccounts();
            }
            else if (!allSubjectGroup.Any() && !allAccount.Any())
            {
                allAccount = null;
                allSubjectGroup = null;
            }

            IEnumerable<Subject> allSubject = iSubjectService.GetSubjects();

            // Trường hợp chọn Grade
            if (!string.IsNullOrEmpty(requestSearchTutorModel.GradeId))
            {
                allSubject = allSubject.Where(s => s.GradeId == requestSearchTutorModel.GradeId);
            }// kết thúc

            // Trường hợp chọn Gender
            if (requestSearchTutorModel.Gender is not null)
            {
                allAccount = allAccount.Where(s => s.Gender == requestSearchTutorModel.Gender);
            }// kết thúc


            IEnumerable<Subject> subjects = from sg in allSubjectGroup
                                            join s in allSubject
                                            on sg.SubjectGroupId equals s.SubjectGroupId
                                            select s;

            var allSubjectTutor = iSubjectTutorService.GetAllSubjectTutors();

            //Lấy danh sách giảng viên dạy môn học tìm kiếm
            IEnumerable<Tutor> list = from st in allSubjectTutor
                                      join s in subjects
                                      on st.SubjectId equals s.SubjectId
                                      join t in allTutor
                                      on st.TutorId equals t.TutorId
                                      select t;

            list = list.Distinct();


            //_____TẠO DANH SÁCH KẾT QUẢ_____
            var query = (from a in allAccount
                         join t in list
                         on a.Id equals t.AccountId
                         select new ResponseSearchTutorModel
                         {
                             TutorID = t.TutorId,
                             FullName = a.FullName,
                             //Avatar = a.Avatar,
                             HourlyRate = t.HourlyRate,
                             Start = iFeedbackService.TotalStart(t.TutorId),
                             Ratings = iFeedbackService.TotalRate(t.TutorId),
                             Headline = t.Headline,
                             Description = t.Description,
                             TopFeedback = iFeedbackService.GetFeedbacks(t.TutorId).Select(s => s.Description).LastOrDefault(),
                             TitleFeedback = iFeedbackService.GetFeedbacks(t.TutorId).Select(s => s.Title).LastOrDefault(),
                         });

            query = iTutorService.Sorting(query, sortBy, sortType, requestSearchTutorModel.pageIndex);

            return Ok(query);
        }

        [HttpGet("Id/{id}")]
        public IActionResult GetTutorDetail(string id)
        {
            // Lấy thông tin tutor dựa trên id
            var tbTutor = iTutorService.GetTutors().FirstOrDefault(s => s.TutorId == id);
            if (tbTutor == null)
            {
                return NotFound("Tutor not found.");
            }

            // Lấy thông tin tài khoản dựa trên AccountId từ tutor
            var tbAccount = iAccountService.GetAccounts().FirstOrDefault(a => a.Id == tbTutor.AccountId);
            if (tbAccount == null)
            {
                return NotFound("Account not found.");
            }

            // Lấy danh sách SubjectTutor dựa trên TutorId
            var subjectTutors = iSubjectTutorService.GetSubjectTutors(tbTutor.TutorId)
                                    .ToList();

            // Lấy danh sách SubjectId từ SubjectTutor
            var subjectIds = subjectTutors.Select(st => st.SubjectId).ToList();

            // Lấy danh sách Subject dựa trên SubjectId
            var subjects = iSubjectService.GetSubjects()
                                .Where(s => subjectIds.Contains(s.SubjectId))
                                .ToList();

            // Lấy danh sách Description từ Subject
            var subjectDescriptions = subjects.Select(s => s.Description).ToList();

            // Tạo đối tượng TutorDetail
            var tutorDetail = new TutorDetail
            {
                AccountId = tbAccount.Id,
                TutorId = tbTutor.TutorId,
                Avatar = tbAccount.Avatar,
                Photo = tbTutor.Photo,
                FullName = tbAccount.FullName,
                Gender = tbAccount.Gender,
                Headline = tbTutor.Headline,
                Description = tbTutor.Description,
                SubjectTutors = subjectDescriptions,
                TypeOfDegree = tbTutor.TypeOfDegree,
                Education = tbTutor.Education,
                HourlyRate = tbTutor.HourlyRate,
                Address = tbTutor.Address,
                Start = iFeedbackService.TotalStart(tbTutor.TutorId),
                Ratings = iFeedbackService.TotalRate(tbTutor.TutorId),
            };

            return Ok(tutorDetail);
        }

        [HttpPost("UpdateTutor")]
        public async Task<IActionResult> UpdateTutorAccount(TutorVM tutorVM)
        {
            var accountId = currentUserService.GetUserId().ToString();
            if (accountId == null)
            {
                return BadRequest("Sign Account!!!");
            }
            var result = await iTutorService.UpdateTutor(accountId, tutorVM);
            return Ok(result);
        }

        [HttpGet("GetTutorCurrent")]
        public async Task<IActionResult> GetTutorCurrent()
        {
            var accountId = currentUserService.GetUserId().ToString();
            if (accountId == null)
            {
                return BadRequest("Sign Account!!!");
            }
            var result = await iTutorService.GetTutorCurrent(accountId);
            return Ok(result);
        }


        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTutor(string id, Tutor tutor)
        //{
        //    if (id != tutor.TutorId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tutor).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TutorExists(id))
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

        //// POST: api/Tutors
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Tutor>> PostTutor(Tutor tutor)
        //{
        //    _context.Tutors.Add(tutor);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TutorExists(tutor.TutorId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetTutor", new { id = tutor.TutorId }, tutor);
        //}

        //// DELETE: api/Tutors/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTutor(string id)
        //{
        //    var tutor = await _context.Tutors.FindAsync(id);
        //    if (tutor == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Tutors.Remove(tutor);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TutorExists(string id)
        //{
        //    return _context.Tutors.Any(e => e.TutorId == id);
        //}
    }
}
