using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bbom.Admin.Core.Identity;
using Microsoft.AspNet.Identity.Owin;
using Ninject.Infrastructure.Language;
using AuthorizationContext = System.Web.Mvc.AuthorizationContext;

namespace bbom.Admin.Core.Filters
{
    public class SkipAuthorizeAttribute : AuthorizeAttribute
    {
        private string[] _skipRoles = {};
        private ApplicationSignInManager _signInManager;

        private void CacheValidateHandler(HttpContext context, object data, ref HttpValidationStatus validationStatus)
        {
            validationStatus = OnCacheAuthorization(new HttpContextWrapper(context));
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (!string.IsNullOrEmpty(Roles))
            {
                _skipRoles = Roles.Split(',');
                for (int i = 0; i < _skipRoles.Length; i++)
                {
                    _skipRoles[i] = _skipRoles[i].Trim();
                }
            }
            var userName = (string) filterContext.RouteData.Values["subdomain"];
            var roles = CoreFasade.UsersHelper.GetUserRoles(userName);
            if (roles.Intersect(_skipRoles.ToEnumerable()).Any() ||
                filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                HttpCachePolicyBase cache = filterContext.HttpContext.Response.Cache;
                cache.SetProxyMaxAge(new TimeSpan(0L));
                cache.AddValidationCallback(CacheValidateHandler, null);
                if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                {
                    _signInManager = filterContext.HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
                    _signInManager.PasswordSignIn(userName, userName + GlobalConstants.PasswordPostfix, true, false);
                }
            }
            else
            {
                base.HandleUnauthorizedRequest(filterContext);
            }
        }
    }
}