using BusinessObjects;
using BusinessObjects.Models;
using BusinessObjects.Models.TutorModel;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PaymentTransactionService : IPaymentTransactionService
    {
        private readonly IPaymentTransactionRepository paymentTransactionRepository;

        public PaymentTransactionService()
        {
            if (paymentTransactionRepository == null)
            {
                paymentTransactionRepository = new PaymentTransactionRepository();
            }
        }
        public bool AddTransaction(PaymentTransaction transaction)
        {
            return paymentTransactionRepository.AddTransaction(transaction);
        }

        public bool DelTransaction(int id)
        {
            return paymentTransactionRepository.DelTransaction(id);
        }

        public List<PaymentTransaction> GetTransactions()
        {
            return paymentTransactionRepository.GetTransactions();
        }

        public bool UpdateTransaction(PaymentTransaction transaction)
        {
            return paymentTransactionRepository.UpdateTransaction(transaction);
        }

        public async Task<DashBoardAdmin> GetDashBoard(string id, DateTime createDay, int type, string title)
        {
            List<float?> listData = new List<float?>();
            DateTime currentDate = DateTime.Now;
            List<DateTime> dateTimes = new List<DateTime>();

            for (DateTime date = createDay; date <= currentDate; date = date.AddMonths(1))
            {
                dateTimes.Add(date);
            }

            foreach (var month in dateTimes)
            {
                var dataList = paymentTransactionRepository.GetTransactions()
                    .Where(s => s.TranDate?.Month == month.Month && s.TranDate?.Year == month.Year && s.IsValid == true && s.Type == type);
                float? data = 0;
                foreach (var item in dataList)
                {
                    if (item != null)
                    {
                        data += item?.Amount;
                    }
                }
                listData.Add(data);
            }

            DashBoardAdmin result = new DashBoardAdmin()
            {
                Title = title,
                Value = listData.Last(),
                Data = listData,
                Dates = dateTimes,
            };

            return result;
        }
    }
}
