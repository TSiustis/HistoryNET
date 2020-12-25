using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using History.Api.Data;
using History.Shared.Models;
using History.Api.Services;

namespace History.Api.Controllers
{
    [Route("api/Events/{eventId}/[controller]")]
    [ApiController]
    public class EventLinksController : ControllerBase
    {
        private readonly HistoryDbContext _context;
        private readonly UnitOfWork unitOfWork;
        public EventLinksController(HistoryDbContext context)
        {
            _context = context;
            unitOfWork = new UnitOfWork(_context);
        }
        

        // GET: api/Events/{eventId}/EventLinks
        [HttpGet]
        public ActionResult<IEnumerable<Link>> GetLink(int eventId)
        {
            if (!unitOfWork.EventRepository.Exists(eventId))
                return NotFound();
            var links = unitOfWork.EventRepository.GetById(eventId).Link;
            if (links == null)
                return NotFound();
            return Ok(links);
        }

        // GET: api/Events/{eventId}/EventLinks/5
        [HttpGet("{id}")]
        public ActionResult<Link> GetLinkForEvent(int eventId,int id)
        {
            if (!unitOfWork.EventRepository.Exists(eventId))
                return NotFound();
            var link = unitOfWork.EventRepository.GetById(eventId).Link.Where(l=>l.Id==id);

            if (link == null)
            {
                return NotFound();
            }

            return Ok(link);
        }
        //Not supported for now
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLink(int id, Link link)
        {
            if (id != link.Id)
            {
                return BadRequest();
            }

            _context.Entry(link).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LinkExists(id))
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
        //Not supported for now
        [HttpPost]
        public async Task<ActionResult<Link>> PostLink(Link link)
        {
            _context.Link.Add(link);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLink", new { id = link.Id }, link);
        }
        //Not supported for now
        [HttpDelete("{id}")]
        public async Task<ActionResult<Link>> DeleteLink(int id)
        {
            var link = await _context.Link.FindAsync(id);
            if (link == null)
            {
                return NotFound();
            }

            _context.Link.Remove(link);
            await _context.SaveChangesAsync();

            return link;
        }
        [HttpOptions]
        public IActionResult GetLinkOptions()
        {
            Response.Headers.Add("Allow", "GET");
            return Ok();
        }
        private bool LinkExists(int id)
        {
            return _context.Link.Any(e => e.Id == id);
        }
    }
}
