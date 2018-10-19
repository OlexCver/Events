using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using events.tac.local.Models;
using Sitecore.Collections;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;

namespace events.tac.local.Controllers
{
    public class OverviewController : Controller
    {
        private const string FieldNameTitle = "contentheading";
        private const string FieldNameImage = "decorationbanner";

        // GET: Overview
        public ActionResult Index()
        {
            var model = new OverviewList()
            {
                ReadMore = Sitecore.Globalization.Translate.Text("Read More")
            };
             
            model.AddRange(RenderingContext.Current.ContextItem.GetChildren(ChildListOptions.SkipSorting)
                //.OrderBy(i=>i.Modified)
                .OrderBy(i => i.Created)
                .Select(i => new OverviewItem()
                {
                    Url = LinkManager.GetItemUrl(i),
                    Title = new HtmlString(FieldRenderer.Render(i, FieldNameTitle)),
                    Image = new HtmlString(FieldRenderer.Render(i, FieldNameImage, "mw=500&mh=333&class=img-responsive"))
                }
            ));
            return View(model);
        }
    }
}