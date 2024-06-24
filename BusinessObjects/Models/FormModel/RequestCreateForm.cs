using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.FormModel
{
    public class RequestCreateForm
    {
        public string GradeId {  get; set; } = string.Empty;
        public string SubjectGroupId { get; set; } = string.Empty;
        public string Tittle { get; set; } = string.Empty;
        public double? MinHourlyRate { get; set; }
        public double? MaxHourlyRate { get; set; }
        public string? TypeOfDegree { get; set; }
        public string DescribeTutor { get; set; } = string.Empty;
        public bool? TutorGender { get; set; }
    }
}
