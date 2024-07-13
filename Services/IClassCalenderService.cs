using BusinessObjects;
using BusinessObjects.Models;
using Repositories;
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

        void HandleTutorBrowserForm(Form form, string tutorId);

        public int TotalHourByMonth(IEnumerable<Class> classList, string tutorId);

        public string ConvertToDaysOfWeeks(string input);

        Task<bool> HandleAvoidConflictCalendar(string dayOfWeek, DateTime dayStart, DateTime dayEnd, int timeStart, int timeEnd, string id, int typeActor);

        Task<bool> HandleAvoidConflictFormFind(IEnumerable<TutorApply> list, Form form);

        Task<bool> HandleAvoidConflictFormRequest(IEnumerable<RequestTutorForm> list, Form form);

        Task<bool> HandleStudentCreateForm(string dayOfWeek, DateTime dayStart, DateTime dayEnd, int timeStart, int timeEnd, string studentId);
    }
}
