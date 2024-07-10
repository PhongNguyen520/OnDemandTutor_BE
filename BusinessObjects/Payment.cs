using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class Payment
    {
        public string Id { get; set; } = string.Empty;
        public double RequiredAmount { get; set; }
        public string Description { get; set; } = string.Empty;
        public string CurrencyCode { get; set; } = string.Empty;
        public string TxnRef { get; set; } = string.Empty;
        public DateTime? CreateDate { get; set; } = DateTime.Now;
        public DateTime? ExpireDate { get; set; } = DateTime.Now.AddMinutes(30);
        public string? Signature { get; set; } = string.Empty;
        public bool? Status { get; set; } 
        public string? WalletId { get; set; } = string.Empty;
        public string? PaymentDestinationId { get; set; } = string.Empty;

        public virtual Wallet Wallet{ get; set; } = null!;

        public virtual PaymentDestination PaymentDestination { get; set; } = null!;

        public virtual ICollection<PaymentTransaction> PaymentTransactions { get; set; } = new List<PaymentTransaction>();
    }
}
