using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using events.tac.local.Models;
using TAC.Utils.Abstractions;

namespace events.tac.local.Business.Navigation
{
    public class NavigationModelBuilder
    {
        public NavigationMenu CreateNavigatorMenu(IItem root, IItem currentItem)
        {
            var menu = new NavigationMenu()
            {
                Title = root.DisplayName,
                //Url = LinkManager.GetItemUrl(root),
                Url = root.GetUrl(),
                Children = root.IsAncestorOf(currentItem)
                    ? root.GetChildren()
                        //.Where(i => i[TemplateExcludeFieldName] != "1" && i.IsBasedOn(BaseNavigationTemplateId)
                        //.Where(i => i[TemplateExcludeFieldName] != "1")
                        .Select(i => CreateNavigatorMenu(i, currentItem))
                    : null
            };
            return menu;
        }

    }
}