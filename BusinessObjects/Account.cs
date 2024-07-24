using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Account : IdentityUser
{
    public string FullName { get; set; } = null!;

    public bool Gender { get; set; }

    public int PhoneNumber { get; set; }

    public string PhoneNumber { get; set; }

    public bool IsActive { get; set; }

    public String? RefreshToken { get; set; }

    public DateTime? DateExpireRefreshToken { get; set; }

    public string? Avatar { get; set; }

    public virtual ICollection<Message> Messages { get; set; } = new List<Message>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<Tutor> Tutors { get; set; } = new List<Tutor>();

    public virtual ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();

    public virtual ICollection<ConversationAccount> ConversationAccounts { get; set; } = new List<ConversationAccount>();

    public DateTime CreateDay { get; set; } = DateTime.Now;
}
