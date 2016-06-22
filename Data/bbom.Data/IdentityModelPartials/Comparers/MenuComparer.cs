using System.Collections.Generic;
using bbom.Data.ContentModel;

namespace bbom.Data.IdentityModelPartials.Comparers
{
    public class MenuComparer : IComparer<Menu>
    {
        public int Compare(Menu x, Menu y)
        {
            if (x.OrderNum < y.OrderNum)
                return -1;
            if (x.OrderNum > y.OrderNum)
                return 1;
            return 0;
        }
    }
}