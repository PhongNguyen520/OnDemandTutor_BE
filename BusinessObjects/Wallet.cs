﻿using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Wallet
{
    public string WalletId { get; set; } = null!;

    public DateTime CreateDay { get; set; }

    public float? Balance { get; set; }

    public string? BankName { get; set; }

    public string? BankNumber { get; set; }

    public string AccountId { get; set; } = null!;

    public virtual Account Account { get; set; } = null!;

    public virtual ICollection<PaymentTransaction> PaymentTransactions{ get; set; } = new List<PaymentTransaction>();

}
