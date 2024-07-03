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
        public string CreateDay { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? SubjectName { get; set; }
        public double Start { get; set; }
        public string? Title { get; set; }

    }

    public class CreateFeedback
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set;} = string.Empty;
        public string TutorId { get; set; } = string.Empty;
        public string ClassId { get; set; } = string.Empty;
        public int Star { get; set; }
    }
}
