using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using events.tac.local.Extensions;
using events.tac.local.Models;
using Sitecore.Data;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;

namespace events.tac.local.Controllers
{
    public class FeaturedEventController : Controller
    {
        // GET: FeaturedEvent
        public ActionResult Index()
        {
            return View(CreateModel());
        }

        private static  FeaturedEvents CreateModel()
        {
            //Note: we NOT use - RenderingContext.Current.ContextItem;
            //But DataSource item
            var item = RenderingContext.Current.Rendering.Item;
            var model = new FeaturedEvents()
            {
                Heading = new HtmlString(FieldRenderer.Render(item, "ContentHeading")),
                EventImage = new HtmlString(FieldRenderer.Render(item, "Event Image", "mw=400")),
                Intro = new HtmlString(FieldRenderer.Render(item, "ContentIntro")),
            };

            var cssClassSelectedId = RenderingContext.Current.Rendering.Parameters["CssClass"];
            if (!string.IsNullOrEmpty(cssClassSelectedId))
            {
                model.CssClass = cssClassSelectedId.FindItemLookupValue("class");
            }

            //Fill for usual text (control rendering parameters)
            var buttonText = RenderingContext.Current.Rendering.Parameters["buttonText"];
            if (!string.IsNullOrEmpty(buttonText))
            {
                model.ButtonText = buttonText;
            }

            //Fill URL
            model.Url = LinkManager.GetItemUrl(item);
            return model;
        }
    }
}