using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class PaymentNotification
    {
        public string Id { get; set; } = string.Empty;
        public string PaymentRefId { get; set; } = string.Empty;
        public DateTime? NotiDate { get; set; }
        public string? NotiContent { get; set; } = string.Empty;
        public decimal NotiAmount { get; set; }
        public string? NotiMessage { get; set; } = string.Empty;
        public string? NotiSignature { get; set; } = string.Empty;
        public string? NotiStatus { get; set; } = string.Empty;
        public DateTime? NotiResDate { get; set; }
        public string? NotiResMessage { get; set; } = string.Empty;
        public string? NotiResHttpCode { get; set; } = string.Empty;

        public string? NotiMerchantId { get; set; } = string.Empty;
        public string? NotiPaymentId { get; set; } = string.Empty;

        public virtual Merchant Merchant { get; set; } = null!;

        public virtual Payment Payment { get; set; } = null!;
    }
}
