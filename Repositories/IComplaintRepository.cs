using BusinessObjects;
using BusinessObjects.Models;
using DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IComplaintRepository
    {
        public bool AddComplaint(Complaint complaint);

        public bool DelComplaints(int id);

        public List<Complaint> GetComplaints();

        public bool UpdateComplaints(Complaint complaint);

        Task<bool> CreateComplaint(ComplaintDTO model);

        Task<IQueryable<ComplaintVM>> GetAllComplaintOfUser(string classId);

        Task<ComplaintVM> UpdateProcessnoteStatus(string complaintId, string proce, bool sta);
    }
}
