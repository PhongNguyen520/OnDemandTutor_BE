using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.FormModel
{
    public class FormVM
    {
        public string FormId { get; set; }
        public DateTime CreateDay { get; set; }
        public string FullName { get; set; }
        public string Tittle { get; set; } = string.Empty;
        public double MinHourlyRate { get; set; }
        public double MaxHourlyRate { get; set; }
        public string SubjectName { get; set; }
        public string Description { get; set; }
        public bool TutorGender { get; set; }
        public string SubjectId { get; set; }
        public string StudentId { get; set; }
    }
}
