using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class StudentDTO
    { 
        public string? SchoolName { get; set; }

        public string? Address { get; set; }

        public int? Age { get; set; }

        public bool IsParent { get; set; }

        public string AccountId { get; set; }
    }
}
