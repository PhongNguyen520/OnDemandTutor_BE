using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class ClassCalender
    {
        [Required]
        public string CalenderId { get; set; }
        public DateTime DayOfWeek { get; set; }
        public int TimeStart { get; set; }
        public int TimeEnd { get; set; }
        public bool IsActive { get; set; }
        public string ClassId { get; set; }
        public virtual Class Classes { get; set; } = null!;
    }
}
