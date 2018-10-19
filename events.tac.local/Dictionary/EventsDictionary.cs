using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;

namespace events.tac.local.Dictionary
{
    public class EventsDictionary
    {
        private readonly Item _rootItem;

        public EventsDictionary(ID root)
        {
            _rootItem = Sitecore.Context.Database.GetItem(root);

        }

        public string GetText(String key)
        {
            return _rootItem.GetChildren(ChildListOptions.SkipSorting)
                       .FirstOrDefault(i => i["Key"].Equals(key))?["Text"] ?? 
                   $"No text foun by KEY[{key}]";
        }

    }
}