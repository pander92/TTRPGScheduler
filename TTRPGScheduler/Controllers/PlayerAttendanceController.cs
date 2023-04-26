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
    public class PlayerAttendanceController : ControllerBase
    {
        private readonly DBContext _context;

        public PlayerAttendanceController(DBContext context)
        {
            _context = context;
        }

        // GET: api/PlayerAttendance
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerAttendance>>> GetPlayerAttendance()
        {
          if (_context.PlayerAttendance == null)
          {
              return NotFound();
          }
            return await _context.PlayerAttendance.ToListAsync();
        }

        // GET: api/PlayerAttendance/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerAttendance>> GetPlayerAttendance(int id)
        {
          if (_context.PlayerAttendance == null)
          {
              return NotFound();
          }
            var playerAttendance = await _context.PlayerAttendance.FindAsync(id);

            if (playerAttendance == null)
            {
                return NotFound();
            }

            return playerAttendance;
        }

        // PUT: api/PlayerAttendance/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerAttendance(int attendanceId, bool availabiltiy)
        {
            PlayerAttendance updatedAttendance = await _context.PlayerAttendance.FirstOrDefaultAsync(x => x.attendanceId == attendanceId);
            if (attendanceId != updatedAttendance.attendanceId)
            {
                return BadRequest();
            }

            updatedAttendance.availability = availabiltiy;
            _context.Entry(updatedAttendance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerAttendanceExists(attendanceId))
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

        // POST: api/PlayerAttendance?playerId=2&sessionId=3&availability=true
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PlayerAttendance>> PostPlayerAttendance(int playerId, int sessionId, bool availability)
        {
          if (_context.PlayerAttendance == null)
          {
              return Problem("Entity set 'DBContext.PlayerAttendance'  is null.");
          }
            PlayerAttendance resp = new PlayerAttendance(sessionId, playerId, availability);
            _context.PlayerAttendance.Add(resp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerAttendance", new { id = resp.attendanceId }, resp);
        }

        // DELETE: api/PlayerAttendance/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayerAttendance(int id)
        {
            if (_context.PlayerAttendance == null)
            {
                return NotFound();
            }
            var playerAttendance = await _context.PlayerAttendance.FindAsync(id);
            if (playerAttendance == null)
            {
                return NotFound();
            }

            _context.PlayerAttendance.Remove(playerAttendance);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerAttendanceExists(int id)
        {
            return (_context.PlayerAttendance?.Any(e => e.attendanceId == id)).GetValueOrDefault();
        }
    }
}
