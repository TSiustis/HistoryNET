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
    public class EventsController : ControllerBase
    {
        private readonly HistoryDbContext _context;
        private readonly UnitOfWork unitOfWork;
        public EventsController(HistoryDbContext context)
        {
            _context = context;
            unitOfWork = new UnitOfWork(_context);
        }

        [HttpGet("GetAllEventsForDay", Name = nameof(GetAllEventsForDay))]
        public  ActionResult GetAllEventsForDay(string Day)
        {
            var events = unitOfWork.EventRepository.GetAllForDay(Day);
            if (events== null)
                return NotFound();
            return Ok(events);
        }
        [HttpGet("GetAllEventsForYear", Name = nameof(GetAllEventsForYear))]
        public ActionResult GetAllEventsForYear(string Year)
        {
            var events = unitOfWork.EventRepository.GetAllForYear(Year);
            if (events == null)
                return NotFound();
            return Ok(events);
        }

        [HttpGet("GetAllEventsForDayAndYear", Name = nameof(GetAllEventsForDayAndYear))]
        public ActionResult GetAllEventsForDayAndYear(string Year,string Day)
        {
            var events = unitOfWork.EventRepository.GetAllForDayAndYear(Year,Day);
            if (events == null)
                return NotFound();
            return Ok(events);
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
            var @event = unitOfWork.EventRepository.GetById(id);

            if (@event == null)
            {
                return NotFound();
            }

            return Ok(@event);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPut("{id}")]
        public IActionResult PutEvent(int id, Event @event)
        {
          

            return NoContent();
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        public ActionResult PostEvent(Event @event)
        {
            return NotFound();
        }

        // DELETE: api/Events/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(Event ev)
        {
             unitOfWork.EventRepository.Delete(ev);
            return Ok();
           
        }

       
    }
}
