using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.TutorModel
{
    public class TutorDetail
    {
        public string AccountId { get; set; } = string.Empty;
        public string TutorId { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
        public string? Photo { get; set; }
        public string FullName { get; set; } = string.Empty;
        public bool Gender { get; set; }
        public string? Headline { get; set; }
        public string? Description { get; set; }
        public List<string> SubjectTutors { get; set; } = new List<string>();
        public string TypeOfDegree { get; set; } = string.Empty;
        public string Education { get; set; } = string.Empty;
        public double HourlyRate { get; set; }
        public string? Address { get; set; }
        public double Start { get; set; }
        public double Ratings { get; set; }

    }
}
