using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UprootTools.Models;

namespace UprootTools.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class PointOfInterestController : ControllerBase
  {
    private readonly DatabaseContext _context;
    private readonly string _MAPBOX_TOKEN;

    public PointOfInterestController(DatabaseContext context, IConfiguration config)
    {
      _context = context;
      this._MAPBOX_TOKEN = config["mapbox_token"];
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
      var client = new HttpClient();
      var resp = await client.GetAsync($"https://api.mapbox.com/geocoding/v5/mapbox.places/{pointOfInterest.Address}.json?access_token={this._MAPBOX_TOKEN}");

      var json = await JsonDocument.ParseAsync(await resp.Content.ReadAsStreamAsync());
      var root = json.RootElement;
      var feature = root.GetProperty("features").EnumerateArray().First();
      var center = feature.GetProperty("center").EnumerateArray();
      var lng = center.First();
      var lat = center.Skip(1).First();

      Console.WriteLine($"{lat},{lng}");
      pointOfInterest.Latitude = lat.GetDouble();
      pointOfInterest.Longitude = lng.GetDouble();



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
