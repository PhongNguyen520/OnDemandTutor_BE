using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PaymentTransactionService : IPaymentTransactionService
    {
        private readonly IPaymentTransactionRepository paymentTransactionRepository;

        public PaymentTransactionService()
        {
            if (paymentTransactionRepository == null)
            {
                paymentTransactionRepository = new PaymentTransactionRepository();
            }
        }
        public bool AddTransaction(PaymentTransaction transaction)
        {
           return paymentTransactionRepository.AddTransaction(transaction);
        }

        public bool DelTransaction(int id)
        {
            return paymentTransactionRepository.DelTransaction(id);
        }

        public List<PaymentTransaction> GetTransactions()
        {
            return paymentTransactionRepository.GetTransactions();
        }

        public bool UpdateTransaction(PaymentTransaction transaction)
        {
            return paymentTransactionRepository.UpdateTransaction(transaction);
        }
    }
}
