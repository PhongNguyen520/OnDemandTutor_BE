using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.TutorModel
{
    public class TutorDetail
    {
        public string AccountId { get; set; }
        public string TutorId { get; set; }
        public string Avatar { get; set; }
        public string Photo { get; set; }
        public string FullName { get; set; }
        public bool Gender { get; set; }
        public string Headline { get; set; }
        public string Description { get; set; }
        public string TypeOfDegree { get; set; }
        public string Education { get; set; }
        public double HourlyRate { get; set; }
        public string Address { get; set; }
        public double Start { get; set; }
        public double Ratings { get; set; }

    }
}
