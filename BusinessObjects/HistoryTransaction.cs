using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects
{
    public class HistoryTransaction
    {
        public string HistoryId { get; set; }
        public float? Amount { get; set; }
        public DateTime DateCreate { get; set; }
        public string? Description { get; set; }
        public string? CardType { get; set; }
        public string? BackTranNo { get; set; }
        public string WalletId { get; set; }
        public virtual Wallet Wallet { get; set; }
    }
}
