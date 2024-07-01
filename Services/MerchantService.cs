using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class MerchantService : IMerchantService
    {
        private readonly IMerchantRepository imerchantRepository;

        public MerchantService()
        {
            if(imerchantRepository == null)
            {
                imerchantRepository = new MerchantRepository();
            }
        }
        public bool AddMerchant(Merchant merchant)
        {
            return imerchantRepository.AddMerchant(merchant);
        }

        public bool DelMerchant(int id)
        {
            return imerchantRepository.DelMerchant(id);
        }

        public List<Merchant> GetMerchants()
        {
            return imerchantRepository.GetMerchants();
        }

        public bool UpdateMerchant(Merchant merchant)
        {
            return imerchantRepository.UpdateMerchant(merchant);
        }
    }
}
