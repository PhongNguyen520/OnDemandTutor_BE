using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class ComplaintDTO
    {
        public DateTime CreateDay { get; set; }

        public string Description { get; set; } = null!;

        public string ClassId { get; set; } = null!;

        public string? Complainter { get; set; }

    }

    public class ComlaintClass()
    {
        public string ComplaintId { get; set; } = null!;
        public string ClassId { get; set; } = null!;
        public string? Complainter { get; set; }
    }
}

