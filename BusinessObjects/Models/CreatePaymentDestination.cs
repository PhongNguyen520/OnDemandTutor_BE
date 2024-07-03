using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Models
{
    public class CreatePaymentDestination
    {
        public string? BankName { get; set; } = string.Empty;
        public string? BankCode { get; set; } = string.Empty;
        public string? BankLogo { get; set; } = string.Empty;
      
    }
}
