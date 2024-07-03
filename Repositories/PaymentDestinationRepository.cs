using BusinessObjects;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PaymentDestinationRepository : IPaymentDestinationRepository
    {
        private readonly PaymentDestinationDAO paymentDestinationDAO;

        public PaymentDestinationRepository()
        {
            if (paymentDestinationDAO == null)
            {
                paymentDestinationDAO = new PaymentDestinationDAO();
            }
        }
        public bool AddDestination(PaymentDestination destination)
        {
            return paymentDestinationDAO.AddDestination(destination);
        }

        public bool DelDestination(int id)
        {
            return paymentDestinationDAO.DelDestination(id);
        }

        public List<PaymentDestination> GetDestinations()
        {
            return paymentDestinationDAO.GetDestinations();
        }

        public bool UpdateDestination(PaymentDestination destination)
        {
            return paymentDestinationDAO.UpdateDestination(destination);
        }
    }
}
