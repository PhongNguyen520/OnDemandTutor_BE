using BusinessObjects;
using BusinessObjects.Models;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TutorAdService : ITutorAdService
    {
        private readonly ITutorAdRepository iTutorAdRepository;


        public TutorAdService(ITutorAdRepository tutorAdRepository)
        {
            iTutorAdRepository = tutorAdRepository;
        }

        public bool AddTutorAd(TutorAd tutorAd)
        {
            return iTutorAdRepository.AddTutorAd(tutorAd);
        }

        public bool DelTutorAds(int id)
        {
            return iTutorAdRepository.DelTutorAds(id);
        }

        public List<TutorAd> GetTutorAds()
        {
            return iTutorAdRepository.GetTutorAds();
        }

        public bool UpdateTutorAds(TutorAd tutorAd)
        {
            return iTutorAdRepository.UpdateTutorAds(tutorAd);
        }

        public async Task<List<TutorAd>> GetAllTutorAdIsActive()
        {
            return await iTutorAdRepository.GetAllTutorAdIsActive();
        }

        public async Task<bool> CeateAd(TutorAd model)
        {
            return await iTutorAdRepository.CeateAd(model);
        }

        public async Task<bool> UpdateIsActiveTutorAd(TutorAdIsAc model)
        {
            return await iTutorAdRepository.UpdateIsActiveTutorAd(model);
        }
    }
}
