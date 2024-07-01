using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IPaymentSignatureRepository
    {
        public bool AddSignature(PaymentSignature signature);

        public bool DelSignature(int id);

        public List<PaymentSignature> GetSignatures();

        public bool UpdateSignature(PaymentSignature signature);
    }
}
