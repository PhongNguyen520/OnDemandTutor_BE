using BusinessObjects.Models.TutorModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Threading.Tasks.Dataflow;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemHandlerController : ControllerBase
    {
        private readonly ITutorService _tutorService;
        private readonly IClassService _classService;
        private readonly IClassCalenderService _classCalenderService;
        private readonly IAccountService _accountService;
        private readonly ISubjectTutorService _subjectTutorService;
        private readonly ISubjectService _subjectService;

        public SystemHandlerController(IAccountService accountService, IClassService classService)
        {
            _tutorService = new TutorService();
            _classService = classService;
            _classCalenderService = new ClassCalenderService();
            _accountService = accountService;
            _subjectTutorService = new SubjectTutorService();
            _subjectService = new SubjectService();
        }

        [HttpGet("top10Tutor")]
        public IActionResult GetTopTutor()
        {
            var tutors = _tutorService.GetTutors();
            var classList = _classService.GetClasses().Where(s => s.DayStart.Month <= DateTime.Now.Month - 1 && s.DayEnd.Month >= DateTime.Now.Month - 1);
            var result = new List<TopTutorVM>();

            foreach (var tutor in tutors)
            {
                var item = new TopTutorVM()
                {
                    TutorId = tutor.TutorId,
                    FullName = _accountService.GetAccounts().Where(s => s.Id == tutor.AccountId).Select(s => s.FullName).FirstOrDefault(),
                    Avatar = _accountService.GetAccounts().Where(s => s.Id == tutor.AccountId).Select(s => s.Avatar).FirstOrDefault(),
                    Headline = tutor.Headline,
                    SubjectTutors = (from st in _subjectTutorService.GetSubjectTutors(tutor.TutorId)
                                     join s in _subjectService.GetSubjects()
                                     on st.SubjectId equals s.SubjectId
                                     select s.Description).ToList(),
                    Description = tutor.Description,
                    TotalHour = _classCalenderService.TotalHourByMonth(classList, tutor.TutorId),
                };
                result.Add(item);
            }
            result = result.AsEnumerable().OrderByDescending(s => s.TotalHour).Take(10).ToList();

            return Ok(result);
        }
    }
}
