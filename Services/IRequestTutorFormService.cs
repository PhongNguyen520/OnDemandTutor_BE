using BusinessObjects;
using BusinessObjects.Models.RequestFormModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IRequestTutorFormService
    {
        public bool AddRequestTutorForm(RequestTutorForm form);

        public bool DelRequestTutorForms(int id);

        public List<RequestTutorForm> GetRequestTutorForms();

        public bool UpdateRequestTutorForms(RequestTutorForm form);

        public FormMember? GetFormMember(bool? status, string id);
    }
}
