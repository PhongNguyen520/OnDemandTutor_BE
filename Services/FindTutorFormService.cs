using BusinessObjects;
using BusinessObjects.Models.FindFormModel;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FindTutorFormService : IFindTutorFormService
    {
        private readonly IFindTutorFormRepository iFindTutorFormRepository = null;

        public FindTutorFormService()
        {
            if (iFindTutorFormRepository == null)
            {
                iFindTutorFormRepository = new FindTutorFormRepository();
            }
        }
        public bool AddFindTutorForm(FindTutorForm form)
        {
            return iFindTutorFormRepository.AddFindTutorForm(form);
        }

        public bool DelFindTutorForms(int id)
        {
            return iFindTutorFormRepository.DelFindTutorForms(id);
        }

        public List<FindTutorForm> GetFindTutorForms()
        {
            return iFindTutorFormRepository.GetFindTutorForms();
        }

        public bool UpdateFindTutorForms(FindTutorForm form)
        {
            return iFindTutorFormRepository.UpdateFindTutorForms(form);
        }

        public IEnumerable<FindTutorForm> Filter(RequestSearchPostModel requestSearchPostModel)
        {
            return iFindTutorFormRepository.Filter(requestSearchPostModel);
        }

        public IEnumerable<FormFindTutorVM> Sorting(IEnumerable<FormFindTutorVM> query, string? sortBy, string? sortType, int pageIndex)
        {
            return iFindTutorFormRepository.Sorting(query, sortBy, sortType, pageIndex);
        }
    }
}
