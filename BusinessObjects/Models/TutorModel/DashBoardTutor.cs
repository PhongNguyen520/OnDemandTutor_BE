using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.TutorModel
{
    public class DashBoardTutor
    {
        public string Title { get; set; } = string.Empty;
        public double Value { get; set; } = 0;
        public List<int> Data { get; set; } = new List<int>();
        public List<DateTime> Dates { get; set; } = new List<DateTime>();
    }
}
