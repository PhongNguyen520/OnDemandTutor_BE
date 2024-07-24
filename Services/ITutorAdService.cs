using BusinessObjects;
using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ITutorAdService
    {
        public bool AddTutorAd(TutorAd tutorAd);

        public bool DelTutorAds(int id);

        public List<TutorAd> GetTutorAds();

        public bool UpdateTutorAds(TutorAd tutorAd);

        Task<List<TutorIsActiveVM>> GetAllTutorAdIsActive();

        Task<bool> CeateAd(TutorAd model);

        Task<bool> UpdateIsActiveTutorAd(TutorAdIsAc model);

        Task<List<AdsVMPl>> GetAllAds();
    }
}
