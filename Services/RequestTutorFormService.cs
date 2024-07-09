using BusinessObjects;
using BusinessObjects.Models;
using BusinessObjects.Models.RequestFormModel;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IRequestTutorFormRepository _requestTutorFormRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ITutorRepository _tutorRepository;
        private readonly IAccountRepository _accountRepository;

        public RequestTutorFormService()
        {
            _accountRepository = new AccountRepository();
            _requestTutorFormRepository = new RequestTutorFormRepository();
            _studentRepository = new StudentRepository();
            _tutorRepository = new TutorRepository();
        }

        public bool AddRequestTutorForm(RequestTutorForm form)
        {
            return _requestTutorFormRepository.AddRequestTutorForm(form);
        }

        public bool DelRequestTutorForms(int id)
        {
            return _requestTutorFormRepository.DelRequestTutorForms(id);
        }

        public List<RequestTutorForm> GetRequestTutorForms()
        {
            return _requestTutorFormRepository.GetRequestTutorForms();
        }

        public bool UpdateRequestTutorForms(RequestTutorForm form)
        {
            return _requestTutorFormRepository.UpdateRequestTutorForms(form);
        }

        public FormMember? GetFormMember(bool? status, string id)
        {
            FormMember? result = null;
            var student = _studentRepository.GetStudents().Where(s => s.AccountId == id);
            if (student.Any())
            {
                var account = _accountRepository.GetAccounts().Where(s => s.Id == student.First().AccountId).First();
                result = new FormMember()
                {
                    Avatar = account.Avatar,
                    FullName = account.FullName,
                    List = _requestTutorFormRepository.GetRequestTutorForms()
                            .Where(s => s.StudentId == student.First().StudentId && s.Status == status).ToList(),
                };
            }

            var tutor = _tutorRepository.GetTutors().Where(s => s.AccountId == id);
            if (tutor.Any())
            {
                var account = _accountRepository.GetAccounts().Where(s => s.Id == tutor.First().AccountId).First();
                result = new FormMember()
                {
                    Avatar = account.Avatar,
                    FullName = account.FullName,
                    List = _requestTutorFormRepository.GetRequestTutorForms()
                            .Where(s => s.TutorId == tutor.First().TutorId && s.Status == status).ToList(),
                };
            }
            return result;
        }
    }
}
