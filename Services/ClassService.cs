using BusinessObjects;
using BusinessObjects.Models;
using DAOs;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClassService : IClassService
    {
        private readonly IClassRepository _classRepository;
        private readonly IFindTutorFormRepository _findTutorFormRepository;
        private readonly IRequestTutorFormRepository _requestTutorFormRepository;

        public ClassService()
        {
            _classRepository = new ClassRepository();
            _findTutorFormRepository = new FindTutorFormRepository();
            _requestTutorFormRepository = new RequestTutorFormRepository();
        }

        public bool AddClass(Class @class)
        {
            return _classRepository.AddClass(@class);
        }

        public bool DelClasses(int id)
        {
            return _classRepository.DelClasses(id);
        }

        public List<Class> GetClasses()
        {
            return _classRepository.GetClasses();
        }

        public bool UpdateClasses(Class @class)
        {
            return _classRepository.UpdateClasses(@class);
        }

        public Form? CheckTypeForm(string id)
        {
            Form? result = null;
            var findTutorForm = _findTutorFormRepository.GetFindTutorForms().Where(s => s.FormId == id);
            if (findTutorForm.Any())
            {
                result = new Form()
                {
                    FormId = id,
                    DayOfWeek = findTutorForm.First().DayOfWeek,
                    DayStart = findTutorForm.First().DayStart,
                    DayEnd = findTutorForm.First().DayEnd,
                    TimeEnd = findTutorForm.First().TimeEnd,
                    TimeStart = findTutorForm.First().TimeStart,
                    StudentId = findTutorForm.First().StudentId,
                    SubjectId = findTutorForm.First().SubjectId,
                };
            }

            var requestTutorForm = _requestTutorFormRepository.GetRequestTutorForms().Where(s => s.FormId == id);
            if (requestTutorForm.Any())
            {
                result = new Form()
                {
                    FormId = id,
                    DayOfWeek = requestTutorForm.First().DayOfWeek,
                    DayStart = requestTutorForm.First().DayStart,
                    DayEnd = requestTutorForm.First().DayEnd,
                    TimeEnd = requestTutorForm.First().TimeEnd,
                    TimeStart = requestTutorForm.First().TimeStart,
                    StudentId = requestTutorForm.First().StudentId,
                    SubjectId = requestTutorForm.First().SubjectId,
                };
            }


            return result;
        }
    }
}
