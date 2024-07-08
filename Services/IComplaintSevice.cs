using BusinessObjects;
using BusinessObjects.Models;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IComplaintService
    {
        public bool AddComplaint(Complaint complaint);

        public bool DelComplaints(int id);

        public List<Complaint> GetComplaints();

        public bool UpdateComplaints(Complaint complaint);

        Task<bool> CreateComplaint(ComplaintDTO model);

        Task<IQueryable<ComplaintVM>> ViewAllComplaintInClass(string classId);

        Task<ComplaintVM> ModeratorComplaint(string complaintId, string proce, bool sta);

        Task<IQueryable<ComlaintClass>> ShowListComplaintClass();
    }
}
