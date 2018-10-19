using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Links;
using Sitecore.Web.UI.WebControls;

namespace events.tac.local.Models
{
    public class EventDetails: SearchResultItem
    {
        public string ContentHeading { get; set; }
        public string ContentIntro { get; set; }
        public DateTime StartDate { get; set; }

        public HtmlString EventImage => 
            new HtmlString(FieldRenderer.Render(GetItem(), "Event Image", "DisableWebEditing=true&mw=150"));
    }
}