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
        [HttpGet("moderator/findtutorform")]
        public IActionResult GetRequestList()
        {
            return Ok(_findTutorFormService.GetFindTutorForms().Where(s => s.IsActive == false && s.Status == false));
        }

        // TUTOR FILTER DANH SÁCH FORM
        [HttpGet("tutor/searchpost")]
        public IActionResult Get(RequestSearchPostModel requestSearchPostModel)
        {
            var sortBy = requestSearchPostModel.SortContent != null ? requestSearchPostModel.SortContent?.sortTutorBy.ToString() : null;
            var sortType = requestSearchPostModel.SortContent != null ? requestSearchPostModel.SortContent?.sortTutorType.ToString() : null;
            var searchQuery = requestSearchPostModel.Search.ToLower();

            //List post active 
            var allPost = _findTutorFormService.GetFindTutorForms().Where(s => s.IsActive == true && s.Status == false);

            var allStudents = _studentService.GetStudents();
            var allAccounts = _accountService.GetAccounts();


            // TÌM KIẾM THEO TÊN NHÓM MÔN HỌC
            if (searchQuery != null)
            {
                var allSubjectGroup = _subjectGroupService.GetSubjectGroups().Where(su => su.SubjectName.ToLower().Contains(searchQuery));

                if (!allSubjectGroup.Any())
                {
                    allSubjectGroup = _subjectGroupService.GetSubjectGroups();
                }
                else
                {
                    allSubjectGroup = null;
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
                            Tittle = post.Tittle,
                            MinHourlyRate = post.MinHourlyRate,
                            MaxHourlyRate = post.MaxHourlyRate,
                            SubjectName = post.SubjectName,
                            Description = post.DescribeTutor,
                            TutorGender = post.TutorGender,
                            SubjectId = post.SubjectId,
                            StudentId = post.StudentId,
                        };


            //query = iTutorService.Sorting(query, sortBy, sortType, requestSearchPostModel.pageIndex);

            return Ok(query);
        }

        // STUDENT XEM DANH SÁCH FORM ĐÃ ĐĂNG KÍ
        [HttpGet("student/form/")]
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
            var forms = _findTutorFormService.GetFindTutorForms().Where(s => s.StudentId == student.StudentId);

            // Tạo danh sách các FormVM để trả về
            var query = from post in forms
                        select new FormVM
                        {
                            FormId = post.FormId,
                            CreateDay = post.CreateDay,
                            FullName = user.FullName,
                            Tittle = post.Tittle,
                            MinHourlyRate = post.MinHourlyRate,
                            MaxHourlyRate = post.MaxHourlyRate,
                            SubjectName = post.SubjectName,
                            Description = post.DescribeTutor,
                            TutorGender = post.TutorGender,
                            SubjectId = post.SubjectId,
                            StudentId = student.StudentId,
                        };

            return Ok(query);
        }


        // STUDENT TẠO FORM
        [HttpPost("student/createform/")]
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
                Tittle = form.Tittle,
                DescribeTutor = form.DescribeTutor,
                Status = false,
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
        [HttpPut("moderator/browserform/{id}")]
        public IActionResult BrowserForm(string id, bool action)
        {
            FindTutorForm form = (FindTutorForm)_findTutorFormService.GetFindTutorForms().Where(s => s.FormId == id);
            if (action)
            {
                form.IsActive = true;
            }
            else
            {
                form.IsActive = false;
            }

            return Ok(_findTutorFormService.UpdateFindTutorForms(form));
        }


        // STUDENT UPDATE FORM
        [HttpPut("student/updateform/{id}")]
        public IActionResult UpdateForm(string id)
        {
            var result = new FindTutorForm
            {
                //MaxHourlyRate = form.MaxHourlyRate,
                //MinHourlyRate = form.MinHourlyRate,
                //TutorGender = form.TutorGender,
                //TypeOfDegree = form.TypeOfDegree,
                //Tittle = form.Tittle,
                //DescribeTutor = form.DescribeTutor,
                //SubjectId = subject.SubjectId,
            };
            return Ok();
        }

        // STUDENT XÁC NHẬN ĐÃ TÌM ĐC TUTOR
        [HttpPut("student/submitform/{id}")]
        public IActionResult SubmitForm(string id)
        {
            FindTutorForm form = (FindTutorForm)_findTutorFormService.GetFindTutorForms().Where(s => s.FormId == id);

            form.Status = true;

            return Ok(_findTutorFormService.UpdateFindTutorForms(form));
        }

    }
}
