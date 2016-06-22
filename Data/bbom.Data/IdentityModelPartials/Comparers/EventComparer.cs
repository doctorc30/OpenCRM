using System.Collections.Generic;
using bbom.Data.ContentModel;

namespace bbom.Data.IdentityModelPartials.Comparers
{
    public class EventComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            if (x.StartDate < y.StartDate)
                return -1;
            if (x.StartDate > y.StartDate)
                return 1;
            return 0;
        }
    }
}