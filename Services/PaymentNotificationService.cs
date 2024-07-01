using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PaymentNotificationService : IPaymentNotificationService
    {
        private readonly IPaymentNotificationRepository paymentNotificationRepository;

        public PaymentNotificationService()
        {
            if (paymentNotificationRepository == null)
            {
                paymentNotificationRepository = new PaymentNotificationRepository();
            }
        }
        public bool AddNotification(PaymentNotification notification)
        {
            return paymentNotificationRepository.AddNotification(notification);
        }

        public bool DelNotification(int id)
        {
            return paymentNotificationRepository.DelNotification(id);
        }

        public List<PaymentNotification> GetNotifications()
        {
            return paymentNotificationRepository.GetNotifications();
        }

        public bool UpdateNotification(PaymentNotification notification)
        {
            return paymentNotificationRepository.UpdateNotification(notification);
        }
    }
}
