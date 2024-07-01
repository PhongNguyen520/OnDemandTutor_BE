using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IPaymentTransactionRepository
    {
        public bool AddTransaction(PaymentTransaction transaction);

        public bool DelTransaction(int id);

        public List<PaymentTransaction> GetTransactions();

        public bool UpdateTransaction(PaymentTransaction transaction);
    }
}
