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
            

            var url = "https://en.wikipedia.org/wiki/April_7";
            var web = new HtmlAgilityPack.HtmlWeb();
            var doc = web.Load(url);

            var xpathEvents = doc.DocumentNode.SelectNodes("//div[@class='mw-parser-output']/ul[position() =1]/li");

            var xpathBirths = doc.DocumentNode.SelectNodes("//div[@class='mw-parser-output']/ul[position() =2]/li");

            var xpathDeaths = doc.DocumentNode.SelectNodes("//div[@class='mw-parser-output']/ul[position() =3]/li");
           
            List<string> resultEvents = new List<string>();
            List<string> resultDeaths = new List<string>();
            List<string> resultBirths = new List<string>();
            List<Event> events = new List<Event>();
            events = _scraper.GetEvents(xpathEvents);
            foreach(var item in events)
            {
                System.Diagnostics.Debug.WriteLine(item.ToString());
            }
           
            foreach (HtmlNode node in xpathDeaths)
            {
                
                resultDeaths.Add(HttpUtility.HtmlDecode(node.InnerHtml));
            }
            foreach (HtmlNode node in xpathBirths)
            {
                resultBirths.Add(HttpUtility.HtmlDecode(node.InnerHtml));
            }
            //List<string[]> events = new List<string[]>();
            List<string[]> deaths = new List<string[]>();
            List<string[]> births = new List<string[]>();
            
            for (int i = 0; i < resultDeaths.Count; i++)
            {
                deaths.Add(resultDeaths[i].Split(" – "));

            }
            for (int i = 0; i < resultBirths.Count; i++)
            {
                births.Add(resultBirths[i].Split(" – "));

            }
            //foreach (var item in scrape)
            //{

            //}
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            var json = JsonSerializer.Serialize(resultEvents, jso);
            return json;
        }


    }
}
