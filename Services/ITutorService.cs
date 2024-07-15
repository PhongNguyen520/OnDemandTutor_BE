using BusinessObjects;
using BusinessObjects.Models;
using BusinessObjects.Models.TutorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ITutorService
    {
        public bool AddTutor(Tutor tutor);

        public bool DelTutors(int id);

        public List<Tutor> GetTutors();

        public bool UpdateTutors(Tutor tutor);


        public IEnumerable<Tutor> Filter(RequestSearchTutorModel requestSearchTutorModel);

        public IEnumerable<ResponseSearchTutorModel> Sorting
            (IEnumerable<ResponseSearchTutorModel> query,
            string? sortBy,
            string? sortType);
        Task<TutorVM> UpdateTutor(string idAccount, TutorVM tutorVM);
        Task<TutorVM> GetTutorCurrent(string idAccount);
        Task<bool> ChangeStatusTutor(IsActiveTutor model);

        Task<DashBoardTutor> NumberOfClasses(string id, DateTime createDay);

        Task<DashBoardTutor> NumberOfHour(string id, DateTime createDay);

        Task<DashBoardTutor> NumberOfClassesIsCancel(string id, DateTime createDay);
    }
}
