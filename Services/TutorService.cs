using BusinessObjects;
using BusinessObjects.Models;
using BusinessObjects.Models.TutorModel;
using Microsoft.AspNet.SignalR.Tracing;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TutorService : ITutorService
    {
        private readonly ITutorRepository _repository;
        private readonly IClassRepository _classRepository = new ClassRepository();
        private readonly IClassCalenderRepository _calenderRepository = new ClassCalenderRepository();

        public TutorService()
        {
            _repository = new TutorRepository();
        }

        //------------------------
        public TutorService(ITutorRepository repository)
        {
            _repository = repository;
        }

        public bool AddTutor(Tutor tutor)
        {
            return _repository.AddTutor(tutor);
        }

        public bool DelTutors(int id)
        {
            return _repository.DelTutors(id);
        }

        public IEnumerable<Tutor> Filter(RequestSearchTutorModel requestSearchTutorModel)
        {
            return _repository.Filter(requestSearchTutorModel);
        }


        public List<Tutor> GetTutors()
        {
            return _repository.GetTutors();
        }

        public IEnumerable<ResponseSearchTutorModel> Sorting(IEnumerable<ResponseSearchTutorModel> query, string? sortBy, string? sortType)
        {
            return _repository.Sorting(query, sortBy, sortType);
        }


        public bool UpdateTutors(Tutor tutor)
        {
            throw new NotImplementedException();
        }

        public async Task<TutorVM> UpdateTutor(string idAccount, TutorVM tutorVM)
        {
            return await _repository.UpdateTutor(idAccount, tutorVM);
        }

        public async Task<TutorVM> GetTutorCurrent(string idAccount)
        {
            return await _repository.GetTutorCurrent(idAccount);
        }

        public async Task<bool> ChangeStatusTutor(IsActiveTutor model)
        {
            return await _repository.UpdateIsActiveTutor(model);
        }

        public async Task<DashBoardTutor> NumberOfClasses(string id, DateTime createDay)
        {
            
            var tutor = _repository.GetTutors().FirstOrDefault(s => s.AccountId == id);

            List<int> listData = new List<int>();
            DateTime currentDate = DateTime.Now;
            List<DateTime> dateTimes = new List<DateTime>();

            for (DateTime date = createDay; date <= currentDate; date = date.AddMonths(1))
            {
                dateTimes.Add(date);
            }

            foreach (var month in dateTimes)
            {
                int data = _classRepository.GetClasses()
                    .Where(s => s.TutorId == tutor.TutorId && s.CreateDay.Month == month.Month && s.CreateDay.Year == month.Year)
                    .Count();
                listData.Add(data);
            }

            DashBoardTutor result = new DashBoardTutor()
            {
                Title = "Number of classes by month",
                Value = _classRepository.GetClasses().Where(s => s.TutorId == tutor.TutorId && s.CreateDay.Month == DateTime.Now.Month).Count(),
                Data = listData,
                Dates = dateTimes,
            };

            return result;
        }

        public async Task<DashBoardTutor> NumberOfHour(string id, DateTime createDay)
        {
            
            var tutor = _repository.GetTutors().FirstOrDefault(s => s.AccountId == id);

            List<int> listData = new List<int>();
            DateTime currentDate = DateTime.Now;
            List<DateTime> dateTimes = new List<DateTime>();

            for (DateTime date = createDay; date <= currentDate; date = date.AddMonths(1))
            {
                dateTimes.Add(date);
            }

            foreach (var month in dateTimes)
            {
                //Lấy class có khoảng thời gian hoạt động trong tháng
                var classes = _classRepository.GetClasses()
                    .Where(s => s.TutorId == tutor.TutorId 
                    && s.DayStart.Month <= month.Month 
                    && s.DayEnd.Month >= month.Month
                    && s.DayStart.Year == month.Year
                    && s.DayEnd.Year == month.Year);
                int data = 0;
                foreach (var item in classes)
                {
                    //Lấy ngày trong lớp học có thời gian học trong tháng
                    var calenders = _calenderRepository.GetClassCalenders().Where(s => s.ClassId == item.ClassId && s.DayOfWeek.Month == month.Month && s.DayOfWeek.Year == month.Year);
                    foreach (var calender in calenders)
                    {
                        data = data + (calender.TimeEnd - calender.TimeStart);
                    }           
                }
                
                listData.Add(data);
            }

            DashBoardTutor result = new DashBoardTutor()
            {
                Title = "Number of teaching hours per month",
                Value = _classRepository.GetClasses().Where(s => s.TutorId == tutor.TutorId && s.CreateDay.Month == DateTime.Now.Month).Count(),
                Data = listData,
                Dates = dateTimes,
            };

            return result;
        }

        public async Task<DashBoardTutor> NumberOfClassesIsCancel(string id, DateTime createDay)
        {

            var tutor = _repository.GetTutors().FirstOrDefault(s => s.AccountId == id);

            List<int> listData = new List<int>();
            DateTime currentDate = DateTime.Now;
            List<DateTime> dateTimes = new List<DateTime>();

            for (DateTime date = createDay; date <= currentDate; date = date.AddMonths(1))
            {
                dateTimes.Add(date);
            }

            foreach (var month in dateTimes)
            {
                int data = _classRepository.GetClasses()
                    .Where(s => s.TutorId == tutor.TutorId && s.CancelDay?.Month == month.Month && s.CancelDay?.Year == month.Year)
                    .Count();
                listData.Add(data);
            }

            DashBoardTutor result = new DashBoardTutor()
            {
                Title = "Number of classes canceled by month",
                Value = _classRepository.GetClasses().Where(s => s.TutorId == tutor.TutorId && s.CreateDay.Month == DateTime.Now.Month).Count(),
                Data = listData,
                Dates = dateTimes,
            };

            return result;
        }

        public async Task<List<AccountTutorAdVM>> GetAccountHaveAd()
        {
            return await _repository.GetAccountHaveAd();
        }

        public async Task<List<HistoryTutorApply>> GetAllStatusHistoryTutorApply()
        {
            return await _repository.GetAllStatusHistoryTutorApply();
        }
    }
}
