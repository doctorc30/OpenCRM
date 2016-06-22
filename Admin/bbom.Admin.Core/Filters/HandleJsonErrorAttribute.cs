using System;
using System.Web;
using System.Web.Mvc;
using bbom.Admin.Core.Notifications;

namespace bbom.Admin.Core.Filters
{
    public class HandleJsonErrorAttribute : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            if (filterContext == null)
            {
                throw new ArgumentNullException("filterContext");
            }

            Exception exception = filterContext.Exception;

            if (!ExceptionType.IsInstanceOfType(exception))
            {
                return;
            }

            filterContext.Result = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                Data = Alert.ShowError(exception.Message)
            };
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = new HttpException(null, exception).GetHttpCode();

            // Certain versions of IIS will sometimes use their own error page when
            // they detect a server error. Setting this property indicates that we
            // want it to try to render ASP.NET MVC's error page instead.
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
        }
    }
}