using System;

namespace bbom.Admin.Core.Extensions
{
    public static class DateTimeExtend
    {
        public static bool IsBeetwen(this DateTime dateTime, DateTime from, DateTime to)
        {
            if (dateTime == from && dateTime == to)
                return true;
            if (to < from)
                return false;
            return dateTime >= from && dateTime <= to;
        }
    }
}