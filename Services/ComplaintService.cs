using BusinessObjects;
using BusinessObjects.Models;
using DAOs;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository iComplaintRepository;

        public ComplaintService(IComplaintRepository complaintRepository)
        {
            if (iComplaintRepository == null)
            {
                //iComplaintRepository = new ComplaintRepository();
            }
            iComplaintRepository = complaintRepository;

        }

        public bool AddComplaint(Complaint complaint)
        {
            return iComplaintRepository.AddComplaint(complaint);
        }

        public bool DelComplaints(int id)
        {
            return iComplaintRepository.DelComplaints(id);
        }

        public List<Complaint> GetComplaints()
        {
            return iComplaintRepository.GetComplaints();
        }

        public bool UpdateComplaints(Complaint complaint)
        {
            return iComplaintRepository.UpdateComplaints(complaint);
        }

        public async Task<bool> CreateComplaint(ComplaintDTO model)
        {
            return await iComplaintRepository.CreateComplaint(model);
        }

        public async Task<IQueryable<ComplaintVM>> ViewAllComplaintInClass(string classId)
        {
            return await iComplaintRepository.GetAllComplaintOfUser(classId);
        }

        public Task<ComplaintVM> ModeratorComplaint(string complaintId, string proce, bool sta)
        {
            return iComplaintRepository.UpdateProcessnoteStatus(complaintId, proce, sta);
        }

        public Task<List<ComlaintClass>> ShowListComplaintClass()
        {
            return iComplaintRepository.GetAllComplaintStatusNull();
        }
    }
}
