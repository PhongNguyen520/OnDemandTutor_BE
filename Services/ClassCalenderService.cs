using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ClassCalenderService : IClassCalenderService
    {
        private readonly IClassCalenderService _classCalenderService = null;

        public ClassCalenderService()
        {
            if (_classCalenderService == null)
            {
                _classCalenderService= new ClassCalenderService();
            }
        }

        public bool AddClassCalender(ClassCalender calender)
        {
            return _classCalenderService.AddClassCalender(calender);
        }

        public bool DelClassCalenders(int id)
        {
            return _classCalenderService.DelClassCalenders(id);
        }

        public List<ClassCalender> GetClassCalenders()
        {
            return _classCalenderService.GetClassCalenders();
        }

        public List<DateTime> GetDatesByDaysOfWeek(DateTime startDate, DateTime endDate, List<DayOfWeek> desiredDays)
        {
            return _classCalenderService.GetDatesByDaysOfWeek(startDate, endDate, desiredDays);
        }

        public List<DayOfWeek> ParseDaysOfWeek(string input)
        {
            return _classCalenderService.ParseDaysOfWeek(input);
        }

        public bool UpdateClassCalenders(ClassCalender calender)
        {
            return _classCalenderService.UpdateClassCalenders(calender);
        }
    }
}
