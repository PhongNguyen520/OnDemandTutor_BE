﻿using BusinessObjects;
using BusinessObjects.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TutorApplyService : ITutorApplyService
    {
        private readonly ITutorApplyRepository _repository;

        public TutorApplyService()
        {
            _repository = new TutorApplyRepository();
        }

        public async Task<bool> AddTutorApply(TutorApply tutorApply)
        {
            return _repository.AddTutorApply(tutorApply);
        }

        public bool DelTutorApplies(int id)
        {
            return _repository.DelTutorApplies(id);
        }

        public List<TutorApply> GetTutorApplies()
        {
            return _repository.GetTutorApplies();
        }

        public bool UpdateTutorApplies(TutorApply tutorApply)
        {
            return _repository.UpdateTutorApplies(tutorApply);
        }

        public async Task<IEnumerable<TutorApply>> TutorApplyForm(string tutorId)
        {
            return _repository.GetTutorApplies().Where(s => s.TutorId == tutorId);
        }
    }
}
