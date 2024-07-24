using AutoMapper;
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
        private IMapper _mapper;

        public ClassRepository()
        {
            if (classDAO == null)
            {
                classDAO = new ClassDAO();
            }
        }
        public ClassRepository(DAOs.DbContext dbContext, IMapper mapper)
        {
            if (classDAO == null)
            {
                classDAO = new ClassDAO();
            }
            _dbContext = dbContext;
            _mapper = mapper;
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
            if (classi.Status == null && classi.IsApprove == true && classi.DayEnd.Date <= DateTime.Now.Date)
            {
                classi.Status = true;
                float amount = (float)(classi.Price * 0.6);
                returnBalance.PlusMoney = amount;
                returnBalance.TutorId = classi.TutorId;

                var walletAdmin = await _dbContext.Wallets.FirstOrDefaultAsync(_ => _.WalletId == "jfdskj-dfhs");

                walletAdmin.Balance -= amount;
                _dbContext.Update(walletAdmin);
                _dbContext.SaveChanges();
                return returnBalance;
            }
            returnBalance.PlusMoney = 0;
            returnBalance.TutorId = classi.TutorId;
            return returnBalance;
        }

        public async Task<List<ListClassVMPhuc>> GetClassByDay()
        {
            var list = await _dbContext.Classes
                             .GroupBy(_ => _.CreateDay.Date)
                             .Select(_ => new ListClassVMPhuc
                             {
                                 Date = _.Key,
                                 ClassVMPhuc = _.Select(_ => _mapper.Map<ClassVMPhuc>(_))
                                 .ToList()
                             })
                             .ToListAsync();
            return list;
        }

        public async Task<List<ListClassVMPhucMonthYear>> GetClassByMonth()
        {
            var list = await _dbContext.Classes
                             .GroupBy(_ => new { _.CreateDay.Year, _.CreateDay.Month })
                             .Select(_ => new ListClassVMPhucMonthYear
                             {
                                 Year = _.Key.Year,
                                 Month = _.Key.Month,
                                 ClassVMPhuc = _.Select(_ => _mapper.Map<ClassVMPhuc>(_))
                                 .ToList()
                             })
                             .ToListAsync();
            return list;
        }

    }
}
