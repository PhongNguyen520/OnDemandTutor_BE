using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IMerchantRepository
    {
        public bool AddMerchant(Merchant merchant);

        public bool DelMerchant(int id);

        public List<Merchant> GetMerchants();

        public bool UpdateMerchant(Merchant merchant);
        
    }
}
