using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TTRPGScheduler.Models;

namespace TTRPGScheduler.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProposedSessionController : ControllerBase
    {
        private readonly DBContext _context;

        public ProposedSessionController(DBContext context)
        {
            _context = context;
        }

        // GET: api/ProposedSession
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProposedSession>>> GetProposedSession()
        {
          if (_context.ProposedSession == null)
          {
              return NotFound();
          }
            //get all sessions, and remove those that arent viable
            List<ProposedSession> vlist = await _context.ProposedSession.ToListAsync();
            foreach(ProposedSession i in vlist.ToList())
            {
                if (!i.viability)
                {
                    vlist.Remove(i);
                }
            }
            return vlist;
        }

        // GET: api/ProposedSession/15 Mar 2022
        [HttpGet("{sDate}")]
        public async Task<ActionResult<IEnumerable<ProposedSession>>> GetProposedSession(string sDate)
        {
          if (_context.ProposedSession == null)
          {
              return NotFound();
          }
            //parse input into date format
            var d = DateOnly.Parse(sDate);
            //get sessions matching that date
            var proposedSession = await _context.ProposedSession.Where(x => x.sessionDate == d).ToListAsync();

            if (proposedSession == null)
            {
                return NotFound();
            }

            return proposedSession;
        }

        // PUT: api/ProposedSession/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProposedSession(int id, ProposedSession proposedSession)
        {
            if (id != proposedSession.sessionId)
            {
                return BadRequest();
            }

            _context.Entry(proposedSession).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProposedSessionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ProposedSession
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProposedSession>> PostProposedSession(ProposedSession proposedSession)
        {
          if (_context.ProposedSession == null)
          {
              return Problem("Entity set 'DBContext.ProposedSession'  is null.");
          }
            _context.ProposedSession.Add(proposedSession);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProposedSession", new { id = proposedSession.sessionId }, proposedSession);
        }

        // DELETE: api/ProposedSession/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProposedSession(int id)
        {
            if (_context.ProposedSession == null)
            {
                return NotFound();
            }
            var proposedSession = await _context.ProposedSession.FindAsync(id);
            if (proposedSession == null)
            {
                return NotFound();
            }

            _context.ProposedSession.Remove(proposedSession);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProposedSessionExists(int id)
        {
            return (_context.ProposedSession?.Any(e => e.sessionId == id)).GetValueOrDefault();
        }
    }
}
