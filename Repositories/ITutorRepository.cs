﻿using BusinessObjects;
using BusinessObjects.Models;
using BusinessObjects.Models.TutorModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ITutorRepository
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
        Task<bool> UpdateIsActiveTutor(IsActiveTutor model);
        Task<List<AccountTutorAdVM>> GetAccountHaveAd();
        Task<List<HistoryTutorApply>> GetAllStatusHistoryTutorApply();
    }
}

