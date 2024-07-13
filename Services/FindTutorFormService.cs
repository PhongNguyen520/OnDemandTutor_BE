using BusinessObjects;
using BusinessObjects.Models.FindFormModel;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FindTutorFormService : IFindTutorFormService
    {
        private readonly IFindTutorFormRepository _findTutorFormRepository = null;
        private readonly IClassCalenderRepository _classCalenderRepository = new ClassCalenderRepository();
        private readonly ISubjectRepository _subjectRepository = new SubjectRepository();

        public FindTutorFormService()
        {
            if (_findTutorFormRepository == null)
            {
                _findTutorFormRepository = new FindTutorFormRepository();
            }
        }
        public bool AddFindTutorForm(FindTutorForm form)
        {
            return _findTutorFormRepository.AddFindTutorForm(form);
        }

        public bool DelFindTutorForms(int id)
        {
            return _findTutorFormRepository.DelFindTutorForms(id);
        }

        public List<FindTutorForm> GetFindTutorForms()
        {
            return _findTutorFormRepository.GetFindTutorForms();
        }

        public bool UpdateFindTutorForms(FindTutorForm form)
        {
            return _findTutorFormRepository.UpdateFindTutorForms(form);
        }

        public IEnumerable<FindTutorForm> Filter(RequestSearchPostModel requestSearchPostModel)
        {
            return _findTutorFormRepository.Filter(requestSearchPostModel);
        }

        public IEnumerable<FormFindTutorVM> Sorting(IEnumerable<FormFindTutorVM> query, string? sortBy, string? sortType)
        {
            return _findTutorFormRepository.Sorting(query, sortBy, sortType);
        }

        public IEnumerable<FormFindTutorVM> GetFormList(IEnumerable<FindTutorForm> allPosts, IEnumerable<Student> allStudents)
        {
            var query = from post in allPosts
                        join student in allStudents
                        on post.StudentId equals student.StudentId
                        select new FormFindTutorVM
                        {
                            FormId = post.FormId,
                            CreateDay = post.CreateDay.ToString("yyyy-MM-dd HH:mm"),
                            FullName = student.Account.FullName,
                            Avatar = student.Account.Avatar,
                            Title = post.Title,
                            DayStart = post.DayStart.ToString("yyyy-MM-dd"),
                            DayEnd = post.DayEnd.ToString("yyyy-MM-dd"),
                            DayOfWeek = _classCalenderRepository.ConvertToDaysOfWeeks(post.DayOfWeek),
                            TimeStart = post.TimeStart,
                            TimeEnd = post.TimeEnd,
                            MinHourlyRate = post.MinHourlyRate,
                            MaxHourlyRate = post.MaxHourlyRate,
                            Description = post.DescribeTutor,
                            SubjectName = post.Subject.Description,
                            TutorGender = post.TutorGender,
                            TypeOfDegree = post.TypeOfDegree,
                            Status = post.Status,
                            IsActived = post.IsActived,
                            GradeId = post.Subject.GradeId,
                            SubjectGroupId = post.Subject.SubjectGroupId,
                            SubjectId = post.SubjectId,
                            StudentId = post.StudentId,
                            UserIdStudent = post.Student.AccountId,
                        };

            return query;
        }
    }
}
