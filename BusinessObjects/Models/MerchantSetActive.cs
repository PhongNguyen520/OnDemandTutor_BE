using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class MerchantSetActive
    {
        public string? Id { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}
