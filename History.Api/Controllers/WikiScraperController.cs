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

namespace History.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WikiScraperController : ControllerBase
    {

        private readonly ILogger<WikiScraperController> _logger;
        public WikiScraperController(ILogger<WikiScraperController> logger)
        {
            _logger = logger;
        }
       // public async Task<List<dynamic>> GetPageData(string url, List<dynamic> results, DateTimeOffset date)
       // {
       //     var config = Configuration.Default.WithDefaultLoader();

       //     var context = BrowsingContext.New(config);

       //     var document = await context.OpenAsync("<add your target url here>");
       //     _logger.LogInformation(document.DocumentElement.OuterHtml);

       //     var events = document.QuerySelectorAll("span#Events");
       //     var births = document.QuerySelectorAll("span#Births");
       //     var deaths = document.QuerySelectorAll("span#Deaths");
       // }
       [HttpGet]
       public string Get()
        {
            

            var url = "https://en.wikipedia.org/wiki/April_7";
            //_logger.LogInformation(document.DocumentElement.OuterHtml);
            var web = new HtmlAgilityPack.HtmlWeb();
            var doc = web.Load(url);

            var xpathEvents = doc.DocumentNode.SelectNodes("//div[@class='mw-parser-output']/ul[position() =1]/li");

            var xpathBirths = doc.DocumentNode.SelectNodes("//div[@class='mw-parser-output']/ul[position() =2]/li");

            var xpathDeaths = doc.DocumentNode.SelectNodes("//div[@class='mw-parser-output']/ul[position() =3]/li");
            //var events = document.QuerySelectorAll("tr.vevent");
            //var births = document.QuerySelectorAll("span#Births");
            //var deaths = document.QuerySelectorAll("span#Deaths");

            List<string> resultEvents = new List<string>();
            List<string> resultDeaths = new List<string>();
            List<string> resultBirths = new List<string>();
            var titles = new List<string>();
            var anchors = new List<string>();
            foreach (HtmlNode node in xpathEvents)
            {
                resultEvents.Add(HttpUtility.HtmlDecode(node.InnerHtml));
                foreach (var nodeA in node.SelectNodes("/"+node.XPath+"/a[@href]"))

                    if (!nodeA.GetAttributeValue("href", string.Empty).Replace("/wiki/", "").All(Char.IsDigit))
                        anchors.Add("https://en.wikipedia.org" + nodeA.GetAttributeValue("href", string.Empty));
                foreach (var nodeB in node.SelectNodes("/" + node.XPath + "/a[@title]"))
                    if(!nodeB.GetAttributeValue("title", string.Empty).All(Char.IsDigit))
                    titles.Add(nodeB.GetAttributeValue("title", string.Empty));
            }
            foreach (HtmlNode node in xpathDeaths)
            {
                
                resultDeaths.Add(HttpUtility.HtmlDecode(node.InnerHtml));
            }
            foreach (HtmlNode node in xpathBirths)
            {
                resultBirths.Add(HttpUtility.HtmlDecode(node.InnerHtml));
            }
            List<string[]> events = new List<string[]>();
            List<string[]> deaths = new List<string[]>();
            List<string[]> births = new List<string[]>();
            for (int i = 0;i < resultEvents.Count;i++)
            {
                events.Add(resultEvents[i].Split(" – "));

            }
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
