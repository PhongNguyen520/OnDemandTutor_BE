using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class CreateClassByRequestVM
    {
        public string ClassName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        // StudentID == userId của Student
        public string FormId { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;
    }

    public class CreateClassByFindVM
    {
        public string ClassName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        // StudentID == userId của Student
        public string FormId { get; set; } = string.Empty;
        public string StudentId { get; set; } = string.Empty;

        public string TutorId {  get; set; } = string.Empty;
    }

    public class ClassVM
    {
        public string Classid { get; set; } = string.Empty;
        public string? ClassName { get; set; }
        public string Createday { get; set; } = string.Empty;
        public string DayStart { get; set; } = string.Empty;
        public string DayEnd { get; set; } = string.Empty;
        public string? Description { get; set; }
        public double Price { get; set; }
        public string? SubjectName { get; set; }
        public string? UserId { get; set; }
        public string? FullName { get; set; }
        public string? Avatar {  get; set; }
    }

    public class ClassDetail
    {
        public string ClassId { get; set; } = string.Empty;
        public string? ClassName { get; set; }
        public string? Description { get; set; }
        public string? SubjectName { get; set; }
        public List<CalenderVM> Calenders { get; set; } = new List<CalenderVM>();
    }

    public class CalenderVM
    {
        public string BookDay { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string ClassId { get; set; } = string.Empty;
    }
}
