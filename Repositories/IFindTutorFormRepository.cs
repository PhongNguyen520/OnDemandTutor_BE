using BusinessObjects;
using BusinessObjects.Models.FindFormModel;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IFindTutorFormRepository
    {
        public bool AddFindTutorForm(FindTutorForm form);

        public bool DelFindTutorForms(int id);

        public List<FindTutorForm> GetFindTutorForms();

        public bool UpdateFindTutorForms(FindTutorForm form);
        public IEnumerable<FindTutorForm> Filter(RequestSearchPostModel requestSearchPostModel);

        public IEnumerable<FormFindTutorVM> Sorting
           (IEnumerable<FormFindTutorVM> query,
           string? sortBy,
           string? sortType,
           int pageIndex);

    }
}
