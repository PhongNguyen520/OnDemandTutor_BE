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
using NuGet.Protocol;
using BusinessObjects.Constrant;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = AppRole.Tutor)]
    public class TutorsController : ControllerBase
    {
        private readonly ITutorService iTutorService;

        public TutorsController()
        {
            iTutorService = new TutorService();
        }

        // GET: api/Tutors
        [HttpGet]
        public IActionResult GetTutors()
        {
            return Ok(iTutorService.GetTutors());
        }

        //// GET: api/Tutors/5
        [HttpGet("{id}")]
        public IActionResult GetTutor(string id)
        {
            var tutor = iTutorService.GetTutor(id);
            return Ok(tutor);
        }

        //// PUT: api/Tutors/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTutor(string id, Tutor tutor)
        //{
        //    if (id != tutor.TutorId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tutor).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TutorExists(id))
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

        //// POST: api/Tutors
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<Tutor>> PostTutor(Tutor tutor)
        //{
        //    _context.Tutors.Add(tutor);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (TutorExists(tutor.TutorId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetTutor", new { id = tutor.TutorId }, tutor);
        //}

        //// DELETE: api/Tutors/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteTutor(string id)
        //{
        //    var tutor = await _context.Tutors.FindAsync(id);
        //    if (tutor == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Tutors.Remove(tutor);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool TutorExists(string id)
        //{
        //    return _context.Tutors.Any(e => e.TutorId == id);
        //}
    }
}
