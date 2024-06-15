using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class FeedbackVM
    {
        public string FeedbackId { get; set; }
        public string FullName { get; set;}
        public DateTime CreateDay { get; set; }
        public string Description { get; set;}
        public string SubjectName { get; set; }
        public double Start { get; set; }

    }
}
