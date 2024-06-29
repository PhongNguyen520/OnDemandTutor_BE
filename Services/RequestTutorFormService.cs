using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RequestTutorFormService : IRequestTutorFormService
    {
        private readonly RequestTutorFormRepository _repository = null;

        public RequestTutorFormService()
        {
            if (_repository == null)
            {
                _repository = new RequestTutorFormRepository();
            }
        }

        public bool AddRequestTutorForm(RequestTutorForm form)
        {
            return _repository.AddRequestTutorForm(form);
        }

        public bool DelRequestTutorForms(int id)
        {
            return _repository.DelRequestTutorForms(id);
        }

        public List<RequestTutorForm> GetRequestTutorForms()
        {
            return _repository.GetRequestTutorForms();
        }

        public bool UpdateRequestTutorForms(RequestTutorForm form)
        {
            return _repository.UpdateRequestTutorForms(form);
        }
    }
}
