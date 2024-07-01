using BusinessObjects;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class PaymentNotificationRepository : IPaymentNotificationRepository
    {
        private readonly PaymentNotificationDAO paymentNotificationDAO;

        public PaymentNotificationRepository()
        {
            if (paymentNotificationDAO == null)
            {
                paymentNotificationDAO = new PaymentNotificationDAO();
            }
        }
        public bool AddNotification(PaymentNotification notification)
        {
            return paymentNotificationDAO.AddNotification(notification);
        }

        public bool DelNotification(int id)
        {
            return paymentNotificationDAO.DelNotification(id);
        }

        public List<PaymentNotification> GetNotifications()
        {
            return paymentNotificationDAO.GetNotifications();
        }

        public bool UpdateNotification(PaymentNotification notification)
        {
            return paymentNotificationDAO.UpdateNotification(notification);
        }
    }
}
