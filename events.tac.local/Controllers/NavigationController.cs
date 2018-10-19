using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Web;
using System.Web.Mvc;
using events.tac.local.Business.Navigation;
using events.tac.local.Extensions;
using events.tac.local.Models;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using TAC.Utils.Abstractions;
using TAC.Utils.SitecoreModels;

namespace events.tac.local.Controllers
{
    public class NavigationController : Controller
    {

        private readonly NavigationModelBuilder _modelBuilder;
        private readonly RenderingContext _renderingContext;


        /// <summary>
        /// From DI container => see ServiceConfigurator.cs
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// <param name="renderingContext"></param>
        public NavigationController(
            NavigationModelBuilder modelBuilder, 
            RenderingContext renderingContext)
        {
            _modelBuilder = modelBuilder;
            _renderingContext = renderingContext;
        }

        private static readonly ID BaseNavigationTemplateId = ID.Parse("{96F5676B-4515-4046-A962-1C62253BE1AD}");

        private const string TemplateRootDefault = "Event Section";
        private const string TemplateLookupFieldName = "TemplateName";
        private const string TemplateExcludeFieldName = "ExcludeFromNavigation";

        // GET: Navigation
        public ActionResult Index()
        {

            var currentItem = _renderingContext.ContextItem;
            var dataSourceItem = _renderingContext.Rendering.Item;
            var templateId = _renderingContext.Rendering.Parameters[TemplateLookupFieldName];

            //If TemplateID selected - high priority
            //Otherwise check DataSource
            //Otherwise find by "Event Section" template name
            var section = !string.IsNullOrEmpty(templateId) ? 
                FindRootByTemplateId(currentItem, templateId) :
                    (dataSourceItem.ID != currentItem.ID) ? 
                FindRootByDataSource(dataSourceItem, currentItem)
                : FindRootByTemplateName(dataSourceItem, TemplateRootDefault);


            var model = _modelBuilder.CreateNavigatorMenu(new SitecoreItem(section), new SitecoreItem(currentItem));
            return View(model);
        }

        private Item FindRootByTemplateId(Item currentItem, string selTemplate)
        {
            var templateId = ID.Parse(selTemplate.FindItemLookupValue("TemplateId"));
            return currentItem.Axes
                .GetAncestors()
                .FirstOrDefault(i => i.TemplateID == templateId);
        }

        private Item FindRootByTemplateName(Item currentItem, string templateName)
        {
            var section = currentItem.Axes
                .GetAncestors()
                .FirstOrDefault(i => i.TemplateName == templateName);
            return section;
        }

        private static Item FindRootByDataSource(Item dataSourceItem, Item currentItem)
        {
            var section = currentItem.Axes
                    .GetAncestors()
                    .FirstOrDefault(i => i.ID == dataSourceItem.ID);
            return section;
        }



    }
}