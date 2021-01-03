using History.Shared.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace History.Api.Data
{
    public class HistoryDbContext : DbContext
    {
        //public HistoryDbContext() 
        //{
        //}
        public HistoryDbContext(DbContextOptions options) : base(options)
        {
            Database.SetCommandTimeout(Int32.MaxValue);
        }
        public DbSet<Event> Event { get; set; }
        public DbSet<Death> Death { get; set; }
        public DbSet<Birth> Birth { get; set; }
        public DbSet<Link> Link { get; set; }
    }
}
