using History.Shared.Models;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace History.Api.Helper
{
    public class PageScraper
    {
        public List<T> GetData<T>(string Day) where T: BaseModel, new()    
        {


            var url = "https://en.wikipedia.org/wiki/" + Day;
            var web = new HtmlAgilityPack.HtmlWeb();
            var doc = web.Load(url);
            var xpathEvents = doc.DocumentNode.SelectNodes("//div[@class='mw-parser-output']/ul[position() =1]/li");
            var titles = new List<string>();
            var anchors = new List<string>();
            List<T> events = new List<T>();
            foreach (HtmlNode node in xpathEvents)
            {
                T ev = new T();
                List<Link> links = new List<Link>();


                ev.Html = HttpUtility.HtmlDecode(node.InnerHtml);
                try
                {
                    ev.Content = HttpUtility.HtmlDecode(node.InnerText).Split(" – ")[1];
                    ev.Year = HttpUtility.HtmlDecode(node.InnerText).Split("–")[0];
                }
                catch(IndexOutOfRangeException)
                {
                    //on some pages the split separator is different for the last list item so we will ignore it for now
                }
                try
                {
                    foreach (var nodeA in node.SelectNodes("/" + node.XPath + "/a[@href]"))
                        if (!nodeA.GetAttributeValue("href", string.Empty).Replace("/wiki/", "").All(Char.IsDigit))
                            anchors.Add("https://en.wikipedia.org" + nodeA.GetAttributeValue("href", string.Empty));
                }
                catch (NullReferenceException) { }
                try
                {
                    foreach (var nodeB in node.SelectNodes("/" + node.XPath + "/a[@title]"))
                        if (!nodeB.GetAttributeValue("title", string.Empty).All(Char.IsDigit))
                            titles.Add(nodeB.GetAttributeValue("title", string.Empty));
                }
                catch (NullReferenceException)
                {

                }
                for (int i = 0; i < titles.Count; i++)
                {
                    Link link = new Link
                    {
                        Title = titles[i],
                        Url = anchors[i]
                    };


                    links.Add(link);

                }
                ev.Link = links;
                events.Add(ev);
            }
            return events;
        }


     
    }
}
