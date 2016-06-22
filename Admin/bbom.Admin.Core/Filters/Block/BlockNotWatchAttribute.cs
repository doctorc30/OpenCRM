using System.Linq;
using System.Web.Mvc;
using bbom.Data.ModelPartials;
using bbom.Data.ModelPartials.Constants;

namespace bbom.Admin.Core.Filters.Block
{
    public class BlockNotWatchAttribute : ActionFilterAttribute
    {
        private const string Action = "Events";
        private const string Controller = "Home";
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userName = (string) filterContext.RouteData.Values["subdomain"];
            var roles = CoreFasade.UsersHelper.GetUserRoles(userName);
            if (roles.Any(t => t == UserRole.NotWatch))
            {
                filterContext.HttpContext.Request.RequestContext.RouteData.Values["id"] = 0;
                filterContext.HttpContext.Request.RequestContext.RouteData.Values["action"] = Action;
                filterContext.HttpContext.Request.RequestContext.RouteData.Values["controller"] = Controller;
                filterContext.Result = new RedirectToRouteResult(filterContext.RouteData.Values);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}