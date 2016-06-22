using System.Web;
using System.Web.Mvc;
using bbom.Admin.Core.Extensions;

namespace bbom.Admin.Core.Filters
{
    public class AutoSignoutAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var urlUserName = (string)filterContext.RouteData.Values["subdomain"];
            var userName = filterContext.HttpContext.User.GetUserName();
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                if (urlUserName != null && userName != urlUserName)
                {
                    filterContext.HttpContext.GetOwinContext().Authentication.SignOut();
                }
            }
        }
    }
}