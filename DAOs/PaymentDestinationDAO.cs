using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class PaymentDestinationDAO
    {
        private readonly DbContext dbContext;
        public PaymentDestinationDAO() {
            if (dbContext == null)
            {
                dbContext = new DbContext();
            }
        }

        public bool AddDestination(PaymentDestination destination)
        {
            dbContext.PaymentDestinations.Add(destination);
            dbContext.SaveChanges();
            return true;
        }

        public bool DelDestination(int id)
        {
            PaymentDestination destination = dbContext.PaymentDestinations.Find(id);
            dbContext.PaymentDestinations.Remove(destination);
            dbContext.SaveChanges();
            return true;
        }

        public List<PaymentDestination> GetDestinations()
        {
            return dbContext.PaymentDestinations.OrderByDescending(x => x.Id).ToList();
        }

        public bool UpdateDestination(PaymentDestination destination)
        {
            dbContext.PaymentDestinations.Update(destination);
            dbContext.SaveChanges();
            return true;
        }
    }
}
