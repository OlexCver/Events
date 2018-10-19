using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using events.tac.local.Models;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;

namespace events.tac.local.Controllers
{
    public class BreadcrumbController : Controller
    {
        // GET: Breadcrumb
        public ActionResult Index()
        {
            return View(CreateModel());
        }


        private IEnumerable<NavigationItem> CreateModel()
        {
            var currentItem = RenderingContext.Current.ContextItem;
            var homeItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);
            var breadcrumbs = RenderingContext.Current.ContextItem.Axes.GetAncestors()
                .Where(i => i.Axes.IsDescendantOf(homeItem))
                .Concat(new Item[] {currentItem})
                .Select(s => new NavigationItem()
                {
                    Title = s.DisplayName,
                     //Title = new HtmlString(FieldRenderer.Render(s, "ContentHeading", "disable-web-editing=true")),
                    Url = LinkManager.GetItemUrl(s),
                    Active = (s.ID== currentItem.ID)
                });


            return breadcrumbs;
        }
    }
}