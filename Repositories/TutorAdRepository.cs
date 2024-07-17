using BusinessObjects;
using BusinessObjects.Models;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class TutorAdRepository : ITutorAdRepository
    {
        private readonly TutorAdDAO tutorAdDAO = null;
        private readonly DAOs.DbContext _dbContext;

        public TutorAdRepository(DbContext dbContext)
        {
            if (tutorAdDAO == null)
            {
                tutorAdDAO = new TutorAdDAO();
            }
            _dbContext = dbContext;
        }

        public bool AddTutorAd(TutorAd tutorAd)
        {
            return tutorAdDAO.AddTutorAd(tutorAd);
        }

        public bool DelTutorAds(int id)
        {
            return tutorAdDAO.DelTutorAds(id);
        }

        public List<TutorAd> GetTutorAds()
        {
            return tutorAdDAO.GetTutorAds();
        }

        public bool UpdateTutorAds(TutorAd tutorAd)
        {
            return tutorAdDAO.UpdateTutorAds(tutorAd);
        }

        public async Task<List<TutorAd>> GetAllTutorAdIsActive()
        {
            var list = _dbContext.TutorAds
                       .Where(_ => _.IsActived == null)
                       .ToList();
            return list;
        }

        public async Task<bool> CeateAd(TutorAd model)
        {
            _dbContext.Add(model);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateIsActiveTutorAd(TutorAdIsAc model)
        {
            var tutorAd = _dbContext.TutorAds
                                    .FirstOrDefault(_ => _.AdsId == model.Id);
            if(tutorAd == null)
            {
                return false;
            }
            tutorAd.IsActived = model.IsActive;
            _dbContext.Update(tutorAd);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
