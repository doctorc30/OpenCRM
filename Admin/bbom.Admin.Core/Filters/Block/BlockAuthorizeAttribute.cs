using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bbom.Admin.Core.Extensions;

namespace bbom.Admin.Core.Filters.Block
{
    public class BlockAuthorizeAttribute : AuthorizeAttribute
    {
        private string[] _blockedRoles = { };

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (!String.IsNullOrEmpty(Roles))
            {
                _blockedRoles = Roles.Split(',');
                for (int i = 0; i < _blockedRoles.Length; i++)
                {
                    _blockedRoles[i] = _blockedRoles[i].Trim();
                }
            }
            return httpContext.Request.IsAuthenticated && Role(httpContext);
        }

        private bool Role(HttpContextBase httpContext)
        {
            if (_blockedRoles.Length > 0)
            {
                if ( _blockedRoles.Any(t => httpContext.User.IsRole(t)))
                {
                    httpContext.Request.RequestContext.RouteData.Values["subdomain"] = httpContext.User.GetUserName();
                    httpContext.Request.RequestContext.RouteData.Values["action"] = Action;
                    httpContext.Request.RequestContext.RouteData.Values["controller"] = Controller;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }

        public string Action { get; set; }
        public string Controller { get; set; }
    }
}