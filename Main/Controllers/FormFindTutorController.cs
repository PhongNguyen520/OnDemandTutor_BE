using API.Services;
using BusinessObjects;
using BusinessObjects.Models.FormModel;
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

        public FormFindTutorController(ICurrentUserService currentUserService, IAccountService accountService)
        {
            _findTutorFormService = new FindTutorFormService();
            _currentUserService = currentUserService;
            _subjectService = new SubjectService();
            _studentService = new StudentService();
            _accountService = accountService;
            _subjectGroupService = new SubjectGroupService();
        }

        // MODERATER XEM DANH SÁCH FORM CHX DUYỆT
        [HttpGet("moderator/viewformlist")]
        public IActionResult GetRequestList()
        {
            var result = _findTutorFormService.GetFindTutorForms()
                                              .Where(s => s.IsActived == null && s.Status == null)
                                              .OrderBy(s => s.CreateDay);
            return Ok(result);
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
                        select new FormVM
                        {
                            FormId = post.FormId,
                            CreateDay = post.CreateDay,
                            FullName = account.FullName,
                            Title = post.Title,
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
            // Lấy userId từ _currentUserService
            var userId = _currentUserService.GetUserId().ToString();

            // Tìm thông tin người dùng từ bảng tài khoản
            var user = _accountService.GetAccounts().FirstOrDefault(s => s.Id == userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Tìm thông tin học sinh từ bảng học sinh bằng AccountId
            var student = _studentService.GetStudents().FirstOrDefault(s => s.AccountId == userId);
            if (student == null)
            {
                return NotFound("Student not found");
            }

            // Lấy danh sách các form tìm gia sư của học sinh
            var forms = _findTutorFormService.GetFindTutorForms().Where(s => s.StudentId == student.StudentId && s.IsActived == null || s.IsActived == true);

            // Tạo danh sách các FormVM để trả về
            var query = from post in forms
                        select new FormVM
                        {
                            FormId = post.FormId,
                            CreateDay = post.CreateDay,
                            FullName = user.FullName,
                            Title = post.Title,
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
        public IActionResult CreateForm(RequestCreateForm form)
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

        // STUDENT XÁC NHẬN ĐÃ TÌM ĐC TUTOR
        [HttpPut("student/submitform")]
        public IActionResult SubmitForm(string id)
        {
            var form = _findTutorFormService.GetFindTutorForms().FirstOrDefault(s => s.FormId == id);
            if (form == null)
            {
                return NotFound();
            }
            form.IsActived = true;

            return Ok(_findTutorFormService.UpdateFindTutorForms(form));
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
