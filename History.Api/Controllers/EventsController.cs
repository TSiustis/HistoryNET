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
using Microsoft.AspNetCore.Cors;
using History.Api.Helper;
using Newtonsoft.Json;

namespace History.Api.Controllers
{
    [EnableCors("MyPolicy")]
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
        /// <summary>
        /// Returns all events that happened on the given day
        /// </summary>
        ///  <remarks>
        /// Sample request:
        /// /api/Events/GetAllEventsForDay?Day=August_7
        /// </remarks>
        /// <returns>The events for a given day</returns>
        /// <response code="200">Returns the events for a given day</response>
        /// <response code ="404">If the event  is null</response>
        [HttpGet("GetAllEventsForDay", Name = nameof(GetAllEventsForDay))]
        public  ActionResult GetAllEventsForDay(string Day, [FromQuery] QueryParameters queryParameters)
        {
            var events = unitOfWork.EventRepository.GetAllForDay(Day,queryParameters);
            var metadata = new
            {
                events.TotalCount,
                events.PageSize,
                events.CurrentPage,
                events.TotalPages,
                events.HasNext,
                events.HasPrevious
            };
            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            if (events== null)
                return NotFound();
            return Ok(events);
        }
        /// <summary>
        /// Returns all events specified by a year
        /// </summary>
        ///  <remarks>
        /// Sample request:
        /// /api/Events/GetAllEventsForYear?Year=2007
        /// </remarks>
        /// <returns>The events for the given year</returns>
        /// <response code="200">Returns the events from the given year</response>
        /// <response code ="404">If the event  is null</response>
        [HttpGet("GetAllEventsForYear", Name = nameof(GetAllEventsForYear))]
        public ActionResult GetAllEventsForYear(string Year, [FromQuery] QueryParameters queryParameters)
        {
            var events = unitOfWork.EventRepository.GetAllForYear(Year, queryParameters);
            var metadata = new
            {
                events.TotalCount,
                events.PageSize,
                events.CurrentPage,
                events.TotalPages,
                events.HasNext,
                events.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            if (events == null)
                return NotFound();
            return Ok(events);
        }

        /// <summary>
        /// Returns all html link specified by an id from  a event article
        /// </summary>
        ///  <remarks>
        /// Sample request:
        /// /api/Events/GetAllEventsForDayAndYear?Year=1995?Day=August_1
        /// </remarks>
        /// <returns>All events from a given day and year</returns>
        /// <response code="200">Returns the events from the given day and year</response>
        /// <response code ="404">If the event  does not exist in the database</response>
        [HttpGet("GetAllEventsForDayAndYear", Name = nameof(GetAllEventsForDayAndYear))]
        public ActionResult GetAllEventsForDayAndYear(string Year,string Day, [FromQuery] QueryParameters queryParameters)
        {
            var events = unitOfWork.EventRepository.GetAllForDayAndYear(Year,Day,queryParameters);
            var metadata = new
            {
                events.TotalCount,
                events.PageSize,
                events.CurrentPage,
                events.TotalPages,
                events.HasNext,
                events.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
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
                return NotFound(id);
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
