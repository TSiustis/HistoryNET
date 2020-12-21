using System;
using System.Collections.Generic;
using System.Text;

namespace History.Shared.Models
{
    public abstract class BaseModel
    {

        public int Id { get; set; }
        public string Year { get; set; }
        public string Html { get; set; }
        public string Content { get; set; }
        public List<Link> Link { get; set; }

        public override string ToString()
        {
            return this.GetType().ToString() + " Year: " + Year +  " Html: " + Html + "Content: " + Content + Link.ToString();
        }
    }
}
