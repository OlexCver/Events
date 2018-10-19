using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using events.tac.local.Models;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;

namespace events.tac.local.Controllers
{

    public class AnyController : Controller
    {
        //Test Merge 

        // GET: Any
        public ActionResult Index()
        {
            return View(CreateModel());
        }

        private AnyModel CreateModel()
        {
            var item = RenderingContext.Current.ContextItem;
            return new AnyModel()
            {
                Heading = new HtmlString(FieldRenderer.Render(item, "ContentHeading")),
                Intro = new HtmlString(FieldRenderer.Render(item, "ContentIntro")),
            };
        }
    }
}