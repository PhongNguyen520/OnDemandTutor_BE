using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository ipaymentRepository;
        public PaymentService() {
            if (ipaymentRepository == null)
            {
                ipaymentRepository = new PaymentRepository();
            }
        }

        public bool AddPayment(Payment payment)
        {
           return ipaymentRepository.AddPayment(payment);
        }

        public bool DelPayment(int id)
        {
            return ipaymentRepository.DelPayment(id);
        }

        public List<Payment> GetPayments()
        {
            return ipaymentRepository.GetPayments();
        }

        public bool UpdatePayment(Payment payment)
        {
            return ipaymentRepository.UpdatePayment(payment);
        }
    }
}
