using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class TutorApply
    {
        public string TutorId { get; set; } = string.Empty;
        public string FormId { get; set; } = string.Empty;
        public DateTime DayApply { get; set; }
        public bool? IsApprove { get; set; }
        public virtual FindTutorForm FindTutorForm { get; set; } = null!;
        public virtual Tutor Tutor { get; set; } = null!;
    }
}
