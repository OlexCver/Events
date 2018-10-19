using System.Collections.Generic;

namespace events.tac.local.Models
{
    public class OverviewList : List<OverviewItem>
    {
        public OverviewList() { }

        public string ReadMore { get; set; }

    }
}