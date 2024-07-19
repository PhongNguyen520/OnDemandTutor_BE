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
    public class TutorAdRepository : ITutorAdRepository
    {
        private readonly TutorAdDAO tutorAdDAO = null;
        private readonly DAOs.DbContext _dbContext;
        private readonly IMapper _mapper;

        public TutorAdRepository(DAOs.DbContext dbContext, IMapper mapper)
        {
            if (tutorAdDAO == null)
            {
                tutorAdDAO = new TutorAdDAO();
            }
            _dbContext = dbContext;
            _mapper = mapper;
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

        public async Task<List<TutorIsActiveVM>> GetAllTutorAdIsActive()
        {
            var list = _dbContext.TutorAds
                       .Include(_ => _.Tutor)
                       .Where(_ => _.IsActived == null)
                       .ToList();
            var listEnd = _mapper.Map<List<TutorIsActiveVM>>(list);
            return listEnd;
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
