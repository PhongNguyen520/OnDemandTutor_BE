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
        public string? CardType { get; set; } = string.Empty;
        public string? TxnRef { get; set; } = string.Empty;
        public string? BankTranNo { get; set; } = string.Empty;
        public string? TranStatus { get; set; } = string.Empty;
        public string? ResponseCode { get; set; } = string.Empty;
        public bool? IsValid{ get; set; }
        public string? PaymentId { get; set; } = string.Empty;
        public virtual Payment Payment { get; set; } = null!;
    }
}
