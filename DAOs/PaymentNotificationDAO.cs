using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOs
{
    public class PaymentNotificationDAO
    {
        private readonly DbContext _dbContext;

        public PaymentNotificationDAO()
        {
            _dbContext = new DbContext();
        }

        public bool AddNotification(PaymentNotification notification)
        {
            _dbContext.PaymentNotifications.Add(notification);
            _dbContext.SaveChanges();
            return true;
        }

        public bool DelNotification(int id)
        {
            PaymentNotification notification = _dbContext.PaymentNotifications.Find(id);
            _dbContext.PaymentNotifications.Remove(notification);
            _dbContext.SaveChanges();
            return true;
        }

        public List<PaymentNotification> GetNotifications()
        {
            return _dbContext.PaymentNotifications.OrderByDescending(x => x.Id).ToList();
        }

        public bool UpdateNotification(PaymentNotification notification)
        {
            _dbContext.PaymentNotifications.Update(notification);
            _dbContext.SaveChanges();
            return true;
        }
    }
}
