using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class ComplaintVM
    {
        public string? ComplaintId { get; set; }

        public DateTime CreateDay { get; set; }

        public string Description { get; set; } = null!;

        public string ClassId { get; set; } = null!;

        public string? Complainter { get; set; }

        public bool? Status { get; set; }

        public string? Processnote { get; set; }

        public DateTime? ProcessDate { get; set; }
    }
}
