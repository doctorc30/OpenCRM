using System.Web.Mvc;

namespace bbom.Admin.Core.Filters
{
    public class RemoveSubdomainAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RouteData.Values.ContainsKey("subdomain"))
            {
                filterContext.RouteData.Values.Remove("subdomain");
                filterContext.Result = new RedirectToRouteResult(filterContext.RouteData.Values);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}