using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class CreateWallet
    {
        public DateTime CreateDay { get; set; }

        public int? Balance { get; set; }

        public string? BankName { get; set; } = null!;

        public int BankNumber { get; set; }

    }
}
