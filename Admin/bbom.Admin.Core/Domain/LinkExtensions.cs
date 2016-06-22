using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace bbom.Admin.Core.Domain
{
    public static class UrlExtensions
    {
        
        public static string Action2(this UrlHelper helper, string actionName, string controllerName, bool requireAbsoluteUrl)
        {
            if (requireAbsoluteUrl)
            {
                HttpContextBase currentContext = new HttpContextWrapper(HttpContext.Current);
                RouteData routeData = RouteTable.Routes.GetRouteData(currentContext);

                routeData.Values["controller"] = controllerName;
                routeData.Values["action"] = actionName;

                DomainRoute domainRoute = routeData.Route as DomainRoute;
                if (domainRoute != null)
                {
                    DomainData domainData = domainRoute.GetDomainData(new RequestContext(currentContext, routeData), routeData.Values);
                    var url = UrlHelper.GenerateUrl(null , actionName, controllerName, "http", domainData.HostName, domainData.Fragment, null, helper.RouteCollection, helper.RequestContext, true);
                    return url;
                }
            }
            return UrlHelper.GenerateUrl(null, actionName, controllerName, null, helper.RouteCollection, helper.RequestContext, true); ;
        }
        

    }
}
