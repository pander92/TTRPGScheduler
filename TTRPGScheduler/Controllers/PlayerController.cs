using System;
using System.Collections;
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
    public class PlayerController : ControllerBase
    {
        private readonly DBContext _context;

        public PlayerController(DBContext context)
        {
            _context = context;
        }

        // GET: api/Player
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayer()
        {
          if (_context.Player == null)
          {
              return NotFound();
          }
            return await _context.Player.ToListAsync();
        }

        // GET: api/Player/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
          if (_context.Player == null)
          {
              return NotFound();
          }
            var player = await _context.Player.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // PUT: api/Player/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            if (id != player.playerId)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        // POST: api/Player
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
          if (_context.Player == null)
          {
              return Problem("Entity set 'DBContext.Player'  is null.");
          }
            _context.Player.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayer", new { id = player.playerId }, player);
        }

        // DELETE: api/Player/5
        [HttpDelete]
        public async Task<Response> DeletePlayer(int playerId)
        {
            string description = "";
            if (_context.Player == null)
            {
                description = "Player doesnt exist. ";
                return new Response(HttpContext.Response.StatusCode, description);
            }
            var player = await _context.Player.FindAsync(playerId);
            if (player == null)
            {
                description = "Player not found. ";
                return new Response(HttpContext.Response.StatusCode, description);
            }

            _context.Player.Remove(player);
            await _context.SaveChangesAsync();
            HttpResponse resp = HttpContext.Response;
            description += "Delete Player " + handleStatusDesc(resp.StatusCode);

            Response final = new Response(resp.StatusCode, description);

            return final;
        }

        private bool PlayerExists(int id)
        {
            return (_context.Player?.Any(e => e.playerId == id)).GetValueOrDefault();
        }


        //this just translates status codes into descriptive text
        public string handleStatusDesc(int statusCode)
        {
            string description;
            if (statusCode >= 200 && statusCode < 300)
            {
                description = " Request succeeded";
            }
            else if (statusCode == 301)
            {
                description = "permanent redirect";
            }
            else if (statusCode == 404)
            {
                description = "Not found";
            }
            else if (statusCode == 500)
            {
                description = "internal service error";
            }
            else if (statusCode == 503)
            {
                description = "service unavailable";
            }
            else if (statusCode == 405)
            {
                description = "not allowed";
            }
            else
            {
                description = "unknown error " + statusCode ;
            }

            return description;
        }
    }
}
