using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IPaymentRepository
    {
        public bool AddPayment(Payment payment);

        public bool DelPayment(int id);

        public List<Payment> GetPayments();

        public bool UpdatePayment(Payment payment);
       
    }
}
