using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.RequestFormModel
{
    public class FormRequestTutorVM
    {
        public required string FormId { get; set; }
        public string CreateDay { get; set; } = string.Empty;
        public DateTime DayStart { get; set; }
        public DateTime DayEnd { get; set; }
        public string DayOfWeek { get; set; } = string.Empty;
        public int TimeStart { get; set; }
        public int TimeEnd { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Avatar { get; set; }
        public string? SubjectName { get; set; }
        public string? Description { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string TutorId { get; set; } = string.Empty;
    }
}
