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
        public List<Event> GetEvents(HtmlNodeCollection xpathEvents)
        {

            var titles = new List<string>();
            var anchors = new List<string>();
            var events = new List<Event>();
            foreach (HtmlNode node in xpathEvents)
            {
                Event ev = new Event();
                List<Link> links = new List<Link>();


                ev.Html = node.InnerHtml;
                ev.Content = HttpUtility.HtmlDecode(node.InnerText).Split(" – ")[1];
                ev.Year = HttpUtility.HtmlDecode(node.InnerText).Split("–")[0];
                

                foreach (var nodeA in node.SelectNodes("/" + node.XPath + "/a[@href]"))
                    if (!nodeA.GetAttributeValue("href", string.Empty).Replace("/wiki/", "").All(Char.IsDigit))
                        anchors.Add("https://en.wikipedia.org" + nodeA.GetAttributeValue("href", string.Empty));

                foreach (var nodeB in node.SelectNodes("/" + node.XPath + "/a[@title]"))
                    if (!nodeB.GetAttributeValue("title", string.Empty).All(Char.IsDigit))
                        titles.Add(nodeB.GetAttributeValue("title", string.Empty));

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
