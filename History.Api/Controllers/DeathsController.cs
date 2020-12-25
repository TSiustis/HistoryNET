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
    public class DeathsController : ControllerBase
    {
        private readonly HistoryDbContext _context;
        private readonly UnitOfWork unitOfWork;
        public DeathsController(HistoryDbContext context)
        {
            _context = context;
            unitOfWork = new UnitOfWork(_context);
        }

        [HttpGet("GetAllDeathsForDay", Name = nameof(GetAllDeathsForDay))]
        public ActionResult GetAllDeathsForDay(string Day)
        {
            var Deaths = unitOfWork.DeathRepository.GetAllForDay(Day);
            if (Deaths == null)
                return NotFound();
            return Ok(Deaths);
        }
        [HttpGet("GetAllDeathsForYear", Name = nameof(GetAllDeathsForYear))]
        public ActionResult GetAllDeathsForYear(string Year)
        {
            var Deaths = unitOfWork.DeathRepository.GetAllForYear(Year);
            if (Deaths == null)
                return NotFound();
            return Ok(Deaths);
        }

        [HttpGet("GetAllDeathsForDayAndYear", Name = nameof(GetAllDeathsForDayAndYear))]
        public ActionResult GetAllDeathsForDayAndYear(string Year, string Day)
        {
            var Deaths = unitOfWork.DeathRepository.GetAllForDayAndYear(Year, Day);
            if (Deaths == null)
                return NotFound();
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

        [HttpPut("{id}")]
        public IActionResult PutDeath(int id, Death @Death)
        {


            return NoContent();
        }

        [HttpPost]
        public  ActionResult PostDeath(Death @Death)
        {
            return NotFound();
        }

        // DELETE: api/Deaths/5
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
