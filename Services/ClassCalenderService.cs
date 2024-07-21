using BusinessObjects;
using BusinessObjects.Models;
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
        private readonly IRequestTutorFormRepository requestTutorFormRepository = new RequestTutorFormRepository();
        private readonly IFindTutorFormRepository findTutorFormRepository = new FindTutorFormRepository();
        private readonly IClassRepository classRepository = new ClassRepository();
        private readonly ITutorApplyRepository tutorApplyRepository = new TutorApplyRepository();

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

        public void HandleTutorBrowserForm(Form form, string tutorId)
        {
            var formRequestList = requestTutorFormRepository.GetRequestTutorForms().Where(s => s.Status == null && s.FormId != form.FormId && s.TutorId == tutorId);
            var formFindList = tutorApplyRepository.GetTutorApplies().Where(s => s.IsApprove == null && s.FormId != form.FormId && s.TutorId == tutorId);
            List<RequestTutorForm> list = new List<RequestTutorForm>();

            List<DayOfWeek> formDays = classCalenderRepository.ParseDaysOfWeek(form.DayOfWeek);

            var filteredDates = classCalenderRepository.GetDatesByDaysOfWeek(form.DayStart, form.DayEnd, formDays);

            if (formRequestList.Any())
            {
                foreach (var f in formRequestList)
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
                                f.Status = false;
                                requestTutorFormRepository.UpdateRequestTutorForms(f);
                                break;
                            }

                        }
                    }
                }
            }
            if (formFindList.Any())
            {
                foreach (var f in formFindList)
                {
                    List<DayOfWeek> fDays = classCalenderRepository.ParseDaysOfWeek(f.FindTutorForm.DayOfWeek);
                    var fDates = classCalenderRepository.GetDatesByDaysOfWeek(f.FindTutorForm.DayStart, f.FindTutorForm.DayEnd, fDays);
                    foreach (var d in fDates)
                    {
                        if (filteredDates.Contains(d))
                        {
                            if (form.TimeStart <= f.FindTutorForm.TimeStart && form.TimeEnd >= f.FindTutorForm.TimeEnd
                             || form.TimeStart >= f.FindTutorForm.TimeStart && form.TimeStart < f.FindTutorForm.TimeEnd
                             || form.TimeEnd > f.FindTutorForm.TimeStart && form.TimeEnd <= f.FindTutorForm.TimeEnd)
                            {
                                f.IsActived = false;
                                tutorApplyRepository.UpdateTutorApplies(f);
                                break;
                            }

                        }
                    }
                }
            }
        }

        //Handle To Avoid Conflict With Class Calender
        public async Task<bool> HandleAvoidConflictCalendar(string dayOfWeek, DateTime dayStart, DateTime dayEnd, int timeStart, int timeEnd, string id, int typeActor)
        {

            //1.Get all booking day
            List<DayOfWeek> desiredDays = classCalenderRepository.ParseDaysOfWeek(dayOfWeek);

            var filteredDates = classCalenderRepository.GetDatesByDaysOfWeek(dayStart, dayEnd, desiredDays);

            //2.Select tutor's / student's calender
            List<Class> classList = new();
            if (typeActor == 1)
            {
                classList = classRepository.GetClasses().Where(s => s.Status == null && s.IsApprove != false && s.TutorId == id).ToList();
                if (!classList.Any())
                {
                    return true;
                }
            }
            if (typeActor == 2)
            {
                classList = classRepository.GetClasses().Where(s => s.Status == null && s.IsApprove != false && s.StudentId == id).ToList();
                if (!classList.Any())
                {
                    return true;
                }
            }
            var calender = from a in classList
                           join b in classCalenderRepository.GetClassCalenders()
                           on a.ClassId equals b.ClassId
                           select b;

            // 3. Checking
            foreach (var day in calender)
            {
                for (int i = 0; i < filteredDates.Count; i++)
                {
                    if (day.DayOfWeek == filteredDates[i])
                    {
                        if (timeStart <= day.TimeStart && timeEnd >= day.TimeEnd
                        || timeStart >= day.TimeStart && timeStart < day.TimeEnd
                         || timeEnd > day.TimeStart && timeEnd <= day.TimeEnd)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public async Task<bool> HandleAvoidConflictFormRequest(IEnumerable<RequestTutorForm> list, Form form)
        {
            List<DayOfWeek> formDays = classCalenderRepository.ParseDaysOfWeek(form.DayOfWeek);

            var filteredDates = classCalenderRepository.GetDatesByDaysOfWeek(form.DayStart, form.DayEnd, formDays);

            foreach (var f in list)
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
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public async Task<bool> HandleAvoidConflictFormFind(IEnumerable<TutorApply> list, Form form)
        {
            List<DayOfWeek> formDays = classCalenderRepository.ParseDaysOfWeek(form.DayOfWeek);

            var filteredDates = classCalenderRepository.GetDatesByDaysOfWeek(form.DayStart, form.DayEnd, formDays);

            var newList = from a in list
                          join f in findTutorFormRepository.GetFindTutorForms()
                          on a.FormId equals f.FormId
                          select f;

            foreach (var f in newList)
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
                            return false;
                        }

                    }
                }
            }
            return true;
        }

        public async Task<bool> HandleStudentCreateForm(string dayOfWeek, DateTime dayStart, DateTime dayEnd, int timeStart, int timeEnd, string studentId)
        {
            var formFindList = findTutorFormRepository.GetFindTutorForms().Where(s => s.Status == null && s.StudentId == studentId);
            var formRequestList = requestTutorFormRepository.GetRequestTutorForms().Where(s => s.Status == null && s.StudentId == studentId);

            List<DayOfWeek> formDays = classCalenderRepository.ParseDaysOfWeek(dayOfWeek);

            var filteredDates = classCalenderRepository.GetDatesByDaysOfWeek(dayStart, dayEnd, formDays);

            if (!formFindList.Any() && !formRequestList.Any())
            {
                return true;
            }

            if (formFindList.Any())
            {
                foreach (var f in formFindList)
                {
                    List<DayOfWeek> fDays = classCalenderRepository.ParseDaysOfWeek(f.DayOfWeek);
                    var fDates = classCalenderRepository.GetDatesByDaysOfWeek(f.DayStart, f.DayEnd, fDays);
                    foreach (var d in fDates)
                    {
                        if (filteredDates.Contains(d))
                        {
                            if (timeStart <= f.TimeStart && timeEnd >= f.TimeEnd
                             || timeStart >= f.TimeStart && timeStart < f.TimeEnd
                             || timeEnd > f.TimeStart && timeEnd <= f.TimeEnd)
                            {
                                return false;
                            }

                        }
                    }
                }
            }

            if (formRequestList.Any())
            {
                foreach (var f in formRequestList)
                {
                    List<DayOfWeek> fDays = classCalenderRepository.ParseDaysOfWeek(f.DayOfWeek);
                    var fDates = classCalenderRepository.GetDatesByDaysOfWeek(f.DayStart, f.DayEnd, fDays);
                    foreach (var d in fDates)
                    {
                        if (filteredDates.Contains(d))
                        {
                            if (timeStart <= f.TimeStart && timeEnd >= f.TimeEnd
                             || timeStart >= f.TimeStart && timeStart < f.TimeEnd
                             || timeEnd > f.TimeStart && timeEnd <= f.TimeEnd)
                            {
                                return false;
                            }

                        }
                    }
                }
            }

            return true;
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

        public string ConvertToDaysOfWeeks(string input)
        {
            return classCalenderRepository.ConvertToDaysOfWeeks(input);
        }
    }
}
