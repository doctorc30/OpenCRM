using System.Linq;
using System.Web.Mvc;
using bbom.Data.ModelPartials.Constants;

namespace bbom.Admin.Core.Filters
{
    /// <summary>
    /// Реализует перенаправления для действий контроллера StartBuisnes
    /// </summary>
    public class StartBuisnesValidateAttribute : ActionFilterAttribute
    {
        private string _errorAction = "Error";
        private string _errorController = "";
        private string _firstStep = "FirstStep";
        private string _secondStep = "SecondStep";
        private string _therdStep = "TherdStep";
        private string _startBuisnes = "StartBuisnes";
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var action = filterContext.HttpContext.Request.RequestContext.RouteData.Values["action"];

            var userName = (string)filterContext.RouteData.Values["subdomain"];
            var user = CoreFasade.UsersHelper.GetUser(userName);
            var roles = CoreFasade.UsersHelper.GetUserRoles(user.UserName);

            if (action.Equals(_firstStep))
            {
                if (roles.Contains(UserRole.NotChangePassword) && !string.IsNullOrEmpty(user.Email))
                {
                    filterContext.Result = new RedirectResult($"~/{_startBuisnes}/{_secondStep}");
                }
                if (!roles.Contains(UserRole.NotChangePassword) && (roles.Contains(UserRole.NotUser)) &&
                    user.EmailConfirmed && !string.IsNullOrEmpty(user.Email))
                {
                    filterContext.Result = new RedirectResult("~/Payment/Index");
                }
                if (roles.Contains(UserRole.NotPay))
                {
                    filterContext.Result = new RedirectResult("~/Payment/Index");
                }
                if (user.EmailConfirmed && !string.IsNullOrEmpty(user.Email) && (roles.Contains(UserRole.User) || roles.Contains(UserRole.Admin)))
                    filterContext.Result = new RedirectResult($"~/{_startBuisnes}/{_secondStep}");
            }
            if (action.Equals(_secondStep))
            {
                if (string.IsNullOrEmpty(user.Email))
                {
                    filterContext.HttpContext.Request.RequestContext.RouteData.Values["action"] = _firstStep;
                    filterContext.HttpContext.Request.RequestContext.RouteData.Values["controller"] = _startBuisnes;
                }
                if (!roles.Contains(UserRole.NotChangePassword))
                {
                    filterContext.Result = new RedirectResult($"~/{_startBuisnes}/{_therdStep}");
                }
            }

            if (action.Equals(_therdStep))
            {
                if (!user.EmailConfirmed || string.IsNullOrEmpty(user.Email))
                {
                    filterContext.HttpContext.Request.RequestContext.RouteData.Values["action"] = _errorAction;
                    filterContext.HttpContext.Request.RequestContext.RouteData.Values["controller"] = _errorController;
                }
                if (roles.Contains(UserRole.NotChangePassword))
                {
                    filterContext.HttpContext.Request.RequestContext.RouteData.Values["action"] = _secondStep;
                    filterContext.HttpContext.Request.RequestContext.RouteData.Values["controller"] = _startBuisnes;
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}