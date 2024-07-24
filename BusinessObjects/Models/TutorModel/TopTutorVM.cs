using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.TutorModel
{
    public class TopTutorVM
    {
        public string TutorId { get; set; } = string.Empty;
        public int TotalHour { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Avatar { get; set; }
        public string? Headline { get; set; }
        public string? Description { get; set; }
        public List<string> SubjectTutors { get; set; } = new List<string>();
    }
}
