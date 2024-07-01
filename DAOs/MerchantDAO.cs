using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class MerchantDAO
    {
        private readonly DbContext dbContext = null;

        public MerchantDAO()
        {

            if (dbContext == null)
            {
                dbContext = new DbContext();
            }
        }

        public bool AddMerchant(Merchant merchant)
        {
            dbContext.Merchants.Add(merchant);
            dbContext.SaveChanges();
            return true;
        }

        public bool DelMerchant(int id)
        {
            Merchant merchant = dbContext.Merchants.Find(id);
            dbContext.Merchants.Remove(merchant);
            dbContext.SaveChanges();
            return true;
        }

        public List<Merchant> GetMerchants()
        {
            return dbContext.Merchants.OrderByDescending(x => x.Id).ToList();
        }

        public bool UpdateMerchant(Merchant merchant)
        {
            dbContext.Merchants.Update(merchant);
            dbContext.SaveChanges();
            return true;
        }
    }
}
