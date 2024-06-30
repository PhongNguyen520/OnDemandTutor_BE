using BusinessObjects;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RequestTutorFormRepository : IRequestTutorFormRepository
    {
        private readonly RequestTutorFormDAO dao = null;

        public RequestTutorFormRepository()
        {
            if (dao == null)
            {
                dao = new RequestTutorFormDAO();
            }
        }

        public bool AddRequestTutorForm(RequestTutorForm form)
        {
            return dao.AddRequestTutorForm(form);
        }

        public bool DelRequestTutorForms(int id)
        {
            return dao.DelRequestTutorForms(id);
        }

        public List<RequestTutorForm> GetRequestTutorForms()
        {
            return dao.GetRequestTutorForms();
        }

        public bool UpdateRequestTutorForms(RequestTutorForm form)
        {
            return dao.UpdateRequestTutorForms(form);
        }
    }
}
