using BusinessObjects;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class MerchantRepository : IMerchantRepository
    {
        private readonly MerchantDAO merchantDAO;

        public MerchantRepository()
        {
            if (merchantDAO == null)
            {
                merchantDAO = new MerchantDAO();
            }
        }

        public bool AddMerchant(Merchant merchant)
        {
            return merchantDAO.AddMerchant(merchant);
        }

        public bool DelMerchant(int id)
        {
            return merchantDAO.DelMerchant(id);
        }

        public List<Merchant> GetMerchants()
        {
            return merchantDAO.GetMerchants();
        }

        public bool UpdateMerchant(Merchant merchant)
        {
            return merchantDAO.UpdateMerchant(merchant);
        }
    }
}
