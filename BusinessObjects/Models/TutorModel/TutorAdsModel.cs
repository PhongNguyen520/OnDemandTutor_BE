using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models.TutorModel
{
    public class TutorAdsModel
    {
        public string Title { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }
        public string? VideoUrl { get; set; }
    }
}
