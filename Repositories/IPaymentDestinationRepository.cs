using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IPaymentDestinationRepository
    {
        public bool AddDestination(PaymentDestination destination);

        public bool DelDestination(int id);

        public List<PaymentDestination> GetDestinations();

        public bool UpdateDestination(PaymentDestination destination);
    }
}
