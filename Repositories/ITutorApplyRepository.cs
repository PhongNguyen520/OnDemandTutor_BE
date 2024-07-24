﻿using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ITutorApplyRepository
    {
        public bool AddTutorApply(TutorApply tutorApply);

        public bool DelTutorApplies(int id);

        public List<TutorApply> GetTutorApplies();

        public bool UpdateTutorApplies(TutorApply tutorApply);
    }
}
