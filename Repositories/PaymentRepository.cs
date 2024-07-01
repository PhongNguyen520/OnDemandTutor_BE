using BusinessObjects;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentDAO paymentDAO = null;
        public PaymentRepository() {
            if(paymentDAO == null)
            {
                paymentDAO = new PaymentDAO();
            }
        }
        public bool AddPayment(Payment payment)
        {
            return paymentDAO.AddPayment(payment);
        }

        public bool DelPayment(int id)
        {
            return paymentDAO.DelPayment(id);
        }

        public List<Payment> GetPayments()
        {
            return paymentDAO.GetPayments();
        }

        public bool UpdatePayment(Payment payment)
        {
            return paymentDAO.UpdatePayment(payment);
        }
    }
}
