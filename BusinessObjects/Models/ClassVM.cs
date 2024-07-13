using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class CreateClassVM
    {
        public string ClassName { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FormId { get; set; } = string.Empty;
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
        public bool? Status { get; set; }
        public bool? IsApprove { get; set; }
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
        public string Id { get; set; } = string.Empty;
        public string BookDay { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string ClassId { get; set; } = string.Empty;
    }

    public class Form
    {
        public string FormId { get; set; } = string.Empty;
        public string DayOfWeek { get; set; } = string.Empty;
        public DateTime DayStart { get; set; }
        public DateTime DayEnd { get; set; }
        public int TimeStart { get; set; }
        public int TimeEnd { get; set; }
        public string StudentId { get; set; } = string.Empty;
        public string SubjectId { get; set; } = string.Empty;
    }

    public class HandleCreateForm
    {
        public string DayOfWeek { get; set; } = string.Empty;
        public DateTime DayStart { get; set; }
        public DateTime DayEnd { get; set; }
        public int TimeStart { get; set; }
        public int TimeEnd { get; set; }
        public string StudentId { get; set; } = string.Empty;
    }
}
