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
using System.Diagnostics;
using System.Linq;

namespace API.Controllers
{
    [Route("api/formfindtutor")]
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
        private readonly IPagingListService<FormFindTutorVM> _pagingListService;
        private readonly IRequestTutorFormService _requestTutorFormService;

        public FormFindTutorController(ICurrentUserService currentUserService, IAccountService accountService, IClassService classService)
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
            _classService = classService;
            _pagingListService = new PagingListService<FormFindTutorVM>();
            _requestTutorFormService = new RequestTutorFormService();
        }

        // MODERATER XEM DANH SÁCH FORM CHX DUYỆT
        [HttpGet("moderator_getforms")]
        public IActionResult GetRequestList(int pageIndex)
        {
            PagingResult<FormFindTutorVM> result = new PagingResult<FormFindTutorVM>();

            var forms = _findTutorFormService.GetFindTutorForms()
                                              .Where(s => s.IsActived == null && s.Status == null);
            if (!forms.Any())
            {
                return Ok(result);
            }

            var students = _studentService.GetStudents();
            // Tạo danh sách các FormVM để trả về
            var query = _findTutorFormService.GetFormList(forms, students).OrderBy(s => s.CreateDay);
            result = _pagingListService.Paging(query.ToList(), pageIndex, 7);

            return Ok(result);
        }

        // TUTOR FILTER DANH SÁCH FORM
        [HttpGet("search_post")]
        public IActionResult Get([FromQuery] RequestSearchPostModel requestSearchPostModel)
        {
            var sortBy = requestSearchPostModel.SortContent != null ? requestSearchPostModel.SortContent?.sortPostBy.ToString() : null;
            var sortType = requestSearchPostModel.SortContent != null ? requestSearchPostModel.SortContent?.sortPostType.ToString() : null;
            string searchQuery = "";

            if (requestSearchPostModel.Search != null)
            {
                searchQuery = requestSearchPostModel.Search.ToLower();
            }

            PagingResult<FormFindTutorVM> result = new PagingResult<FormFindTutorVM>();

            //List post active 
            var allPost = _findTutorFormService.Filter(requestSearchPostModel);

            if (!allPost.Any())
            {
                return Ok(result);
            }

            var allStudents = _studentService.GetStudents();


            // TÌM KIẾM THEO TÊN NHÓM MÔN HỌC
            if (searchQuery != null)
            {
                var allSubjectGroup = _subjectGroupService.GetSubjectGroups().Where(su => su.SubjectName.ToLower().Contains(searchQuery));

                if (!allSubjectGroup.Any())
                {
                    return Ok(result);
                }

                var allSubject = _subjectService.GetSubjects();

                // Trường hợp chọn Grade
                if (!string.IsNullOrEmpty(requestSearchPostModel.GradeId))
                {
                    allSubject = allSubject.Where(s => s.GradeId == requestSearchPostModel.GradeId).ToList();
                }// kết thúc


                // Lấy danh sách môn học
                var subjects = from sg in allSubjectGroup
                               join s in allSubject
                               on sg.SubjectGroupId equals s.SubjectGroupId
                               select s;

                //Lấy danh sách form yêu cầu môn học tìm kiếm
                allPost = from p in allPost
                          join s in subjects
                          on p.SubjectId equals s.SubjectId
                          select p;

                if (!allSubjectGroup.Any())
                {
                    return Ok(result);
                }

                allPost = allPost.Distinct();

            }
            //_____TẠO DANH SÁCH KẾT QUẢ_____
            var query = _findTutorFormService.GetFormList(allPost, allStudents);

            query = _findTutorFormService.Sorting(query, sortBy, sortType);

            result = _pagingListService.Paging(query.ToList(), requestSearchPostModel.pageIndex, 7);

            return Ok(result);
        }

        // STUDENT XEM DANH SÁCH FORM ĐÃ ĐĂNG KÍ
        [HttpGet("student_getforms")]
        public IActionResult GetList(bool? status, bool? isActive, int pageIndex)
        {
            var user = _currentUserService.GetUserId().ToString();
            PagingResult<FormFindTutorVM> result = new PagingResult<FormFindTutorVM>();

            if (user == null)
            {
                return NotFound("User not found");
            }


            var student = _studentService.GetStudents().Where(s => s.AccountId == user);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            var forms = _findTutorFormService.GetFindTutorForms().Where(s => s.StudentId == student.First().StudentId
                                                                            && s.IsActived == isActive
                                                                            && s.Status == status);

            if (!forms.Any())
            {
                return Ok(result);
            }

            // Tạo danh sách các FormVM để trả về
            var query = _findTutorFormService.GetFormList(forms, student).OrderByDescending(s => s.CreateDay);

            result = _pagingListService.Paging(query.ToList(), pageIndex, 7);

            return Ok(result);
        }

        [HttpGet("tutor_getforms")]
        public IActionResult GetApplyForm(bool? isApprove, int pageIndex)
        {
            var userId = _currentUserService.GetUserId().ToString();

            PagingResult<FormFindTutorVM> result = new PagingResult<FormFindTutorVM>();

            var user = _accountService.GetAccounts().First(s => s.Id == userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var tutor = _tutorService.GetTutors().First(s => s.AccountId == userId);
            if (tutor == null)
            {
                return NotFound("Tutor not found");
            }
            var allStudents = _studentService.GetStudents();
            var formApply = _tutorApplyService.GetTutorApplies().Where(s => s.TutorId == tutor.TutorId && s.IsApprove == isApprove).ToList();

            var forms = from form in _findTutorFormService.GetFindTutorForms()
                        join tutorApply in formApply
                        on form.FormId equals tutorApply.FormId
                        select form;

            if (!forms.Any())
            {
                return Ok(result);
            }

            // Tạo danh sách các FormVM để trả về
            var query = _findTutorFormService.GetFormList(forms, allStudents).OrderByDescending(s => s.CreateDay);

            result = _pagingListService.Paging(query.ToList(), pageIndex, 7);

            return Ok(result);
        }

        // STUDENT TẠO FORM
        [HttpPost("create_form")]
        public IActionResult CreateForm(RequestCreateFormFindTutor form)
        {
            if (form == null)
            {
                return Ok("Form data is required.");
            }

            var userId = _currentUserService.GetUserId().ToString();
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User is not logged in.");
            }

            var stId = _studentService.GetStudents().First(s => s.AccountId == userId);

            var subject = _subjectService.GetSubjects()
                .First(s => s.SubjectGroupId == form.SubjectGroupId && s.GradeId == form.GradeId);
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
                //RejectReason = null,
            };

            try
            {
                _findTutorFormService.AddFindTutorForm(result);
                return Ok(new { message = "Form created successfully", formId = result.FormId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the form." + ex);
            }
        }

        // TUTOR APPLY POST
        [HttpPost("tutor_applypost")]
        public async Task<IActionResult> TutorApply([FromBody] string formId)
        {
            var userId = _currentUserService.GetUserId().ToString();
            var tutor = _tutorService.GetTutors().Where(s => s.AccountId == userId).First();

            var form = _findTutorFormService.GetFindTutorForms().Where(s => s.FormId == formId).First();

            if (_tutorApplyService.GetTutorApplies().Where(s => s.TutorId == tutor.TutorId && s.FormId == formId).Any())
            {
                return Ok("You applied this post before");
            }

            //Handle To Avoid Conflict With Tutor Calender
            var checkTutorCalendar = await _classCalenderService.HandleAvoidConflictCalendar(form.DayOfWeek, 
                                                                                       form.DayStart, 
                                                                                       form.DayEnd, 
                                                                                       form.TimeStart, 
                                                                                       form.TimeEnd, 
                                                                                       tutor.TutorId,
                                                                                       1);

            if (checkTutorCalendar == false)
            {
                return Ok("The registration schedule coincides with the your teaching schedule!");
            }// end

            Form formContainer = new Form()
            {
                FormId = formId,
                DayOfWeek = form.DayOfWeek,
                DayStart = form.DayStart,
                DayEnd = form.DayEnd,
                TimeStart = form.TimeStart,
                TimeEnd = form.TimeEnd,
            };

            //Handle To Avoid Conflict With Form
            //1. FormFindTutor
            var tutorApply = _tutorApplyService.GetTutorApplies().Where(s => s.TutorId == tutor.TutorId);

            if (tutorApply.Any())
            {
                checkTutorCalendar = await _classCalenderService.HandleAvoidConflictFormFind(tutorApply, formContainer);
                if (checkTutorCalendar == false)
                {
                    return Ok("The registration schedule coincides with the registration schedule of the other post you have applied for!");
                }
            }// end

            //2. FormRequestTutor
            var formRequestList = _requestTutorFormService.GetRequestTutorForms().Where(s => s.Status == null && s.IsActive == null && s.TutorId == tutor.TutorId);

            if (formRequestList.Any())
            {
                checkTutorCalendar = await _classCalenderService.HandleAvoidConflictFormRequest(formRequestList, formContainer);
                if (checkTutorCalendar == false)
                {
                    return Ok("You have a registration request to create a class that coincides with this class registration schedule!");
                }
            }// end

            var result = new TutorApply()
            {
                TutorId = tutor.TutorId,
                FormId = form.FormId,
                DayApply = DateTime.Now,
                IsApprove = null,
                IsActived = null,
            };

            _tutorApplyService.AddTutorApply(result);

            return Ok(result);
        }

        // MODERATER DUYỆT FORM
        [HttpPut("moderator_browserform")]
        public IActionResult BrowserForm([FromBody] List<string> id, bool action, string? rejectReason)
        {
            var forms = _findTutorFormService.GetFindTutorForms().Where(s => s.Status == null && s.IsActived == null);
            if (forms == null)
            {
                return NotFound();
            }
            foreach (var form in forms)
            {
                if (id.Contains(form.FormId))
                {
                    if (action == true)
                    {
                        form.Status = action;
                        _findTutorFormService.UpdateFindTutorForms(form);
                    }
                    else
                    {
                        form.RejectReason = rejectReason;
                        form.Status = action;
                        _findTutorFormService.UpdateFindTutorForms(form);
                    }
                }
            }

            return Ok();
        }

        // STUDENT UPDATE FORM
        [HttpPut("student_updateform")]
        public IActionResult UpdateForm(UpdateFormVM form)
        {
            if (form == null)
            {
                return BadRequest("Form data is null");
            }

            var updatedForm = _findTutorFormService.GetFindTutorForms().Where(s => s.FormId == form.FormId).FirstOrDefault();

            if (updatedForm == null)
            {
                return BadRequest("Form not found");
            }
            else if (updatedForm.Status == true)
            {
                return BadRequest("Form was browsered, you can't edit!");
            }

            updatedForm.Title = form.Title;
            updatedForm.TutorGender = form.TutorGender;
            updatedForm.DescribeTutor = form.Description;
            updatedForm.DayStart = form.DayStart;
            updatedForm.DayEnd = form.DayEnd;
            updatedForm.TimeStart = form.TimeStart;
            updatedForm.TimeEnd = form.TimeEnd;
            updatedForm.DayOfWeek = form.DayOfWeek;
            updatedForm.MinHourlyRate = form.MinHourlyRate;
            updatedForm.MaxHourlyRate = form.MaxHourlyRate;
            updatedForm.SubjectId = _subjectService.GetSubjects()
                .Where(s => s.GradeId == form.GradeId && s.SubjectGroupId == form.SubjectGroupId)
                .Select(s => s.SubjectId).First();

            _findTutorFormService.UpdateFindTutorForms(updatedForm);

            return Ok(updatedForm);
        }

        // STUDENT VIEW APPLY LIST
        [HttpGet("student_get-applylist")]
        public IActionResult ViewApplyList(string formId)
        {
            var forms = _tutorApplyService.GetTutorApplies().Where(s => s.FormId == formId && s.IsActived != false);

            if (!forms.Any())
            {
                return Ok(forms);
            }

            var accounts = _accountService.GetAccounts();
            var tutors = _tutorService.GetTutors();

            var result = from form in forms
                         join tutor in tutors
                         on form.TutorId equals tutor.TutorId
                         join account in accounts
                         on tutor.AccountId equals account.Id
                         select new TutorApplyVM()
                         {
                             TutorId = form.TutorId,
                             TutorName = account.FullName,
                             TutorAvatar = account.Avatar,
                             DayApply = form.DayApply.ToString("yyyy-MM-dd HH:mm"),
                             UserIdTutor = account.Id,
                         };

            return Ok(result.OrderByDescending(s => s.DayApply));
        }

        // STUDENT DUYỆT TUTOR
        [HttpPut("student_browsertutor")]
        public IActionResult SubmitForm(bool? action, string formId, string tutorId)
        {
            var tutorApply = _tutorApplyService.GetTutorApplies().Where(s => s.FormId == formId);
            var form = _findTutorFormService.GetFindTutorForms().First(s => s.FormId == formId);

            if (action == false)
            {
                var tutor = tutorApply.First(s => s.FormId == formId && s.TutorId == tutorId);
                tutor.IsApprove = false;
                _tutorApplyService.UpdateTutorApplies(tutor);
                return Ok(tutor);
            }

            foreach (var tutor in tutorApply)
            {
                if (tutor.TutorId != tutorId)
                {
                    tutor.IsApprove = false;
                }
                else
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
        [HttpDelete("delete_form")]
        public IActionResult DeleteForm(string id)
        {
            var form = _findTutorFormService.GetFindTutorForms().First(s => s.FormId == id);
            if (form == null)
            {
                return NotFound();
            }
            form.IsActived = false;

            return Ok(_findTutorFormService.UpdateFindTutorForms(form));
        }

    }
}
