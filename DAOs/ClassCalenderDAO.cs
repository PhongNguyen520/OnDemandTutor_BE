using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class ClassCalenderDAO
    {
        private DbContext dbContext = null;

        public ClassCalenderDAO()
        {
            if (dbContext == null)
            {
                dbContext = new DbContext();
            }
        }

        public bool AddClassCalenders(ClassCalender calender)
        {
            dbContext.ClassCalenders.Add(calender);
            dbContext.SaveChanges();
            return true;
        }

        public bool DelClassCalenders(int id)
        {
            ClassCalender calender = dbContext.ClassCalenders.Find(id);
            dbContext.ClassCalenders.Remove(calender);
            dbContext.SaveChanges();
            return true;
        }

        public List<ClassCalender> GetClassCalenders()
        {
            return dbContext.ClassCalenders.ToList();
        }

        public bool UpdateClassCalenders(ClassCalender calender)
        {
            dbContext.ClassCalenders.Update(calender);
            dbContext.SaveChanges();
            return true;
        }
    }
}
