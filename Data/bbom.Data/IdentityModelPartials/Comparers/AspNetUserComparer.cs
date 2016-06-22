using System.Collections.Generic;
using bbom.Data.IdentityModel;

namespace bbom.Data.IdentityModelPartials.Comparers
{
    public class AspNetUserComparer : IComparer<AspNetUser>
    {
        public int Compare(AspNetUser x, AspNetUser y)
        {
            if (x.Foot == null || y.Foot == null)
                return 0;
            if (x.Foot < y.Foot)
                return -1;
            if (x.Foot > y.Foot)
                return 1;
            return 0;
        }
    }
}