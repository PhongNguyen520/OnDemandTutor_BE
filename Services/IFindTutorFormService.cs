using BusinessObjects;
using BusinessObjects.Models.FormModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IFindTutorFormService
    {
        public bool AddFindTutorForm(FindTutorForm form);

        public bool DelFindTutorForms(int id);

        public List<FindTutorForm> GetFindTutorForms();

        public bool UpdateFindTutorForms(FindTutorForm form);

        public IEnumerable<FindTutorForm> Filter(RequestSearchPostModel requestSearchPostModel);

        public IEnumerable<FormVM> Sorting
           (IEnumerable<FormVM> query,
           string? sortBy,
           string? sortType,
           int pageIndex);
    }
}
