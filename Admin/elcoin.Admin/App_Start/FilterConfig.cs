using System.Web.Mvc;
using bbom.Admin.Core.Filters;

namespace elcoin.Admin
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new AutoSignoutAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
