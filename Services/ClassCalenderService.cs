using BusinessObjects;
using Microsoft.SqlServer.Server;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClassCalenderService : IClassCalenderService
    {
        private readonly IClassCalenderRepository classCalenderRepository = null;
        private readonly IRequestTutorFormRepository requestTutorFormRepository = null;
        private readonly IClassRepository classRepository = null;

        public ClassCalenderService()
        {
            if (classCalenderRepository == null)
            {
                classCalenderRepository = new ClassCalenderRepository();
                requestTutorFormRepository = new RequestTutorFormRepository();
                classRepository = new ClassRepository();
            }
        }

        public bool AddClassCalender(ClassCalender calender)
        {
            return classCalenderRepository.AddClassCalender(calender);
        }

        public bool DelClassCalenders(int id)
        {
            return classCalenderRepository.DelClassCalenders(id);
        }

        public List<ClassCalender> GetClassCalenders()
        {
            return classCalenderRepository.GetClassCalenders();
        }

        public List<DateTime> GetDatesByDaysOfWeek(DateTime startDate, DateTime endDate, List<DayOfWeek> desiredDays)
        {
            return classCalenderRepository.GetDatesByDaysOfWeek(startDate, endDate, desiredDays);
        }

        public List<DayOfWeek> ParseDaysOfWeek(string input)
        {
            return classCalenderRepository.ParseDaysOfWeek(input);
        }

        public bool UpdateClassCalenders(ClassCalender calender)
        {
            return classCalenderRepository.UpdateClassCalenders(calender);
        }

        public List<RequestTutorForm> HandleCalendar(string formId)
        {
            var form = requestTutorFormRepository.GetRequestTutorForms().Where(s => s.FormId == formId).FirstOrDefault();
            var formList = requestTutorFormRepository.GetRequestTutorForms().Where(s => s.Status == null && s.FormId != formId);
            List<RequestTutorForm> list = new List<RequestTutorForm>();

            List<DayOfWeek> formDays = classCalenderRepository.ParseDaysOfWeek(form.DayOfWeek);

            var filteredDates = classCalenderRepository.GetDatesByDaysOfWeek(form.DayStart, form.DayEnd, formDays);

            foreach (var f in formList)
            {
                List<DayOfWeek> fDays = classCalenderRepository.ParseDaysOfWeek(f.DayOfWeek);
                var fDates = classCalenderRepository.GetDatesByDaysOfWeek(f.DayStart, f.DayEnd, fDays);
                foreach (var d in fDates)
                {
                    if (filteredDates.Contains(d))
                    {
                        if (form.TimeStart <= f.TimeStart && form.TimeEnd >= f.TimeEnd
                         || form.TimeStart >= f.TimeStart && form.TimeStart < f.TimeEnd
                         || form.TimeEnd > f.TimeStart && form.TimeEnd <= f.TimeEnd)
                        {
                            list.Add(f);
                            break;
                        }

                    }
                }
            }
            return list;
        }

        public int TotalHourByMonth(IEnumerable<Class> classList, string tutorId)
        {
            var tutorClass = classList.Where(s => s.TutorId == tutorId);
            int result = 0;
            foreach (var item in tutorClass) 
            {
                var calenderList = classCalenderRepository.GetClassCalenders().Where(s => s.ClassId == item.ClassId);
                foreach (var calender in calenderList)
                {
                    if (calender.DayOfWeek.Month == DateTime.Now.Month - 1)
                    {
                        result = result + (calender.TimeEnd - calender.TimeStart);
                    }
                }
            }
            return result;
        }
}
}
