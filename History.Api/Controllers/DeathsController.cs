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
using History.Api.Helper;
using Newtonsoft.Json;

namespace History.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeathsController : ControllerBase
    {
        private readonly HistoryDbContext _context;
        private readonly UnitOfWork unitOfWork;
        public DeathsController(HistoryDbContext context)
        {
            _context = context;
            unitOfWork = new UnitOfWork(_context);
        }
        /// <summary>
        /// Returns all deaths for a day
        /// </summary>
        ///  <remarks>
        /// Sample request:
        /// /api/Events/GetAllDeathsFor?Day=August_1
        /// </remarks>
        /// <param name="Day"></param>
        /// <returns>Returns notable deaths for a given dayt</returns>
        /// <response code="200">Returns notable deaths for a given day</response>
        /// <response code ="404">If the deaths' page is null</response>
        [HttpGet("GetAllDeathsForDay", Name = nameof(GetAllDeathsForDay))]
        public ActionResult GetAllDeathsForDay(string Day,[FromQuery] QueryParameters queryParameters)
        {
            var Deaths = unitOfWork.DeathRepository.GetAllForDay(Day,queryParameters);
            var metadata = new
            {
                Deaths.TotalCount,
                Deaths.PageSize,
                Deaths.CurrentPage,
                Deaths.TotalPages,
                Deaths.HasNext,
                Deaths.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            if (Deaths == null)
                return NotFound();
            return Ok(Deaths);
        }
        /// <summary>
        /// Returns all deaths for a year
        /// </summary>
        ///  <remarks>
        /// Sample request:
        /// /api/Events/GetAllDeathsForYear?Year=1995
        /// </remarks>
        /// <param name="Year"></param>
        /// <returns>The deaths for a year</returns>
        /// <response code="200">Returns notable deaths for a given year</response>
        /// <response code ="404">If the deaths page is null</response>
        [HttpGet("GetAllDeathsForYear", Name = nameof(GetAllDeathsForYear))]
        public ActionResult GetAllDeathsForYear(string Year,[FromQuery] QueryParameters queryParameters)
        {
            var Deaths = unitOfWork.DeathRepository.GetAllForYear(Year, queryParameters);
            var metadata = new
            {
                Deaths.TotalCount,
                Deaths.PageSize,
                Deaths.CurrentPage,
                Deaths.TotalPages,
                Deaths.HasNext,
                Deaths.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            if (Deaths == null)
                return NotFound();
            return Ok(Deaths);
        }
        /// <summary>
        /// Returns all deaths for a day and year
        /// </summary>
        ///  <remarks>
        /// Sample request:
        /// /api/Events/GetAllDeathsFor?Day=August_1?Year=2007
        /// </remarks>
        /// <param name="Day"></param>
        /// <param name="Year"></param>
        /// <returns>Returns notable deaths for a given day and year</returns>
        /// <response code="200">Returns notable deaths for a given day and year</response>
        /// <response code ="404">If the deaths' page is null</response>
        [HttpGet("GetAllDeathsForDayAndYear", Name = nameof(GetAllDeathsForDayAndYear))]
        public ActionResult GetAllDeathsForDayAndYear(string Year, string Day, [FromQuery]QueryParameters queryParameters)
        {
            var Deaths = unitOfWork.DeathRepository.GetAllForDayAndYear(Year, Day, queryParameters);


            if (Deaths == null)
                return NotFound();
            var metadata = new
            {
                Deaths.TotalCount,
                Deaths.PageSize,
                Deaths.CurrentPage,
                Deaths.TotalPages,
                Deaths.HasNext,
                Deaths.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(Deaths);
        }
        [HttpOptions]
        public IActionResult GetDeathOptions()
        {
            Response.Headers.Add("Allow", "GET");
            return Ok();
        }

        // GET: api/Deaths/5
        [HttpGet("{id}")]
        public IActionResult GetDeathById(int id)
        {
            var Death = unitOfWork.DeathRepository.GetById(id);

            if (Death == null)
            {
                return NotFound();
            }

            return Ok(Death);
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPut("{id}")]
        public IActionResult PutDeath(int id, Death @Death)
        {


            return NoContent();
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        public  ActionResult PostDeath(Death @Death)
        {
            return NotFound();
        }

        // DELETE: api/Deaths/5
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpDelete("{id}")]
        public IActionResult DeleteDeath(Death ev)
        {
            unitOfWork.DeathRepository.Delete(ev);
            return Ok();

        }

        private bool DeathExists(int id)
        {
            return _context.Death.Any(e => e.Id == id);
        }
    }
}
