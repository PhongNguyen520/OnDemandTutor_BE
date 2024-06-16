using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.FormModel
{
    public class RequestCreateForm
    {
        public string GradeId {  get; set; }
        public string SubjectGroupId { get; set; }
        public string Tittle { get; set; }
        public double MinHourlyRate { get; set; }
        public double MaxHourlyRate { get; set; }
        public string TypeOfDegree { get; set; }
        public string DescribeTutor { get; set; }
        public bool TutorGender { get; set; }
    }
}
