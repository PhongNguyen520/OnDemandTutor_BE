using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class VnPayVM
    {
        public string PaymentId { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
    }
}
