using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IClassCalenderService
    {
        public bool AddClassCalender(ClassCalender calender);

        public bool DelClassCalenders(int id);

        public List<ClassCalender> GetClassCalenders();

        public bool UpdateClassCalenders(ClassCalender calender);

        public List<DateTime> GetDatesByDaysOfWeek(DateTime startDate, DateTime endDate, List<DayOfWeek> desiredDays);

        public List<DayOfWeek> ParseDaysOfWeek(string input);

        public List<RequestTutorForm> HandleCalendar(string formId);

        public int TotalHourByMonth(IEnumerable<Class> classList, string tutorId);

        public string ConvertToDaysOfWeeks(string input);
    }
}
