using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class PaymentSignatureDAO
    {
        private readonly DbContext dbContext;
        public PaymentSignatureDAO()
        {
            if (dbContext == null)
            {
                dbContext = new DbContext();
            }
        }

        public bool AddSignature(PaymentSignature signature)
        {
            dbContext.PaymentSignatures.Add(signature);
            dbContext.SaveChanges();
            return true;
        }

        public bool DelSignature(int id)
        {
            PaymentSignature signature  = dbContext.PaymentSignatures.Find(id);
            dbContext.PaymentSignatures.Remove(signature);
            dbContext.SaveChanges();
            return true;
        }

        public List<PaymentSignature> GetSignatures()
        {
            return dbContext.PaymentSignatures.OrderByDescending(x => x.Id).ToList();
        }

        public bool UpdateSignature(PaymentSignature signature)
        {
            dbContext.PaymentSignatures.Update(signature);
            dbContext.SaveChanges();
            return true;
        }
    }
}
