using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.FindFormModel
{
    public class RequestCreateFormFindTutor
    {
        public string GradeId {  get; set; } = string.Empty;
        public string SubjectGroupId { get; set; } = string.Empty;
        public string Tittle { get; set; } = string.Empty;
        public DateTime DayStart { get; set; }
        public DateTime DayEnd { get; set; }
        public string DayOfWeek {  get; set; } = string.Empty;
        public int TimeStart { get; set; }
        public int TimeEnd { get; set; }
        public double? MinHourlyRate { get; set; }
        public double? MaxHourlyRate { get; set; }
        public string? TypeOfDegree { get; set; }
        public string DescribeTutor { get; set; } = string.Empty;
        public bool? TutorGender { get; set; }
    }
}
