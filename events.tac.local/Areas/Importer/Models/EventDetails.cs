using Sitecore.Data;

namespace events.tac.local.Areas.Importer.Models
{
    public struct EventDetails
    {
        public static TemplateID TemplateID = new TemplateID(new ID("{FAA70993-D6D8-441F-9DFE-32E44B71744D}"));

        public struct Fields
        {
            public static readonly ID StartDate = new ID("{D3355E3F-5A74-48D8-9077-DC53163DB895}");
            public static readonly ID Duration = new ID("{A7591EE3-A4CA-4926-A1D3-E596D58A0B9B}");
            public static readonly ID DifficultyLevel = new ID("{7553DCB7-13CE-4865-8892-FA6D7ECFF9B9}");
            public static readonly ID RelatedEvents = new ID("{6A2059C2-E5B5-45D0-8CAA-9EAFC79072AE}");
            public static readonly ID EventImage = new ID("{F994B984-4F25-41E1-8680-A6A297242950}");
        }
    }
}