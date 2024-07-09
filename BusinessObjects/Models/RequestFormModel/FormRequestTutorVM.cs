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
        public string DayStart { get; set; } = string.Empty;
        public string DayEnd { get; set; } = string.Empty;
        public string DayOfWeek { get; set; } = string.Empty;
        public string TimeStart { get; set; } = string.Empty;
        public string TimeEnd { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string? Avatar { get; set; }
        public string? SubjectName { get; set; }
        public string? Description { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string TutorId { get; set; } = string.Empty;
    }

    public class FormMember
    {
        public string? FullName { get; set; }
        public string? Avatar { get; set; }
        public List<RequestTutorForm> List { get; set; } = new List<RequestTutorForm>();
    }
}
