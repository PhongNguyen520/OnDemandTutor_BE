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
    public class ComplaintRepository : IComplaintRepository
    {
        private readonly ComplaintDAO complaintDAO = null;
        private readonly DAOs.DbContext _dbContext;
        private readonly IMapper _mapper;

        public ComplaintRepository(DAOs.DbContext dbContext, IMapper mapper)
        {
            if (complaintDAO == null)
            {
                complaintDAO = new ComplaintDAO();
            }
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool AddComplaint(Complaint complaint)
        {
            return complaintDAO.AddComplaint(complaint);
        }

        public bool DelComplaints(int id)
        {
            return complaintDAO.DelComplaints(id);
        }

        public List<Complaint> GetComplaints()
        {
            return complaintDAO.GetComplaints();
        }

        public bool UpdateComplaints(Complaint complaint)
        {
            return complaintDAO.UpdateComplaints(complaint);
        }

        public async Task<bool> CreateComplaint(ComplaintDTO model)
        {
            var complaintId = Guid.NewGuid().ToString();
            var tutor = await _dbContext.Classes
                                          .Include(_ => _.Tutor)
                                          .FirstOrDefaultAsync(_ => _.ClassId == model.ClassId);
            if (tutor == null)
            {
                return false;
            }
            var student = await _dbContext.Classes
                                            .Include(_ => _.Student)
                                            .FirstOrDefaultAsync(_ => _.ClassId == model.ClassId);
            var complaintEn = _mapper.Map<Complaint>(model);
            complaintEn.ComplaintId = complaintId;
            complaintEn.Status = null;
            complaintEn.TutorId = tutor.TutorId;
            complaintEn.StudentId = student.StudentId;
            complaintEn.Complainter = model.Complainter;
            complaintEn.Processnote = null;
            complaintEn.CreateDay = DateTime.Now;
            complaintEn.ProcessDate = DateTime.Now;

            _dbContext.Add(complaintEn);
            _dbContext.SaveChanges();

            return true;

        }

        public async Task<IQueryable<ComplaintVM>> GetAllComplaintOfUser(string classId)
        {
            var listComplaintOfUser = _dbContext.Complaints
                                                .Where(_ => _.ClassId == classId);
            var listVM = new List<ComplaintVM>();
            foreach (var x in listComplaintOfUser)
            {
                var complaintVM = _mapper.Map<ComplaintVM>(x);
                listVM.Add(complaintVM);
            }

            return listVM.AsQueryable();
        }

        public async Task<ComplaintVM> UpdateProcessnoteStatus (string complaintId, string proce, bool sta)
        {
            var complaintDb =await _dbContext.Complaints                                          
                                            .FirstOrDefaultAsync(_ => _.ComplaintId == complaintId);
            complaintDb.Status = sta;
            complaintDb.Processnote = proce;

            _dbContext.Update(complaintDb);
            _dbContext.SaveChanges();

            var result = _mapper.Map<ComplaintVM>(complaintDb);
            return result;
        }

        public async Task<IQueryable<ComlaintClass>> GetAllComplaintStatusNull()
        {
            var listCom = _dbContext.Complaints.Where(_ => _.Status == null);
            var enList = new List<ComlaintClass>();

            foreach (var x in listCom)
            {
                var comcla = _mapper.Map<ComlaintClass>(x);
                enList.Add(comcla);
            }
            return enList.AsQueryable();
        }
    }
}
