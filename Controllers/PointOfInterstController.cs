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
    public class PointOfInterstController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public PointOfInterstController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/PointOfInterst
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterest>>> GetPointsOfInterest()
        {
            return await _context.PointsOfInterest.ToListAsync();
        }

        // GET: api/PointOfInterst/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PointOfInterest>> GetPointOfInterest(int id)
        {
            var pointOfInterest = await _context.PointsOfInterest.FindAsync(id);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return pointOfInterest;
        }

        // PUT: api/PointOfInterst/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPointOfInterest(int id, PointOfInterest pointOfInterest)
        {
            if (id != pointOfInterest.Id)
            {
                return BadRequest();
            }

            _context.Entry(pointOfInterest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PointOfInterestExists(id))
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

        // POST: api/PointOfInterst
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PointOfInterest>> PostPointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointsOfInterest.Add(pointOfInterest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPointOfInterest", new { id = pointOfInterest.Id }, pointOfInterest);
        }

        // DELETE: api/PointOfInterst/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PointOfInterest>> DeletePointOfInterest(int id)
        {
            var pointOfInterest = await _context.PointsOfInterest.FindAsync(id);
            if (pointOfInterest == null)
            {
                return NotFound();
            }

            _context.PointsOfInterest.Remove(pointOfInterest);
            await _context.SaveChangesAsync();

            return pointOfInterest;
        }

        private bool PointOfInterestExists(int id)
        {
            return _context.PointsOfInterest.Any(e => e.Id == id);
        }
    }
}
