using System.Linq;
using System.Web.Mvc;
using bbom.Data.ModelPartials.Constants;

namespace bbom.Admin.Core.Filters.Block
{
    public class BlockLowRolesAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string[] blockedRoles = UserRole.LowRoles.Split(',');
            for (int i = 0; i < blockedRoles.Length; i++)
            {
                blockedRoles[i] = blockedRoles[i].Trim();
            }
            foreach (var blockedRole in blockedRoles)
            {
                var userName = (string) filterContext.RouteData.Values["subdomain"];
                var roles = CoreFasade.UsersHelper.GetUserRoles(userName);
                if (roles.Any(t => t == blockedRole))
                {
                    filterContext.HttpContext.Request.RequestContext.RouteData.Values["action"] = "FirstStep";
                    filterContext.HttpContext.Request.RequestContext.RouteData.Values["controller"] = "StartBuisnes";
                    filterContext.Result = new RedirectToRouteResult(filterContext.RouteData.Values);
                    break;
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}