using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class RequestTutorFormDAO
    {
        private readonly DbContext dbContext = null;
        public RequestTutorFormDAO()
        {
            if (dbContext == null)
            {
                dbContext = new DbContext();
            }
        }

        public bool AddRequestTutorForm(RequestTutorForm form)
        {
            dbContext.RequestTutorForms.Add(form);
            dbContext.SaveChanges();
            return true;
        }

        public bool DelRequestTutorForms(int id)
        {
            RequestTutorForm form = dbContext.RequestTutorForms.Find(id);
            dbContext.RequestTutorForms.Remove(form);
            dbContext.SaveChanges();
            return true;
        }

        public List<RequestTutorForm> GetRequestTutorForms()
        {
            return dbContext.RequestTutorForms.OrderByDescending(x => x.FormId).ToList();
        }

        public bool UpdateRequestTutorForms(RequestTutorForm form)
        {
            var trackedEntity = dbContext.RequestTutorForms.Local
                           .FirstOrDefault(f => f.FormId == form.FormId);
            if (trackedEntity != null)
            {
                dbContext.Entry(trackedEntity).State = EntityState.Detached;
            }

            dbContext.RequestTutorForms.Update(form);
            dbContext.SaveChanges();
            return true;
        }
    }
}
