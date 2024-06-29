using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class CreateRequestTutor
    {
        public string ClassName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string DayOfWeek { get; set; } = string.Empty;
        public int TimeStart { get; set; }
        public int TimeEnd { get; set; }
        // StudentID == userId của Student
        public DateTime DayStart { get; set; }
        public DateTime DayEnd { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string GradeId { get; set; } = string.Empty;
        public string SubjectGroupId { get; set; } = string.Empty;
    }

    public class ClassVM
    {
        public string? ClassName { get; set; }
        public DateTime Createday { get; set; }
        public string? Description { get; set; }
        public double HourPerDay { get; set; }
        public int DayPerWeek { get; set; }
        public double Price { get; set; }
        public string? SubjectName { get; set; }
        public string? StudentName { get; set; }
        public string? StudentAvatar {  get; set; }
        public string? TutorName { get; set; }
        public string? TutorAvatar {  get; set; }
    }

    public class CalenderVM
    {
        public DateTime BookDay { get; set; }
        public int TimeStart { get; set; }
        public int TimeEnd { get; set; }
        public string ClassId { get; set; } = string.Empty;
    }
}
