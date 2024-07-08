using BusinessObjects;
using BusinessObjects.Models;
using BusinessObjects.Models.TutorModel;
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
    }
}
