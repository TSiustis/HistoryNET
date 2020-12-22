using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Web;
using System.Text;
using History.Shared;
using History.Shared.Models;
using History.Api.Helper;

namespace History.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WikiScraperController : ControllerBase
    {

        private readonly ILogger<WikiScraperController> _logger;
        private readonly PageScraper _scraper;
        public WikiScraperController(ILogger<WikiScraperController> logger)
        {
            _scraper = new PageScraper();
            _logger = logger;
        }
      
       [HttpGet]
       public string Get()
        {
            
            List<Event> events = new List<Event>();
            events = _scraper.GetData<Event>("April 7");

            List<Death> deaths = new List<Death>();
            deaths = _scraper.GetData<Death>("April 8");

            List <Birth> births = new List<Birth>();
            births = _scraper.GetData<Birth>("April 9");

            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            var json = JsonSerializer.Serialize(events, jso);
            return json;
        }


    }
}
