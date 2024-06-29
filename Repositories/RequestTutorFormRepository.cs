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
        private readonly RequestTutorFormDAO _dao = null;

        public RequestTutorFormRepository()
        {
            if (_dao == null)
            {
                _dao = new RequestTutorFormDAO();
            }
        }

        public bool AddRequestTutorForm(RequestTutorForm form)
        {
            return _dao.AddRequestTutorForm(form);
        }

        public bool DelRequestTutorForms(int id)
        {
            return _dao.DelRequestTutorForms(id);
        }

        public List<RequestTutorForm> GetRequestTutorForms()
        {
            return _dao.GetRequestTutorForms();
        }

        public bool UpdateRequestTutorForms(RequestTutorForm form)
        {
            return _dao.UpdateRequestTutorForms(form);
        }
    }
}
