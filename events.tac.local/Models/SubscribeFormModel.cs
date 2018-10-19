using System.Web;
using events.tac.local.Extensions;

namespace events.tac.local.Models
{
    public class SubscribeFormModel
    {
        private readonly string _subscribeTitle;
        private readonly string _subscribeText;

        public HtmlString SubscribeTitle => new HtmlString(_subscribeTitle);
        public HtmlString SubscribeText => new HtmlString(_subscribeText);

        public SubscribeFormModel(string subscribeTitle, string subscribeText)
        {
            _subscribeTitle = subscribeTitle.EmptyIfNull();
            _subscribeText = subscribeText.EmptyIfNull();
        }
    }
}