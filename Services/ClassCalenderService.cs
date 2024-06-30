using BusinessObjects;
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

        public ClassCalenderService()
        {
            if (classCalenderRepository == null)
            {
                classCalenderRepository = new ClassCalenderRepository();
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
    }
}
