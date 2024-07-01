using BusinessObjects;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PaymentSignatureRepository : IPaymentSignatureRepository
    {
        private readonly PaymentSignatureDAO paymentSignatureDAO;

        public PaymentSignatureRepository()
        {
            if (paymentSignatureDAO == null)
            {
                paymentSignatureDAO = new PaymentSignatureDAO();
            }
        }
        public bool AddSignature(PaymentSignature signature)
        {
            return paymentSignatureDAO.AddSignature(signature);
        }

        public bool DelSignature(int id)
        {
            return paymentSignatureDAO.DelSignature(id);
        }

        public List<PaymentSignature> GetSignatures()
        {
            return paymentSignatureDAO.GetSignatures();
        }

        public bool UpdateSignature(PaymentSignature signature)
        {
            return paymentSignatureDAO.UpdateSignature(signature);
        }
    }
}
