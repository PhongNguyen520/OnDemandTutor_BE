using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class PaymentDAO
    {
        private readonly DbContext dbContext = null;

        public PaymentDAO()
        {

            if (dbContext == null)
            {
                dbContext = new DbContext();
            }
        }

        public bool AddPayment(Payment payment)
        {
            dbContext.Payments.Add(payment);
            dbContext.SaveChanges();
            return true;
        }

        public bool DelPayment(int id)
        {
            Payment payment = dbContext.Payments.Find(id);
            dbContext.Payments.Remove(payment);
            dbContext.SaveChanges();
            return true;
        }

        public List<Payment> GetPayments()
        {
            return dbContext.Payments.OrderByDescending(x => x.Id).ToList();
        }

        public bool UpdatePayment(Payment payment)
        {
            dbContext.Payments.Update(payment);
            dbContext.SaveChanges();
            return true;
        }
    }
}

