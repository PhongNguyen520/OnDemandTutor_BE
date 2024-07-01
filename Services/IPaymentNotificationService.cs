using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IPaymentNotificationService
    {
        public bool AddNotification(PaymentNotification notification);

        public bool DelNotification(int id);

        public List<PaymentNotification> GetNotifications();

        public bool UpdateNotification(PaymentNotification notification);
    }
}
