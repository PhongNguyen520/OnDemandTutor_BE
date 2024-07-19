using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class TutorAdIsAc
    {
        public string Id {  get; set; }
        public bool IsActive { get; set; }
    }

    public class TutorIsActiveVM
    {
        public string AdsId { get; set; } = null!;

        public DateTime CreateDay { get; set; }

        public string Video { get; set; } = null!;

        public string Image { get; set; } = null!;

        public string TutorId { get; set; } = null!;

        public string? Title { get; set; }

        public bool? IsActived { get; set; }

        public string AccountTutorId { get; set; }
    }
}
