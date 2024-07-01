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
        private readonly RequestTutorFormRepository repository = null;

        public RequestTutorFormService()
        {
            if (repository == null)
            {
                repository = new RequestTutorFormRepository();
            }
        }

        public bool AddRequestTutorForm(RequestTutorForm form)
        {
            return repository.AddRequestTutorForm(form);
        }

        public bool DelRequestTutorForms(int id)
        {
            return repository.DelRequestTutorForms(id);
        }

        public List<RequestTutorForm> GetRequestTutorForms()
        {
            return repository.GetRequestTutorForms();
        }

        public bool UpdateRequestTutorForms(RequestTutorForm form)
        {
            return repository.UpdateRequestTutorForms(form);
        }
    }
}
