using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using bbom.Admin.Core.Services.EmailService;

namespace bbom.Admin
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_End()
        {
            foreach (var process in Process.GetProcesses().Where(process => process.ProcessName.Contains("iisexpres")))
            {
                process.Close();
            }
        }

        void ErrorMail_Mailing(object sender, Elmah.ErrorMailEventArgs e)
        {
            //CoreFasade.EmailService.SendMail(e.Mail);
            new ServerEmailService().SendMail(e.Mail);
        }

    }
}
