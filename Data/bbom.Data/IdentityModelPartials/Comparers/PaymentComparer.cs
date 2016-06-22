using System.Collections.Generic;
using bbom.Data.IdentityModel;

namespace bbom.Data.IdentityModelPartials.Comparers
{
    public class PaymentComparer: IComparer<Payment>
    {
        public int Compare(Payment x, Payment y)
        {
            if (x.Date < y.Date)
                return -1;
            if (x.Date > y.Date)
                return 1;
            return 0;
        }
    }
}