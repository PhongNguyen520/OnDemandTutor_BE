using AutoMapper;
using BusinessObjects;
using BusinessObjects.Models;
using DAOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly FeedbackDAO feedbackDAO = null;
        private readonly DAOs.DbContext _context;
        private IMapper _mapper;

        public FeedbackRepository()
        {
            if (feedbackDAO == null)
            {
                feedbackDAO = new FeedbackDAO();
            }
        }

        public FeedbackRepository(DAOs.DbContext dbContext, IMapper mapper)
        {
            _context = dbContext;
            _mapper = mapper;
        }

        public bool AddFeedback(Feedback feedback)
        {
            return feedbackDAO.AddFeedback(feedback);
        }

        public bool DelFeedbacks(int id)
        {
            return feedbackDAO.DelFeedbacks(id);
        }

        public List<Feedback> GetFeedbacks(string id)
        {
            return _context.Feedbacks.Where(_ => _.FeedbackId == id).ToList();
        }

        public bool UpdateFeedbacks(Feedback feedback)
        {
            return feedbackDAO.UpdateFeedbacks(feedback);
        }

        public async Task<List<FeedbackVMPhuc>> GetAllFeedBack()
        {
            var listDB = await _context.Feedbacks.ToListAsync();
            return _mapper.Map<List<FeedbackVMPhuc>>(listDB);
        }
    }
}
