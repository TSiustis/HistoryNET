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
            

            var url = "https://en.wikipedia.org/wiki/August_14";
            //_logger.LogInformation(document.DocumentElement.OuterHtml);
            var web = new HtmlAgilityPack.HtmlWeb();
            var doc = web.Load(url);

            var xpath = doc.DocumentNode.SelectNodes("//div[@class='mw-parser-output']/ul/li");
            //var events = document.QuerySelectorAll("tr.vevent");
            //var births = document.QuerySelectorAll("span#Births");
            //var deaths = document.QuerySelectorAll("span#Deaths");
            
            List<string> result = new List<string>();
            foreach(HtmlNode node in xpath)
            {
                result.Add(HttpUtility.HtmlDecode(node.InnerText));
               // System.Diagnostics.Debug.WriteLine(System.Uri.UnescapeDataString(node.InnerHtml));
                System.Diagnostics.Debug.WriteLine(System.Uri.UnescapeDataString(node.InnerText));
            }
            JsonSerializerOptions jso = new JsonSerializerOptions();
            jso.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            var json = JsonSerializer.Serialize(result, jso);
            return json;
        }


    }
}
