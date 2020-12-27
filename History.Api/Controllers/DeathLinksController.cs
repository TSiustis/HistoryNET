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
    [Route("api/[controller]")]
    [ApiController]
    public class DeathLinksController : ControllerBase
    {
        private readonly HistoryDbContext _context;
        private readonly UnitOfWork unitOfWork;
        public DeathLinksController(HistoryDbContext context)
        {
            _context = context;
            unitOfWork = new UnitOfWork(_context);
        }

        /// <summary>
        /// Returns all html links specified in a 'Death' article
        /// </summary>
        ///  <remarks>
        /// Sample request:
        /// /api/Deaths/{deathId}/Links
        /// </remarks>
        /// <returns>All the links from an 'Death' article</returns>
        /// <response code="200">Returns the links from the given article</response>
        /// <response code ="404">If the article is null</response>
        [HttpGet]
        public ActionResult<IEnumerable<Link>> GetLink(int DeathId)
        {
            if (!unitOfWork.DeathRepository.Exists(DeathId))
                return NotFound();
            var links = unitOfWork.DeathRepository.GetById(DeathId).Link;
            if (links == null)
                return NotFound();
            return Ok(links);
        }

        /// <summary>
        /// Returns the html link specified  by an id from a 'Death' article
        /// </summary>
        ///  <remarks>
        /// Sample request:
        /// /api/Deaths/{deathId}/Links/{id}
        /// </remarks>
        /// <returns>All the links from an 'Death' article</returns>
        /// <response code="200">Returns the links from the given article</response>
        /// <response code ="404">If the article is null</response>
        [HttpGet("{id}")]
        public ActionResult<Link> GetLinkForDeath(int DeathId, int id)
        {
            if (!unitOfWork.DeathRepository.Exists(DeathId))
                return NotFound();
            var link = unitOfWork.DeathRepository.GetById(DeathId).Link.Where(l => l.Id == id);

            if (link == null)
            {
                return NotFound();
            }

            return Ok(link);
        }
        //Not supported for now
        [ApiExplorerSettings(IgnoreApi = true)]
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
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
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
