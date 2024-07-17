using API.Services;
using BusinessObjects.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace API.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _iStudentService;
        private readonly ICurrentUserService _iCurrentUserService;

        public StudentsController(IStudentService iStudentService, ICurrentUserService iCurrentUserService)
        {
            _iStudentService = iStudentService;
            _iCurrentUserService = iCurrentUserService;
        }

        [HttpPost("update_student")]
        public async Task<IActionResult> UpdateStudentAccount(StudentVM studentVM)
        {
            var accountId = _iCurrentUserService.GetUserId().ToString();
            if (accountId == null)
            {
                return BadRequest("Sign Account!!!");
            }
            var result = await _iStudentService.UpdateStudent(accountId, studentVM);
            return Ok(result);
        }

        [HttpGet("get_student-current")]
        public async Task<IActionResult> GetTutorCurrent()
        {
            var accountId = _iCurrentUserService.GetUserId().ToString();
            if (accountId == null)
            {
                return BadRequest("Sign Account!!!");
            }
            var result = await _iStudentService.GetStudentCurrent(accountId);
            return Ok(result);
        }

        [HttpGet("get_subject-name")]
        public async Task<IActionResult> ListSubjectName(string nameFind)
        {
            var rusult = await _iStudentService.ListNameSupsectGroup(nameFind);
            return Ok(rusult);
        }
    }
}
