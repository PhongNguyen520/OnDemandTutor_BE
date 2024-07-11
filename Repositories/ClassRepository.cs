using BusinessObjects;
using BusinessObjects.Models;
using DAOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly ClassDAO classDAO = null;
        private readonly DAOs.DbContext _dbContext;

        public ClassRepository()
        {
            if (classDAO == null)
            {
                classDAO = new ClassDAO();
            }
        }
        public ClassRepository(DAOs.DbContext dbContext)
        {
            if (classDAO == null)
            {
                classDAO = new ClassDAO();
            }
            _dbContext = dbContext;
        }

        public bool AddClass(Class @class)
        {
            return classDAO.AddClass(@class);
        }

        public bool DelClasses(int id)
        {
            return classDAO.DelClasses(id);
        }

        public List<Class> GetClasses()
        {
            return classDAO.GetClasses();
        }

        public bool UpdateClasses(Class @class)
        {
            return classDAO.UpdateClasses(@class);
        }

        public async Task<ReturnBalance> PaymentTutor(string userId)
        {

            ReturnBalance returnBalance = new ReturnBalance();
            var tutor = await _dbContext
                                .Tutors
                                .FirstOrDefaultAsync(_ => _.AccountId == userId);
            if (tutor == null) return null;

            var classi = await _dbContext
                                .Classes
                                .FirstOrDefaultAsync(_ => _.TutorId == tutor.TutorId);
            if (classi == null) return null;
            if (classi.Status == null && classi.IsApprove == true)
            {
                classi.Status = true;
                float amount = classi.Price * (60 / 100);
                returnBalance.PlusMoney = amount;
                returnBalance.TutorId = classi.TutorId;
                return returnBalance;
            } 
            return null;
        }
    }
}
