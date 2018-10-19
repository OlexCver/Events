using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.ComputedFields;
using Sitecore.Links;

namespace TAC.Utils.Search
{
    public class UrlComputedField: AbstractComputedIndexField
    {
        public override object ComputeFieldValue(IIndexable indexable)
        {
            var indexableItem = indexable as SitecoreIndexableItem;
            if (indexableItem == null)
                return null;

            var scItem = indexableItem.Item;
            if (scItem == null)
                return null;

            var options = LinkManager.GetDefaultUrlOptions();
            options.SiteResolving = true;

            var url = LinkManager.GetItemUrl(scItem, options);
            return url;
        }
    }
}
