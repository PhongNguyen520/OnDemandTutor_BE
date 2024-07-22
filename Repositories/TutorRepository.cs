using AutoMapper;
using BusinessObjects;
using BusinessObjects.Models;
using BusinessObjects.Models.TutorModel;
using DAOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Repositories
{
    public class TutorRepository : ITutorRepository
    {
        private readonly TutorDAO tutorDAO = null;
        private readonly DAOs.DbContext _dbContext;
        private readonly UserManager<Account> _userManager;
        private readonly IMapper _mapper;

        public TutorRepository()
        {
            if (tutorDAO == null)
            {
                tutorDAO = new TutorDAO();
            }
        }

        public TutorRepository(UserManager<Account> userManager, IMapper mapper, DAOs.DbContext dbContext)
        {
            if (tutorDAO == null)
            {
                tutorDAO = new TutorDAO();
            }
            _dbContext = dbContext;
            _userManager = userManager;
            _mapper = mapper;
        }


        public bool AddTutor(Tutor tutor)
        {
            return tutorDAO.AddTutor(tutor);
        }

        public bool DelTutors(int id)
        {
            return tutorDAO.DelTutors(id);
        }

        public List<Tutor> GetTutors()
        {
            return tutorDAO.GetTutors();
        }


        public bool UpdateTutors(Tutor tutor)
        {
            return tutorDAO.UpdateTutors(tutor);
        }

        public IEnumerable<Tutor> Filter(RequestSearchTutorModel requestSearchTutorModel)
        {
            var allTutor = tutorDAO.GetTutors().Where(tu => tu.IsActive == true);

            //Trường hợp chọn loại bằng
            if (string.IsNullOrEmpty(requestSearchTutorModel.TypeOfDegree))
            {
                allTutor = tutorDAO.GetTutors().
                    Where(tu => tu.IsActive == true
                    && tu.HourlyRate >= requestSearchTutorModel.MinRate
                    && tu.HourlyRate <= requestSearchTutorModel.MaxRate);
            }
            //Trường KHÔNG hợp chọn loại bằng
            else
            {
                allTutor = tutorDAO.GetTutors().
                    Where(tu => tu.IsActive == true
                    && tu.HourlyRate >= requestSearchTutorModel.MinRate
                    && tu.HourlyRate <= requestSearchTutorModel.MaxRate
                    && tu.TypeOfDegree == requestSearchTutorModel.TypeOfDegree);
            }

            return allTutor;
        }

        public IEnumerable<ResponseSearchTutorModel> Sorting
            (IEnumerable<ResponseSearchTutorModel> query, 
            string? sortBy, 
            string? sortType)
        {
            //_____SORT_____
            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortType == SortTutorTypeEnum.Ascending.ToString() && sortBy == SortTutorByEnum.HourlyRate.ToString())
                {
                    query = query.OrderBy(t => t.HourlyRate);
                }
                else if (sortType == SortTutorTypeEnum.Descending.ToString() && sortBy == SortTutorByEnum.HourlyRate.ToString())
                {
                    query = query.OrderByDescending(t => t.HourlyRate);
                }
                else if (sortType == SortTutorTypeEnum.Ascending.ToString() && sortBy == SortTutorByEnum.Start.ToString())
                {
                    query = query.OrderBy(t => t.Start);
                }
                else if (sortType == SortTutorTypeEnum.Descending.ToString() && sortBy == SortTutorByEnum.Start.ToString())
                {
                    query = query.OrderByDescending(t => t.Start);
                }
            }
            else
            {
                query = query.OrderBy(t => t.HourlyRate);
            }

            return query;
        }

        public async Task<TutorVM> UpdateTutor(string idAccount, TutorVM tutorVM)
        {
 
            var tutorDb = await _dbContext.Tutors
                                          .Include(t => t.Account)
                                          .FirstOrDefaultAsync(t => t.AccountId == idAccount);
            var accountDb = await _userManager.FindByIdAsync(idAccount);

            if (tutorDb == null)
            {
                return null;
            }
            else
            {
                _mapper.Map(tutorVM, tutorDb);
                tutorDAO.UpdateTutors(tutorDb);

                _mapper.Map(tutorVM, accountDb);
                _dbContext.Update(accountDb);
                _dbContext.SaveChanges();
                return tutorVM;
            }
        }

        public async Task<TutorVM> GetTutorCurrent(string idAccount)
        {
            var tutorVM = new TutorVM();
            var tutorDb = await _dbContext.Tutors
                                          .Include(t => t.Account)
                                          .FirstOrDefaultAsync(t => t.AccountId == idAccount);
            var accountDb = await _userManager.FindByIdAsync(idAccount);
            if (tutorDb == null)
            {
                return null;
            }
            else
            {
                tutorVM = _mapper.Map<TutorVM>(tutorDb);
                _mapper.Map(accountDb, tutorVM);
                return tutorVM;
            }
        }

        public async Task<bool> UpdateIsActiveTutor (IsActiveTutor model)
        {
            var tutor = await _dbContext.Tutors.FirstOrDefaultAsync(t => t.AccountId == model.AccountId);
            if(tutor != null)
            {
                tutor.IsActive = model.Status;
                tutorDAO.UpdateTutors(tutor);
                return true;
            }
            return false;
        }

        public async Task<List<AccountTutorAdVM>> GetAccountHaveAd()
        {
            var listTutorHaveAd = await _dbContext.Tutors
                                   .Include(_ => _.Account)
                                   .Where(_ => _.Account.IsActive == true
                                          && _.IsActive == true
                                          && _.TutorAds.Any(_ => _.IsActived == true))
                                   .ToListAsync();
            var result = _mapper.Map<List<AccountTutorAdVM>>(listTutorHaveAd);

            foreach(var x in result)
            {
                var listAd = await _dbContext.TutorAds
                                       .Where(_ => _.TutorId == x.TutorId
                                              && _.IsActived == true)
                                       .ToListAsync();
                x.TutorAds = _mapper.Map<List<TutorAdsModel>>(listAd);
            }

            return result;
        }

        public async Task<List<HistoryTutorApply>> GetAllStatusHistoryTutorApply()
        {
            var result = await _dbContext.HistoryTutors.Where(_ => _.Status == null).ToListAsync();
            return result;
        }

    }
}
