using System;
using System.Linq;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace events.tac.local.Extensions
{
    public static class ItemExtensions
    {
        public static bool IsBasedOn(this Item item, ID templateId)
        {
            var template = Sitecore.Data.Managers.TemplateManager.GetTemplate(item.TemplateID, item.Database);
            return template.DescendsFromOrEquals(templateId);
        }

        public static bool ContainsChild(this Item item, Func<Item, bool> selector )
        {
            return item.GetChildren(ChildListOptions.SkipSorting)
                        .FirstOrDefault(selector)!=null;
        }

        public static Item GetFirstChild(this Item item, Func<Item, bool> selector)
        {
            return item.GetChildren(ChildListOptions.SkipSorting).FirstOrDefault(selector);
        }


    }
}