using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRequestTutorFormRepository
    {
        public bool AddRequestTutorForm(RequestTutorForm form);

        public bool DelRequestTutorForms(int id);

        public List<RequestTutorForm> GetRequestTutorForms();

        public bool UpdateRequestTutorForms(RequestTutorForm form);
    }
}
