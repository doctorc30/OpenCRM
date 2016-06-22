using System.Linq;
using System.Web.Mvc;
using bbom.Admin.Core.Identity;
using Microsoft.AspNet.Identity.Owin;
using Ninject.Infrastructure.Language;

namespace bbom.Admin.Core.Filters
{
    public class AutoSignInAttribute: ActionFilterAttribute
    {
        private ApplicationSignInManager _signInManager;
        private string[] _signInRoles = { };

        public string Roles { get; set; }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!string.IsNullOrEmpty(Roles))
            {
                _signInRoles = Roles.Split(',');
                for (int i = 0; i < _signInRoles.Length; i++)
                {
                    _signInRoles[i] = _signInRoles[i].Trim();
                }
            }
            var userName = (string)filterContext.RouteData.Values["subdomain"];
            var roles = CoreFasade.UsersHelper.GetUserRoles(userName);
            if (roles.Intersect(_signInRoles.ToEnumerable()).Any())
            {
                _signInManager = CoreFasade.CreateApplicationSignInManager();
                _signInManager.PasswordSignIn(userName, userName + GlobalConstants.PasswordPostfix, true, false);
            }
            base.OnActionExecuting(filterContext);
        }
    }
}