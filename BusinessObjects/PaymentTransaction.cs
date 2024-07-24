using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class PaymentTransaction
    {
        public string Id { get; set; } = string.Empty;
        public float? Amount { get; set; }
        public string? Description { get; set; } = string.Empty;
        public DateTime? TranDate { get; set; } = DateTime.Now;

        public int? Type { get; set; } 
        public bool? IsValid { get; set; }
        public string? WalletId { get; set; } = string.Empty;
        public string? PaymentDestinationId { get; set; } = string.Empty;

        public virtual Wallet Wallet{ get; set; } = null!;

        public virtual PaymentDestination PaymentDestination { get; set; } = null!;

    }
}
