using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPaymentService
    {
        public bool AddPayment(Payment payment);

        public bool DelPayment(int id);

        public List<Payment> GetPayments();

        public bool UpdatePayment(Payment payment);
    }
}
