using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class ComplaintDTO
    {
        public string Description { get; set; } = null!;

        public string ClassId { get; set; } = null!;

        public string? Complainter { get; set; }

    }

    public class ComlaintClass
    {
        public string ComplaintId { get; set; } = null!;
        public string ClassId { get; set; } = null!;
        public string? Complainter { get; set; }
        public string NameAccount { get; set; }
        public DateTime DateCreate { get; set; }

    }

}

