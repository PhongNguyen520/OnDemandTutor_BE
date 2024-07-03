using BusinessObjects;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PaymentTransactionRepository : IPaymentTransactionRepository
    {
        private readonly PaymentTransactionDAO paymentTransactionDAO;

        public PaymentTransactionRepository()
        {
            if (paymentTransactionDAO == null)
            {
                paymentTransactionDAO = new PaymentTransactionDAO();
            }
        }
        public bool AddTransaction(PaymentTransaction transaction)
        {
            return paymentTransactionDAO.AddTransaction(transaction);
        }

        public bool DelTransaction(int id)
        {
            return paymentTransactionDAO.DelTransaction(id);
        }

        public List<PaymentTransaction> GetTransactions()
        {
            return paymentTransactionDAO.GetTransactions();
        }

        public bool UpdateTransaction(PaymentTransaction transaction)
        {
            return paymentTransactionDAO.UpdateTransaction(transaction);
        }
    }
}
