using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class ClassVM
    {
        public string ClassName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double HourPerDay { get; set; }
        public int DayPerWeek { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string GradeId { get; set; } = string.Empty;
        public string SubjectGroupId { get; set; } = string.Empty;
    }
}
