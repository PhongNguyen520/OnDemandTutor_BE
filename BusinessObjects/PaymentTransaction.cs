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
        public string? TranMessage { get; set; } = string.Empty;
        public string? TranPayload { get; set; } = string.Empty;
        public string? TranStatus { get; set; } = string.Empty;
        public decimal? TranAmount { get; set; }
        public DateTime? TranDate { get; set; }
        public string? TranRefId { get; set; } = string.Empty;

        public string? PaymentId { get; set; } = string.Empty;

        public string? WalletId { get; set; } = null!;

        public virtual Payment Payment { get; set; } = null!;
        public virtual Wallet Wallet{ get; set; } = null!;
    }
}
