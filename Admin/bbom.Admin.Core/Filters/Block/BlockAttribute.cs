using System.Linq;
using System.Web.Mvc;
using Ninject.Infrastructure.Language;

namespace bbom.Admin.Core.Filters.Block
{
    public class BlockAttribute : ActionFilterAttribute
    {
        private string[] _blockedRoles = { };

        public string Roles { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!string.IsNullOrEmpty(Roles))
            {
                _blockedRoles = Roles.Split(',');
                for (int i = 0; i < _blockedRoles.Length; i++)
                {
                    _blockedRoles[i] = _blockedRoles[i].Trim();
                }
            }
            var userName = (string) filterContext.RouteData.Values["subdomain"];
            var roles = CoreFasade.UsersHelper.GetUserRoles(userName);
            if (_blockedRoles.Length > 0)
            {
                if (roles.Intersect(_blockedRoles.ToEnumerable()).Any())
                {
                    filterContext.HttpContext.Request.RequestContext.RouteData.Values["action"] = Action;
                    filterContext.HttpContext.Request.RequestContext.RouteData.Values["controller"] = Controller;
                    filterContext.Result = new RedirectToRouteResult(filterContext.RouteData.Values);
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}