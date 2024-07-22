using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class TutorDTO
    {
        public DateTime Dob { get; set; }

        public string Education { get; set; } = null!;

        public string TypeOfDegree { get; set; } = null!;

        public int CardId { get; set; }

        public float HourlyRate { get; set; }

        public string? Photo { get; set; }

        public string? Headline { get; set; }

        public string? Description { get; set; }

        public string? Address { get; set; }

        public bool? IsActive { get; set; }

        public string AccountId { get; set; } = null!;
    }

    public class IsActiveTutor
    {
        public string AccountId { get; set; }
        public bool Status { get; set; }
    }
}
