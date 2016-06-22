using System.Configuration;
using System.Web.Mvc;
using System.Web.Routing;

namespace elcoin.Admin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            string domain = ConfigurationManager.AppSettings["DomainShort"];
            routes.MapRoute(
                name: "DefaultNotRegister",
                url: "newuser{subdomain}/{controller}/{action}/{id}",
                defaults:
                    new
                    {
                        controller = "Home",
                        action = "Index",
                        id = UrlParameter.Optional
                    }
                );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new {controller = "Home", action = "Index", id = UrlParameter.Optional}
                );



            //routes.Add("post_details_subdomen", new DomainRoute(
            //    domain,
            //    "{controller}/{action}/{id}",
            //    new {controller = "Home", action = "Index", id = UrlParameter.Optional}
            //    ));

            //routes.Add("DomainRoute", new DomainRoute(
            //    domain, // Domain with parameters
            //    "{subdomain}/{controller}/{action}/{id}", // URL with parameters
            //    new { subdomain = "www", controller = "Home", action = "Index", id = UrlParameter.Optional }
            //    // Parameter defaults
            //    ));

            //routes.Add("post_details_subdomen", new DomainRoute(
            //    domain,
            //    "{subdomain}/{controller}/{action}/{id}",
            //    new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //    ));




        }
    }
}
