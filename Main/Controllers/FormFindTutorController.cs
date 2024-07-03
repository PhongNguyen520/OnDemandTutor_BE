using API.Services;
using BusinessObjects;
using BusinessObjects.Models;
using BusinessObjects.Models.FindFormModel;
using BusinessObjects.Models.TutorModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Services;
using System;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FormFindTutorController : ControllerBase
    {
        private readonly IFindTutorFormService _findTutorFormService;
        private readonly ICurrentUserService _currentUserService;
        private readonly ISubjectService _subjectService;
        private readonly IStudentService _studentService;
        private readonly IAccountService _accountService;
        private readonly ISubjectGroupService _subjectGroupService;
        private readonly ITutorService _tutorService;
        private readonly ITutorApplyService _tutorApplyService;
        private readonly IClassCalenderService _classCalenderService;
        private readonly IClassService _classService;

        public FormFindTutorController(ICurrentUserService currentUserService, IAccountService accountService)
        {
            _findTutorFormService = new FindTutorFormService();
            _currentUserService = currentUserService;
            _subjectService = new SubjectService();
            _studentService = new StudentService();
            _accountService = accountService;
            _subjectGroupService = new SubjectGroupService();
            _tutorService = new TutorService();
            _tutorApplyService = new TutorApplyService();
            _classCalenderService = new ClassCalenderService();
            _classService = new ClassService();

        }

        // MODERATER XEM DANH SÁCH FORM CHX DUYỆT
        [HttpGet("moderator/viewformlist")]
        public IActionResult GetRequestList()
        {
            var forms = _findTutorFormService.GetFindTutorForms()
                                              .Where(s => s.IsActived == null && s.Status == null)
                                              .OrderBy(s => s.CreateDay);
            var students = _studentService.GetStudents();
            // Tạo danh sách các FormVM để trả về
            var query = from form in forms
                        join student in students
                        on form.StudentId equals student.StudentId
                        select new FormFindTutorVM
                        {
                            FormId = form.FormId,
                            CreateDay = form.CreateDay.ToString("dd/MM/yyyy HH:mm:ss"),
                            FullName = _accountService.GetAccounts().Where(s => s.Id == student.AccountId).Select(s => s.FullName).FirstOrDefault(),
                            Avatar = _accountService.GetAccounts().Where(s => s.Id == student.AccountId).Select(s => s.Avatar).FirstOrDefault(),
                            Title = form.Title,
                            DayStart = form.DayStart,
                            DayEnd = form.DayEnd,
                            DayOfWeek = form.DayOfWeek,
                            TimeStart = form.TimeStart,
                            TimeEnd = form.TimeEnd,
                            MinHourlyRate = form.MinHourlyRate,
                            MaxHourlyRate = form.MaxHourlyRate,
                            Description = form.DescribeTutor,
                            TutorGender = form.TutorGender,
                            SubjectId = form.SubjectId,
                            StudentId = student.StudentId,
                        };
            return Ok(query);
        }

        // TUTOR FILTER DANH SÁCH FORM
        [HttpGet("tutor/searchpost")]
        public IActionResult Get([FromQuery] RequestSearchPostModel requestSearchPostModel)
        {
            var sortBy = requestSearchPostModel.SortContent != null ? requestSearchPostModel.SortContent?.sortPostBy.ToString() : null;
            var sortType = requestSearchPostModel.SortContent != null ? requestSearchPostModel.SortContent?.sortPostType.ToString() : null;
            string searchQuery = null;

            if (requestSearchPostModel.Search != null)
            {
                searchQuery = requestSearchPostModel.Search.ToLower();
            }

            //List post active 
            var allPost = _findTutorFormService.Filter(requestSearchPostModel);

            var allStudents = _studentService.GetStudents();
            var allAccounts = _accountService.GetAccounts();


            // TÌM KIẾM THEO TÊN NHÓM MÔN HỌC
            if (searchQuery != null)
            {
                var allSubjectGroup = _subjectGroupService.GetSubjectGroups().Where(su => su.SubjectName.ToLower().Contains(searchQuery));

                if (!allSubjectGroup.Any())
                {
                    allSubjectGroup = null; 
                }
                else
                {
                    allSubjectGroup = _subjectGroupService.GetSubjectGroups();
                }

                IEnumerable<Subject> allSubject = _subjectService.GetSubjects();

                // Trường hợp chọn Grade
                if (!string.IsNullOrEmpty(requestSearchPostModel.GradeId))
                {
                    allSubject = allSubject.Where(s => s.GradeId == requestSearchPostModel.GradeId);
                }// kết thúc


                // Lấy danh sách môn học
                IEnumerable<Subject> subjects = from sg in allSubjectGroup
                                                join s in allSubject
                                                on sg.SubjectGroupId equals s.SubjectGroupId
                                                select s;

                //Lấy danh sách form yêu cầu môn học tìm kiếm
                allPost = from p in allPost
                          join s in subjects
                          on p.SubjectId equals s.SubjectId
                          select p;

                allPost = allPost.Distinct();

            }
            //_____TẠO DANH SÁCH KẾT QUẢ_____
            var query = from post in allPost
                        join student in allStudents
                        on post.StudentId equals student.StudentId
                        join account in allAccounts
                        on student.AccountId equals account.Id
                        select new FormFindTutorVM
                        {
                            FormId = post.FormId,
                            CreateDay = post.CreateDay.ToString("dd/MM/yyyy HH:mm:ss"),
                            FullName = account.FullName,
                            Avatar = account.Avatar,
                            Title = post.Title,
                            DayStart = post.DayStart,
                            DayEnd = post.DayEnd,
                            DayOfWeek = post.DayOfWeek,
                            TimeStart = post.TimeStart,
                            TimeEnd = post.TimeEnd,
                            MinHourlyRate = post.MinHourlyRate,
                            MaxHourlyRate = post.MaxHourlyRate,
                            Description = post.DescribeTutor,
                            TutorGender = post.TutorGender,
                            SubjectId = post.SubjectId,
                            StudentId = post.StudentId,
                        };


            query = _findTutorFormService.Sorting(query, sortBy, sortType, requestSearchPostModel.pageIndex);

            return Ok(query);
        }

        // STUDENT XEM DANH SÁCH FORM ĐÃ ĐĂNG KÍ
        [HttpGet("student/viewformlist")]
        // Show Student's post list
        public IActionResult GetList()
        {
            var userId = _currentUserService.GetUserId().ToString();

            var user = _accountService.GetAccounts().FirstOrDefault(s => s.Id == userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var student = _studentService.GetStudents().FirstOrDefault(s => s.AccountId == userId);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            var forms = _findTutorFormService.GetFindTutorForms().Where(s => s.StudentId == student.StudentId && s.IsActived == null || s.IsActived == true);

            // Tạo danh sách các FormVM để trả về
            var query = from post in forms
                        select new FormFindTutorVM
                        {
                            FormId = post.FormId,
                            CreateDay = post.CreateDay.ToString("dd/MM/yyyy HH:mm:ss"),
                            FullName = user.FullName,
                            Title = post.Title,
                            DayStart = post.DayStart,
                            DayEnd = post.DayEnd,
                            DayOfWeek = post.DayOfWeek,
                            TimeStart = post.TimeStart,
                            TimeEnd = post.TimeEnd,
                            MinHourlyRate = post.MinHourlyRate,
                            MaxHourlyRate = post.MaxHourlyRate,
                            Description = post.DescribeTutor,
                            TutorGender = post.TutorGender,
                            SubjectId = post.SubjectId,
                            StudentId = student.StudentId,
                        };

            return Ok(query);
        }

        // STUDENT TẠO FORM
        [HttpPost("student/createform")]
        public IActionResult CreateForm(RequestCreateFormFindTutor form)
        {
            if (form == null)
            {
                return BadRequest("Form data is required.");
            }

            var userId = _currentUserService.GetUserId().ToString();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not logged in.");
            }

            var stId = _studentService.GetStudents().FirstOrDefault(s => s.AccountId == userId);

            var subject = _subjectService.GetSubjects()
                .FirstOrDefault(s => s.SubjectGroupId == form.SubjectGroupId && s.GradeId == form.GradeId);
            if (subject == null)
            {
                return NotFound("Subject not found for the given SubjectGroupId and GradeId.");
            }

            var result = new FindTutorForm
            {
                FormId = Guid.NewGuid().ToString(),
                CreateDay = DateTime.Now,
                MaxHourlyRate = form.MaxHourlyRate,
                MinHourlyRate = form.MinHourlyRate,
                TutorGender = form.TutorGender,
                TypeOfDegree = form.TypeOfDegree,
                DayStart = form.DayStart,
                DayEnd = form.DayEnd,
                DayOfWeek = form.DayOfWeek,
                TimeStart = form.TimeStart,
                TimeEnd = form.TimeEnd,
                Title = form.Tittle,
                DescribeTutor = form.DescribeTutor,
                Status = null,
                IsActived = null,
                StudentId = stId.StudentId,
                SubjectId = subject.SubjectId,
            };

            try
            {
                _findTutorFormService.AddFindTutorForm(result);
                return Ok(new { message = "Form created successfully", formId = result.FormId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the form.");
            }
        }

        // TUTOR APPLY POST
        [HttpPost("tutor/applypost")]
        public IActionResult TutorApply([FromBody] string formId)
        {
            var userId = _currentUserService.GetUserId().ToString();
            var tutor = _tutorService.GetTutors().Where(s => s.AccountId == userId).FirstOrDefault();

            var form = _findTutorFormService.GetFindTutorForms().Where(s => s.FormId == formId).FirstOrDefault();

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

            var result = new TutorApply()
            {
                TutorId = tutor.TutorId,
                FormId = form.FormId,
                DayApply = DateTime.Now,
                IsApprove = null,
            };

            _tutorApplyService.AddTutorApply(result);

            return Ok(result);
        }

        // MODERATER DUYỆT FORM
        [HttpPut("moderator/browserform")]
        public IActionResult BrowserForm(string id, bool action)
        {
            var form = _findTutorFormService.GetFindTutorForms().FirstOrDefault(s => s.FormId == id);
            if (form == null)
            {
                return NotFound();
            }

            form.Status = action;

            return Ok(_findTutorFormService.UpdateFindTutorForms(form));
        }

        // STUDENT UPDATE FORM
        [HttpPut("student/updateform")]
        public IActionResult UpdateForm(FindTutorForm form)
        {
            if (form == null)
            {
                return BadRequest("Form data is null");
            }

            var updatedForm = _findTutorFormService.UpdateFindTutorForms(form);

            if (updatedForm == null)
            {
                return NotFound("Form not found");
            }

            return Ok(updatedForm);
        }

        [HttpGet("student/viewApplyList")]
        public IActionResult ViewApplyList(string formId)
        {
            var accounts = _accountService.GetAccounts();
            var result = from form in _tutorApplyService.GetTutorApplies().Where(s => s.FormId == formId)
                         join tutor in _tutorService.GetTutors()
                         on form.TutorId equals tutor.TutorId
                         join account in accounts
                         on tutor.AccountId equals account.Id
                         select new TutorApplyVM()
                         {
                             TutorId = form.TutorId,
                             TutorName = account.FullName,
                             TutorAvatar = account.Avatar,
                             DayApply = form.DayApply,
                         };

            return Ok(result);
        }

        // STUDENT DUYỆT TUTOR
        [HttpPut("student/browsertutor")]
        public IActionResult SubmitForm(string formId, string tutorId)
        {
            var tutorApply = _tutorApplyService.GetTutorApplies();
            var form = _findTutorFormService.GetFindTutorForms().FirstOrDefault(s => s.FormId == formId);
            
            foreach ( var tutor in tutorApply)
            {
                if (tutor.TutorId != tutorId)
                {
                    tutor.IsApprove = false;
                } else
                {
                    tutor.IsApprove = true;
                }
                _tutorApplyService.UpdateTutorApplies(tutor);
            }
            form.IsActived = true;
            _findTutorFormService.UpdateFindTutorForms(form);

            return Ok();
        }

        // STUDENT DELETE FORM
        [HttpDelete("student/deleteform")]
        public IActionResult DeleteForm(string id)
        {
            var form = _findTutorFormService.GetFindTutorForms().FirstOrDefault(s => s.FormId == id);
            if (form == null)
            {
                return NotFound();
            }
            form.IsActived = false;

            return Ok(_findTutorFormService.UpdateFindTutorForms(form));
        }

    }
}
