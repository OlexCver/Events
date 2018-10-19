using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using events.tac.local.Models;
using Sitecore;
using Sitecore.Data.Fields;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;

namespace events.tac.local.Controllers
{
    public class RelatedEventsController : Controller
    {
        // GET: RelatedEvents
        public ActionResult Index()
        {
            return BuildResult();
        }

        private ActionResult BuildResult()
        {
            var item = RenderingContext.Current.Rendering.Item;
            if (item == null) return new EmptyResult();

            var relatedItems = (MultilistField) item.Fields["Related Events"];
            var items = relatedItems.GetItems();

            if (!items.Any() && !Sitecore.Context.PageMode.IsExperienceEditorEditing)
                return new EmptyResult();

            var events = items
                .Select(i => new NavigationItem()
                {
                    Title = i.DisplayName,
                    Url = LinkManager.GetItemUrl(i)
                });
            return View(events);
        }
    }
}