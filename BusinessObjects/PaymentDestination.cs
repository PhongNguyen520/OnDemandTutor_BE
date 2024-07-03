using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class PaymentDestination
    {
        public string Id { get; set; } = string.Empty;
        public string? BankCode { get; set; } = string.Empty;
        public string? BankName { get; set; } = string.Empty;
        public string? BankLogo { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
    }
}
