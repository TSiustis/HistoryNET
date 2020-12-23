using History.Api.Data;
using History.Shared.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace History.Api.Helper
{
    public class ScrapeData
    {
        public static void Scrape(IApplicationBuilder applicationBuilder)
        {
            var scope = applicationBuilder.ApplicationServices.CreateScope();
            HistoryDbContext _historyDbContext = scope.ServiceProvider.GetRequiredService<HistoryDbContext>();
            PageScraper pageScraper = new PageScraper();
            var months  = CultureInfo.GetCultureInfo("en-us").DateTimeFormat.MonthNames;
            List<Event> events = new List<Event>();
            var watch = System.Diagnostics.Stopwatch.StartNew();
           
            if (!_historyDbContext.Event.Any())
            {
                for(int i = 0;i < 12; i++)
                {
                    for (int j = 1; j < DateTime.DaysInMonth(DateTime.Now.Year, i + 1);j++)
                    {
                        events = pageScraper.GetData<Event>(months[i] + "_" + j.ToString());
                        _historyDbContext.AddRange(events);

                        _historyDbContext.SaveChanges();
                    }
                }
            }

            // the code that you want to measure comes here
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            System.Diagnostics.Trace.WriteLine("ScrapeData took" + elapsedMs.ToString() + " ms");
        }
    }
}
