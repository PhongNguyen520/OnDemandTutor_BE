using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Wallet
{
    public string WalletId { get; set; } = null!;

    public DateTime CreateDay { get; set; }

    public float? Balance { get; set; }

    public string? BankName { get; set; }

    public int? BankNumber { get; set; }

    public string AccountId { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<Payment> Payments{ get; set; } = new List<Payment>();

    public virtual ICollection<HistoryTransaction> HistoryTransactions { get; set; } = new List<HistoryTransaction>();
}
