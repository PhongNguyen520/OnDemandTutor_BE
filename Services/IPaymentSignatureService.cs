using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPaymentSignatureService
    {
        public bool AddSignature(PaymentSignature signature);

        public bool DelSignature(int id);

        public List<PaymentSignature> GetSignatures();

        public bool UpdateSignature(PaymentSignature signature);
    }
}
