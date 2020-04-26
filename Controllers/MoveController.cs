using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UprootTools.Models;

namespace UprootTools.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class MoveController : ControllerBase
  {
    private readonly DatabaseContext _context;

    public MoveController(DatabaseContext context)
    {
      _context = context;
    }

    // GET: api/Move
    [HttpGet("current")]
    public async Task<ActionResult<Move>> GetCurrentMove()
    {
      return await _context.Moves.FirstOrDefaultAsync(f => f.IsCurrent);
    }

    // GET: api/Move
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Move>>> GetMoves()
    {
      return await _context.Moves.ToListAsync();
    }

    // GET: api/Move/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Move>> GetMove(int id)
    {
      var move = await _context.Moves.FindAsync(id);

      if (move == null)
      {
        return NotFound();
      }

      return move;
    }

    // PUT: api/Move/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMove(int id, Move move)
    {
      if (id != move.Id)
      {
        return BadRequest();
      }

      _context.Entry(move).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!MoveExists(id))
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

    // POST: api/Move
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for
    // more details see https://aka.ms/RazorPagesCRUD.
    [HttpPost]
    public async Task<ActionResult<Move>> PostMove(Move move)
    {
      _context.Moves.Add(move);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetMove", new { id = move.Id }, move);
    }

    // DELETE: api/Move/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<Move>> DeleteMove(int id)
    {
      var move = await _context.Moves.FindAsync(id);
      if (move == null)
      {
        return NotFound();
      }

      _context.Moves.Remove(move);
      await _context.SaveChangesAsync();

      return move;
    }

    private bool MoveExists(int id)
    {
      return _context.Moves.Any(e => e.Id == id);
    }
  }
}
