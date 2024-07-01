using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using DAOs;
using Services;
using API.Services;
using BusinessObjects.Models;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComplaintsController : ControllerBase
    {
        private readonly IComplaintService _iComplaintService;
        private readonly ICurrentUserService _currentUserService;

        public ComplaintsController(IComplaintService complaintService, ICurrentUserService currentUserService)
        {
            _iComplaintService = complaintService;
            _currentUserService = currentUserService;
        }

        // GET: api/Complaints
        [HttpGet]
        public IActionResult GetComplaints()
        {
            return Ok(_iComplaintService.GetComplaints());
        }

        // GET: api/Complaints/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Complaint>> GetComplaint(string id)
        //{
        //    var complaint = await _context.Complaints.FindAsync(id);

        //    if (complaint == null)
        //    {
        //        return NotFound();
        //    }

        //    return complaint;
        //}

        //// PUT: api/Complaints/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutComplaint(string id, Complaint complaint)
        //{
        //    if (id != complaint.ComplaintId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(complaint).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!ComplaintExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Complaints
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Complaint>> PostComplaint(Complaint complaint)
        //{
        //    _context.Complaints.Add(complaint);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (ComplaintExists(complaint.ComplaintId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetComplaint", new { id = complaint.ComplaintId }, complaint);
        //}

        //// DELETE: api/Complaints/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteComplaint(string id)
        //{
        //    var complaint = await _context.Complaints.FindAsync(id);
        //    if (complaint == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Complaints.Remove(complaint);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool ComplaintExists(string id)
        //{
        //    return _context.Complaints.Any(e => e.ComplaintId == id);
        //}

        [HttpPost("CreateComplaint")]
        public async Task<IActionResult> CreateComplaint(ComplaintDTO complaintDTO)
        {
            complaintDTO.Complainter = _currentUserService.GetUserId().ToString();
            var result = await _iComplaintService.CreateComplaint(complaintDTO);
            if (result)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("ViewAllComplaintInClass/{classId}")]
        public async Task<IActionResult> ViewComplaint(string classId)
        {
            var result = await _iComplaintService.ViewAllComplaintInClass(classId);
            return Ok(result);
        }
    }
}
