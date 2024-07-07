using BusinessObjects;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class TutorApplyRepository : ITutorApplyRepository
    {
        private readonly TutorApplyDAO _tutorApplyDAO = null;

        public TutorApplyRepository()
        {
            if (_tutorApplyDAO == null)
            {
                _tutorApplyDAO= new TutorApplyDAO();
            }
        }


        public bool AddTutorApply(TutorApply tutorApply)
        {
            return _tutorApplyDAO.AddClass(tutorApply);
        }

        public bool DelTutorApplies(int id)
        {
            return _tutorApplyDAO.DelTutorApplies(id);
        }

        public List<TutorApply> GetTutorApplies()
        {
            return _tutorApplyDAO.GetTutorApplies();
        }

        public bool UpdateTutorApplies(TutorApply tutorApply)
        {
            return _tutorApplyDAO.UpdateTutorApplies(tutorApply);
        }
    }
}
