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


        /// <summary>
        /// Returns all html links specified in a event article
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// /api/Events/{eventId}/Links
        /// </remarks>
        /// <param name="eventId"></param>
        /// <returns>All the links from an event</returns>
        /// <response code="200">Returns the links from the given event</response>
        /// <response code ="404">If the event is null</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Link>> GetLink(int eventId)
        {
            if (!unitOfWork.EventRepository.Exists(eventId))
                return NotFound();
            var links = unitOfWork.EventRepository.GetById(eventId).Link;
            if (links == null)
                return NotFound();
            return Ok(links);
        }
        /// <summary>
        /// Returns all html link specified by an id from  a event article
        /// </summary>
        ///  <remarks>
        /// Sample request:
        /// /api/Events/{eventId}/Links/{id}
        /// </remarks>
        /// <returns>The specific link from an event</returns>
        /// <response code="200">Returns the link from the given event</response>
        /// <response code ="404">If the event or link is null</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPut("{id}")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Link>> PostLink(Link link)
        {
            _context.Link.Add(link);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLink", new { id = link.Id }, link);
        }
        //Not supported for now
        [ApiExplorerSettings(IgnoreApi = true)]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
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
