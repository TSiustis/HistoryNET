using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using History.Api.Data;
using History.Shared.Models;

namespace History.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly HistoryDbContext _context;

        public EventsController(HistoryDbContext context)
        {
            _context = context;
        }

        // GET: api/Events

        [HttpGet("GetAllEventsForDay", Name = nameof(GetAllEventsForDay))]
        public  ActionResult GetAllEventsForDay(string Day)
        {
            if (_context.Event.Where(e => e.Day.Equals(Day)) == null)
                return NotFound();
            return Ok(_context.Event.Where(e => e.Day.Equals(Day)).Include(e=>e.Link));
        }
        [HttpGet("GetAllEventsForYear", Name = nameof(GetAllEventsForYear))]
        public ActionResult GetAllEventsForYear(string Year)
        {
            if (_context.Event.Where(e => e.Year.Equals(Year)) == null)
                return NotFound();
            return Ok(_context.Event.Where(e => e.Year.Equals(Year)).Include(e => e.Link));
        }

        [HttpGet("GetAllEventsForDayAndYear", Name = nameof(GetAllEventsForDayAndYear))]
        public ActionResult GetAllEventsForDayAndYear(string Year,string Day)
        {
            if (_context.Event.Where(e => e.Year.Equals(Year) || e.Day.Equals(Day)) == null)
                return NotFound();
            return Ok(_context.Event.Where(e => e.Year.Equals(Year) && e.Day.Equals(Day)).Include(e => e.Link));
        }
        [HttpOptions]
        public IActionResult GetEventOptions()
        {
            Response.Headers.Add("Allow", "GET");
            return Ok();
        }

        // GET: api/Events/5
        [HttpGet("{id}")]
        public IActionResult GetEventById(int id)
        {
            var @event =  _context.Event.Find(id);

            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }

        // PUT: api/Events/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvent(int id, Event @event)
        {
          

            return NoContent();
        }

        // POST: api/Events
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Event>> PostEvent(Event @event)
        {
            return NotFound();
        }

        // DELETE: api/Events/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Event>> DeleteEvent(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }

            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();

            return @event;
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
    }
}
