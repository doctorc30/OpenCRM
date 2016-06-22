using IdentityManager.Core.Logging;
using IdentityManager.Logging;
using IdentityServer3.Core.Configuration;
using IdentitySolomon.IdSvr;
using Microsoft.Owin;
using Owin;
using Serilog;

[assembly: OwinStartup(typeof(IdentitySolomon.Startup))]
namespace IdentitySolomon
{
    internal class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            LogProvider.SetCurrentLogProvider(new DiagnosticsTraceLogProvider());
            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Debug()
               .WriteTo.Trace()
               .CreateLogger();

            //app.Map("/admin", adminApp =>
            //{
            //    var factory = new IdentityManagerServiceFactory();
            //    factory.ConfigureSimpleIdentityManagerService("bbomDb2");
            //    adminApp.UseIdentityManager(new IdentityManagerOptions()
            //    {
            //        Factory = factory
            //    });
            //});

            app.Map("/core", core =>
            {
                var idSvrFactory = Factory.Configure();
                idSvrFactory.ConfigureUserService("identityDb");
                var options = new IdentityServerOptions
                {
                    SiteName = "Solomon SSO",
                    SigningCertificate = Certificate.Get(),
                    Factory = idSvrFactory,
                    //EnableWelcomePage = false,
                    RequireSsl = false,
                    AuthenticationOptions = new AuthenticationOptions
                    {
                        EnableLocalLogin = true,
                        EnablePostSignOutAutoRedirect = true
                    }
                };

                core.UseIdentityServer(options);
            });
        }
    }
}