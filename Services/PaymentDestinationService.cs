using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PaymentDestinationService : IPaymentDestinationService
    {
        private readonly IPaymentDestinationRepository iPaymentDestinationRepository;
        public PaymentDestinationService() {
            if (iPaymentDestinationRepository == null)
            {
                iPaymentDestinationRepository = new PaymentDestinationRepository();
            }
        }
        public bool AddDestination(PaymentDestination destination)
        {
            return iPaymentDestinationRepository.AddDestination(destination);
        }

        public bool DelDestination(int id)
        {
            return iPaymentDestinationRepository.DelDestination(id);
        }

        public List<PaymentDestination> GetDestinations()
        {
            return iPaymentDestinationRepository.GetDestinations();
        }

        public bool UpdateDestination(PaymentDestination destination)
        {
            return iPaymentDestinationRepository.UpdateDestination(destination);
        }
    }
}
