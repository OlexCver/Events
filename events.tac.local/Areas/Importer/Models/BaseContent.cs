using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Data;

namespace events.tac.local.Areas.Importer.Models
{
    public struct BaseContent
    {
        public static TemplateID TemplateID = new TemplateID(new ID("{C70AE340-4139-417A-B4CB-97EBA11684AC}"));
        public struct Fields
        {
            public static ID ContentHeading = new ID("{1384A77F-8E0C-40BA-B628-3C5ED330DD86}");
            public static ID ContentIntro = new ID("{AE97D6D0-6BC0-4395-B623-4DBD705C3C18}");
        }
    }
}