using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class TutorApplyDAO
    {
        //private readonly DbContext dbContext = null;

        //public RoleDAO()
        //{
        //    if (dbContext == null)
        //    {
        //        dbContext = new DbContext();
        //    }
        //}
        //public bool AddRole(Role role)
        //{
        //    dbContext.Roles.Add(role);
        //    dbContext.SaveChanges();
        //    return true;
        //}
        public TutorApplyDAO()
        {
            if (dbContext == null)
            {
                dbContext = new DbContext();
            }
        }
        public bool AddClass(TutorApply tutorApply)
        {
            dbContext.TutorApplies.Add(tutorApply);
            dbContext.SaveChanges();
            return true;
        }

        //public bool DelRoles(int id)
        //{
        //    Role role = dbContext.Roles.Find(id);
        //    dbContext.Roles.Remove(role);
        //    dbContext.SaveChanges();
        //    return true;
        //}
        public bool DelTutorApplies(int id)
        {
            TutorApply tutorApply = dbContext.TutorApplies.Find(id);
            dbContext.TutorApplies.Remove(tutorApply);
            dbContext.SaveChanges();
            return true;
        }

        //public List<Role> GetRoles()
        //{
        //    return dbContext.Roles.OrderByDescending(x => x.RoleId).ToList();
        //}
        public List<TutorApply> GetTutorApplies()
        {
            return dbContext.TutorApplies.ToList();
        }

        //public bool UpdateRoles(Role role)
        //{
        //    dbContext.Roles.Update(role);
        //    dbContext.SaveChanges();
        //    return true;
        //}
        public bool UpdateTutorApplies(TutorApply tutorApply)
        {
            dbContext.TutorApplies.Update(tutorApply);
            dbContext.SaveChanges();
            return true;
        }
    }
}
