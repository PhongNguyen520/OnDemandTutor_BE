using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class FeedbackVM
    {
        public string FeedbackId { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
        public string FullName { get; set;} = string.Empty;
        public DateTime CreateDay { get; set; }
        public string Description { get; set; } = string.Empty;
        public string? SubjectName { get; set; }
        public double Start { get; set; }

    }
}
