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
        private DbContext _dbContext;

        public ClassCalenderDAO()
        {
            if (_dbContext == null)
            {
                _dbContext = new DbContext();
            }
        }

        public bool AddClassCalenders(ClassCalender calender)
        {
            _dbContext.ClassCalenders.Add(calender);
            _dbContext.SaveChanges();
            return true;
        }

        public bool DelClassCalenders(int id)
        {
            ClassCalender calender = _dbContext.ClassCalenders.Find(id);
            _dbContext.ClassCalenders.Remove(calender);
            _dbContext.SaveChanges();
            return true;
        }

        public List<ClassCalender> GetClassCalenders()
        {
            return _dbContext.ClassCalenders.ToList();
        }

        public bool UpdateClassCalenders(ClassCalender calender)
        {
            _dbContext.ClassCalenders.Update(calender);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
