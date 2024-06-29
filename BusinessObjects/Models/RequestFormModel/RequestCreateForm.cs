using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.RequestFormModel
{
    public class RequestCreateForm
    {
        public string GradeId { get; set; } = string.Empty;
        public string SubjectGroupId { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DayStart { get; set; }
        public DateTime DayEnd { get; set; }
        public string DayOfWeek { get; set; } = string.Empty;
        public int TimeStart { get; set; }
        public int TimeEnd { get; set; }
        public string TutorId { get; set; } = string.Empty;
    }
}
