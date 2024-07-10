using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class RequestPayment
    {
        public string? WalletId { get; set; } = string.Empty;
        public string? PaymentDestinationId { get; set; } = string.Empty;
        public double Amount { get; set; }
        public string Description { get; set; }
    }
}
