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
            
            //MOVE THESE TO PAGE SCRAPER
            var url = "https://en.wikipedia.org/wiki/April_7";
            var web = new HtmlAgilityPack.HtmlWeb();
            var doc = web.Load(url);

            var xpathEvents = doc.DocumentNode.SelectNodes("//div[@class='mw-parser-output']/ul[position() =1]/li");

            var xpathBirths = doc.DocumentNode.SelectNodes("//div[@class='mw-parser-output']/ul[position() =2]/li");

            var xpathDeaths = doc.DocumentNode.SelectNodes("//div[@class='mw-parser-output']/ul[position() =3]/li");
           // MOVE THESE TO SEPARAte` coNTROLLERS
            List<Event> events = new List<Event>();
            events = _scraper.GetData<Event>(xpathEvents);

            List<Death> deaths = new List<Death>();
            deaths = _scraper.GetData<Death>(xpathDeaths);

            List<Birth> births = new List<Birth>();
            births = _scraper.GetData<Birth>(xpathBirths);

            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            var json = JsonSerializer.Serialize(events, jso);
            return json;
        }


    }
}
