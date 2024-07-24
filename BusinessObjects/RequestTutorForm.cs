using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class RequestTutorForm
    {
        public string FormId { get; set; } = null!;
        public DateTime CreateDay { get; set; }
        public DateTime DayStart { get; set; }
        public DateTime DayEnd { get; set; }
        public string DayOfWeek { get; set; } = string.Empty;
        public int TimeStart { get; set; }
        public int TimeEnd { get; set; }
        public string Description { get; set; } = string.Empty;
        public bool? Status { get; set; }
        public bool? IsActive { get; set; }
        public string SubjectId { get; set; } = null!;
        public string TutorId { get; set; } = null!;
        public string StudentId { get; set; } = null!;
        public virtual Student Student { get; set; } = null!;
        public virtual Subject Subject { get; set; } = null!;
        public virtual Tutor Tutor { get; set; } = null!;
    }
}
