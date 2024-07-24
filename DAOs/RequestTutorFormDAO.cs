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
            return dbContext.RequestTutorForms.Include("Student")
                                              .Include("Subject")
                                              .Include("Tutor").ToList();
        }

        public bool UpdateRequestTutorForms(RequestTutorForm form)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var trackedEntity = dbContext.RequestTutorForms.Local
                                       .FirstOrDefault(f => f.FormId == form.FormId);
                    if (trackedEntity != null)
                    {
                        dbContext.Entry(trackedEntity).State = EntityState.Detached;
                    }

                    dbContext.RequestTutorForms.Update(form);
                    dbContext.SaveChanges();

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Log the exception or handle it as needed
                    return false;
                }
            }
        }
    }
}
