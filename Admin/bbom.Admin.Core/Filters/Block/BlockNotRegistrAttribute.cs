using System.Linq;
using System.Web.Mvc;
using bbom.Data.ModelPartials;
using bbom.Data.ModelPartials.Constants;

namespace bbom.Admin.Core.Filters.Block
{
    public class BlockNotRegistrAttribute : ActionFilterAttribute
    {
        private const string returnAction = "PersonalPage";
        private const string returnController = "PersonalPageCreator";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var userName = (string) filterContext.RouteData.Values["subdomain"];
            var roles = CoreFasade.UsersHelper.GetUserRoles(userName);
            if (roles.Any(t => t == UserRole.NotRegister))
            {
                filterContext.HttpContext.Request.RequestContext.RouteData.Values["action"] = returnAction;
                filterContext.HttpContext.Request.RequestContext.RouteData.Values["controller"] = returnController;
                filterContext.Result = new RedirectToRouteResult(filterContext.RouteData.Values);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}