using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class ClassCreate
    {
        public string ClassName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double HourPerDay { get; set; }
        public int DayPerWeek { get; set; }
        // StudentID == userId của Student
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
}
