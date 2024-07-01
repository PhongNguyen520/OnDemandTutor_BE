using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PaymentSignatureService : IPaymentSignatureService
    {
        private readonly IPaymentSignatureRepository iPaymentSignatureRepository;

        public PaymentSignatureService()
        {
            if (iPaymentSignatureRepository == null)
            {
                iPaymentSignatureRepository = new PaymentSignatureRepository();
            }
        }
        public bool AddSignature(PaymentSignature signature)
        {
            return iPaymentSignatureRepository.AddSignature(signature);
        }

        public bool DelSignature(int id)
        {
            return iPaymentSignatureRepository.DelSignature(id);
        }

        public List<PaymentSignature> GetSignatures()
        {
            return iPaymentSignatureRepository.GetSignatures();
        }

        public bool UpdateSignature(PaymentSignature signature)
        {
            return iPaymentSignatureRepository.UpdateSignature(signature);
        }
    }
}
