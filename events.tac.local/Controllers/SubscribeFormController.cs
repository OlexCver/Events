using System.Web.Mvc;
using events.tac.local.Dictionary;
using events.tac.local.Models;
using Sitecore.Analytics;
using Sitecore.Analytics.Data;
using Sitecore.Analytics.Outcome.Extensions;
using Sitecore.Analytics.Model.Entities;
using Sitecore.Analytics.Outcome.Model;
using Sitecore.Analytics.Tracking;
using Sitecore.Data;
using TAC.Utils.Mvc;

namespace events.tac.local.Controllers
{
    public class SubscribeFormController : Controller
    {
        readonly EventsDictionary _dictionary = new EventsDictionary(ID.Parse("{1F7E3E8C-75D0-4BCA-A99F-4C173A7B2907}"));

        // GET: SubscribeForm
        public ActionResult Index()
        {
            return View(CreateModel());
        }

        private SubscribeFormModel CreateModel()
        {
            var model = new SubscribeFormModel(
                _dictionary.GetText("SubscribeTitle"),
                _dictionary.GetText("SubscribeText")
                );
            return model;
        }




        [ValidateFormHandler]
        [HttpPost]
        public ActionResult Index(string email)
        {
            //Identify email contact
            Sitecore.Analytics.Tracker.Current.Session.Identify(email);

            //Get current contact
            var contact = Sitecore.Analytics.Tracker.Current.Contact;

            //Analytical - current contact (email)
            CreateEmailAnalytic(email, contact);

            //Analytical - Outcomes(RESULTS only via API)
            // /sitecore/system/Marketing Control Panel/Outcomes/Subscribed
            // {8931433F-EE16-4616-B996-C122E5A4E243}
            CreateOutcomeAnalytic(contact);

            //Analytical - Goals(PURPOSE)
            // /sitecore/system/Marketing Control Panel/Goals/Newsletter Signup
            // {1779CC42-EF7A-4C58-BF19-FA85D30755C9}
            CreateGoalAnalytic();


            return View("Confirmation");
        }

        private void CreateGoalAnalytic()
        {
            var goal = new PageEventData("Newsletter signup");
            var currentPage = Tracker.Current.CurrentPage;
            currentPage.Register(goal);
        }

        private void CreateOutcomeAnalytic(Contact contact)
        {
            var outcome = new ContactOutcome(
                ID.NewID,
                //sitecore/system/Marketing Control Panel/Outcomes/Subscribed
                new ID("{8931433F-EE16-4616-B996-C122E5A4E243}"), //your own Outcome
                new ID(contact.ContactId));
            Tracker.Current.RegisterContactOutcome(outcome);
        }


        private void CreateEmailAnalytic(string email, Contact contact)
        {
            var emails = contact.GetFacet<IContactEmailAddresses>("Emails");
            //are we already saved the contact?
            if (emails.Entries.Contains("personal"))
                return;

            //Otherwise save the contact
            var personalEmail = emails.Entries.Create("personal");
            personalEmail.SmtpAddress = email;
        }
    }
}