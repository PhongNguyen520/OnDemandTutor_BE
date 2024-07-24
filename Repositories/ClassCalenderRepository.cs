using BusinessObjects;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class ClassCalenderRepository : IClassCalenderRepository
    {
        private readonly ClassCalenderDAO dao = null;

        public ClassCalenderRepository()
        {
            if (dao == null)
            {
                dao = new ClassCalenderDAO();
            }
        }

        public bool AddClassCalender(ClassCalender calender)
        {
            return dao.AddClassCalenders(calender);
        }

        public bool DelClassCalenders(int id)
        {
            return dao.DelClassCalenders(id);
        }

        public List<ClassCalender> GetClassCalenders()
        {
            return dao.GetClassCalenders();
        }

        public bool UpdateClassCalenders(ClassCalender calender)
        {
            return dao.UpdateClassCalenders(calender);
        }

        public List<DateTime> GetDatesByDaysOfWeek(DateTime startDate, DateTime endDate, List<DayOfWeek> desiredDays)
        {
            List<DateTime> dates = new List<DateTime>();

            // Duyệt qua từng ngày trong khoảng thời gian
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                // Nếu ngày hiện tại là một trong các thứ mong muốn, thêm vào danh sách
                if (desiredDays.Contains(date.DayOfWeek))
                {
                    dates.Add(date);
                }
            }

            return dates;
        }

        public List<DayOfWeek> ParseDaysOfWeek(string input)
        {
            List<DayOfWeek> daysOfWeek = new List<DayOfWeek>();

            // Chia input theo dấu phẩy và chuyển thành các giá trị DayOfWeek
            string[] tokens = input.Split(',');
            foreach (string token in tokens)
            {
                if (int.TryParse(token.Trim(), out int dayOfWeekValue) && Enum.IsDefined(typeof(DayOfWeek), dayOfWeekValue))
                {
                    daysOfWeek.Add((DayOfWeek)dayOfWeekValue);
                }
            }

            return daysOfWeek;
        }

        public string ConvertToDaysOfWeeks(string input)
        {
            string result = string.Empty;
            List<string> daysOfWeek = new List<string>();

            string[] tokens = input.Split(',');
            foreach (string token in tokens)
            {
                if (Enum.TryParse(typeof(DayOfWeek), token.Trim(), true, out var dayOfWeekValue))
                {
                    daysOfWeek.Add(((DayOfWeek)dayOfWeekValue).ToString());
                }
            }

            result = string.Join(" | ", daysOfWeek);
            return result;
        }
    }
}
