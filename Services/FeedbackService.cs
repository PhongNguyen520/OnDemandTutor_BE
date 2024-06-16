using BusinessObjects;
using DAOs;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository iFeedbackRepository = null;

        public FeedbackService()
        {
            if (iFeedbackRepository == null)
            {
                iFeedbackRepository = new FeedbackRepository();
            }
        }
        public bool AddFeedback(Feedback feedback)
        {
            return iFeedbackRepository.AddFeedback(feedback);
        }

        public bool DelFeedbacks(int id)
        {
            return iFeedbackRepository.DelFeedbacks(id);
        }

        public List<Feedback> GetFeedbacks(string id)
        {
            return iFeedbackRepository.GetFeedbacks(id);
        }

        public bool UpdateFeedbacks(Feedback feedback)
        {
            return iFeedbackRepository.UpdateFeedbacks(feedback);
        }

        public double TotalStart(string id)
        {
            var query = iFeedbackRepository.GetFeedbacks(id);

            if (query.Count() <= 0)
            {
                return 0.00; // Trả về 0.00 nếu không có đánh giá nào
            }

            double totalRate = query.Sum(x => x.Rate);
            double averageRate = totalRate / query.Count();

            // Làm tròn đến hai chữ số thập phân
            double rate = Math.Round(averageRate, 2);

            return rate;
        }

        public int TotalRate(string id)
        {
            var query = iFeedbackRepository.GetFeedbacks(id);
            int rate = 0;
            if (query == null)
            {
                return 0;
            }
            rate = query.Count();
            return rate;
        }
    }
}
