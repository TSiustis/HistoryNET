using System;
using System.Collections.Generic;
using System.Text;

namespace History.Shared
{
   public class Event
    {
        public int Year { get; set; }
        public string Url { get; set; }
        public string Content { get; set; }
        public Link Link { get; set; }
    }
}
