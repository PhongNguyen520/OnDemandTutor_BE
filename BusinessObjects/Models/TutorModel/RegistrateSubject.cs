using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.TutorModel
{
    public class RegistrateSubject
    {
        public string TutorId { get; set; } = string.Empty;
        public string SubjectGroupId { get; set; } = string.Empty;
        public List<string> GradeId { get; set; } = new List<string>();
    }
}
