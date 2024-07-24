using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class PaymentTransactionDAO
    {
        private readonly DbContext dbContext;
        public PaymentTransactionDAO()
        {
            if (dbContext == null)
            {
                dbContext = new DbContext();
            }
        }

        public bool AddTransaction(PaymentTransaction transaction)
        {
            dbContext.PaymentTransactions.Add(transaction);
            dbContext.SaveChanges();
            return true;
        }

        public bool DelTransaction(int id)
        {
            PaymentTransaction transaction = dbContext.PaymentTransactions.Find(id);
            dbContext.PaymentTransactions.Remove(transaction);
            dbContext.SaveChanges();
            return true;
        }

        public List<PaymentTransaction> GetTransactions()
        {
            return dbContext.PaymentTransactions.ToList();
        }

        public bool UpdateTransaction(PaymentTransaction transaction)
        {
            dbContext.PaymentTransactions.Update(transaction);
            dbContext.SaveChanges();
            return true;
        }
    }
}
