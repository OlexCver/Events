using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using events.tac.local.Areas.Importer.Models;
using events.tac.local.Extensions;
using Newtonsoft.Json;
using Sitecore;
using Sitecore.Data;
using Sitecore.Data.Converters;
using Sitecore.Data.Items;
using Sitecore.SecurityModel;

namespace events.tac.local.Areas.Importer.Controllers
{
    public class EventsController : Controller
    {

        // GET: Importer/Events
        public ActionResult Index()
        {
            ViewBag.HandledItems = string.Empty;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(HttpPostedFileBase file, string parentPath)
        {
            var events = await ParseUploadedFile(file);
            if (!events.Any()) return View();

            var database = Sitecore.Configuration.Factory.GetDatabase("master");
            var parentItem = database.GetItem(parentPath);
            var (ItemCreated, ItemEdited) = (0, 0);
            //var tuple = new ItemCalculation();
            using (new SecurityDisabler())
            {
                Array.ForEach(events.ToArray(), ev =>
                    {
                        var name = ItemUtil.ProposeValidItemName(ev.ContentHeading);
                        var item = parentItem.GetFirstChild(i => i.Name == name);
                        (ItemCreated, ItemEdited) = ItemCalculator((ItemCreated, ItemEdited), item);
                        //tuple = ItemCalculator(tuple, item);

                        var itemIsNew = (item == null);

                        item = itemIsNew ? CreateNewItem(parentItem, name) :
                                           CreateNewVersion(item, database);
                        item.Editing.BeginEdit();

                        item.Fields[BaseContent.Fields.ContentHeading].Value = ev.ContentHeading;
                        item.Fields[BaseContent.Fields.ContentIntro].Value = ev.ContentIntro;
                        //Assign Workflow
                        var currentState = itemIsNew ? AssignWorkFlowFromStart(item) :
                                                      item.State.GetWorkflowState().FinalState ? 
                                                          ID.Parse(item.State.GetWorkflowState().StateID) // just return Final State
                                                          :
                                                       AssignWorkFlowLastState(item);


                        item.Editing.EndEdit();
                    }
                );
            }

            ViewBag.HandledItems = $"Item created {ItemCreated.ToString()}, edited {ItemEdited.ToString()}";
            //ViewBag.HandledItems = $"Item created {tuple.ItemCreated.ToString()}, edited {tuple.ItemEdited.ToString()}";
            return View();
        }

        private ID  AssignWorkFlowFromStart(Item item)
        {
            item.Fields[FieldIDs.Workflow].Value = "{76B1062F-20A1-41E7-8CC9-E7B60011EA04}";
            item.Fields[FieldIDs.WorkflowState].Value = "{66DD7BB1-DD3C-40EF-B800-39315B3450FB}";
            return ID.Parse(item.Fields[FieldIDs.WorkflowState].Value);
        }

        private ID AssignWorkFlowLastState(Item item)
        {
            item.Fields[FieldIDs.Workflow].Value = "{76B1062F-20A1-41E7-8CC9-E7B60011EA04}";
            item.Fields[FieldIDs.WorkflowState].Value = "{26BDB2C5-910F-4224-B803-50ABC128DAF9}";
            return ID.Parse(item.Fields[FieldIDs.WorkflowState].Value);
        }


        private static Item CreateNewVersion(Item item, Database database)
        {
            var itemId = item.ID;
            item.Versions.AddVersion();
            item = database.GetItem(itemId);
            return item;
        }

        private static Item CreateNewItem(Item parentItem, string name)
        {
            return parentItem.Add(name, EventDetails.TemplateID);
        }

        private (int itemCreated, int itemEdited) ItemCalculator((int itemCreated, int itemEdited) tuple, Item item)
        {

            if (item != null)
            {
                tuple.itemEdited++;
                return tuple;
            }
            tuple.itemCreated++;
            return tuple;
        }


        //private ItemCalculation ItemCalculator(ItemCalculation tuple, Item item)
        //{

        //    if (item != null)
        //    {
        //        tuple.ItemEdited++;
        //        return tuple;
        //    }
        //    tuple.ItemCreated++;
        //    return tuple;
        //}


        private static async Task<IEnumerable<Event>> ParseUploadedFile(HttpPostedFileBase file)
        {
            IEnumerable<Event> events = new List<Event>();
            //string message = null;
            using (var reader = new StreamReader(file.InputStream))
            {
                var contents = await reader.ReadToEndAsync();
                try
                {
                    events = JsonConvert.DeserializeObject<IEnumerable<Event>>(contents);
                }
                catch (Exception e)
                {
                    //Console.WriteLine(e);
                    //throw;
                }
            }
            return events;
        }
    }


    public class ItemCalculation
    {
        public int ItemCreated { get; set; }
        public int ItemEdited { get; set; }
    }
}