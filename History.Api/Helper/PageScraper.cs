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
        public List<T> GetData<T>(string Day,string position) where T: TypeOfEvent, new()    
        {


            var url = "https://en.wikipedia.org/wiki/" + Day;
            var web = new HtmlAgilityPack.HtmlWeb();
            var doc = web.Load(url);
            var xpathEvents = doc.DocumentNode.SelectNodes("//div[@class='mw-parser-output']/ul[position() ="+position+"]/li");
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
                var AnchorNodes = node.SelectNodes("/" + node.XPath + "/a[@href]");
                if (AnchorNodes != null)
                {
                    foreach (var nodeA in AnchorNodes)
                        if (!nodeA.GetAttributeValue("href", string.Empty).Replace("/wiki/", "").All(Char.IsDigit))
                            anchors.Add("https://en.wikipedia.org" + nodeA.GetAttributeValue("href", string.Empty));
                }
                    var TitleNodes = node.SelectNodes("/" + node.XPath + "/a[@title]");
                if (TitleNodes != null)
                {
                    foreach (var nodeB in TitleNodes)
                        if (!nodeB.GetAttributeValue("title", string.Empty).All(Char.IsDigit))
                            titles.Add(nodeB.GetAttributeValue("title", string.Empty));
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
                ev.Day = Day;
                events.Add(ev);
            }
            return events;
        }


     
    }
}
