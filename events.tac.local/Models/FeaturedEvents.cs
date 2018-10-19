using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace events.tac.local.Models
{
    public class FeaturedEvents
    {
        public FeaturedEvents() { }

        public HtmlString Heading { get; set; }
        public HtmlString Intro { get; set; }
        public HtmlString EventImage { get; set; }
        public string CssClass { get; set; }
        public string ButtonText { get; set; }
        public string Url { get; set; }
    }
}