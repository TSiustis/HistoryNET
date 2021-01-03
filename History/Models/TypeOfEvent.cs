using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace History.Shared.Models
{
    public abstract class TypeOfEvent
    {
        [Key]
        public int Id { get; set; }
        public string Day { get; set; }
        public string Url { get; set; }
        public string Year { get; set; }
        public string Html { get; set; }
        public string Content { get; set; }
        public virtual List<Link> Link { get; set; }

        public override string ToString()
        {
            return this.GetType().ToString() + " Year: " + Year +  " Html: " + Html + "Content: " + Content + Link.ToString();
        }
    }
}
