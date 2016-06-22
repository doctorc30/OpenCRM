using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Threading.Tasks;
using bbom.Admin.Core;
using bbom.Admin.Core.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

namespace elcoin.Admin
{
    public partial class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public static string PublicClientId { get; private set; }

        // Дополнительные сведения о настройке проверки подлинности см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Настройка контекста базы данных, диспетчера пользователей и диспетчера входа для использования одного экземпляра на запрос
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            //app.UseCookieAuthentication(new CookieAuthenticationOptions
            //{
            //    AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
            //    LoginPath = PathString.FromUriComponent("/Account/Login"),
            //    Provider = new CookieAuthenticationProvider
            //    {
            //        // Позволяет приложению проверять метку безопасности при входе пользователя.
            //        // Эта функция безопасности используется, когда вы меняете пароль или добавляете внешнее имя входа в свою учетную запись.  
            //        OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
            //            validateInterval: TimeSpan.FromMinutes(30),
            //            regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
            //        //OnApplyRedirect = ApplyRedirect
            //    }
            //});
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            JwtSecurityTokenHandler.InboundClaimTypeMap = new Dictionary<string, string>();

            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies"
            });

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = "solomon.elcoin",
                Authority = Constants.SSODomain + "core",
                RedirectUri = Constants.DomainUrl,
                ResponseType = "id_token",
                Scope = "openid profile email roles",

                UseTokenLifetime = false,
                SignInAsAuthenticationType = "Cookies",

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    RedirectToIdentityProvider = n =>
                    {
                        var lenght = n.Request.Uri.Segments.Length;
                        if (lenght < 2)
                        {
                            return Task.FromResult(0);
                        }
                        string userName = "";
                        if (n.Request.Uri.Segments.Length > 0)
                        {
                            if (lenght == 2 && n.Request.Uri.Segments[1].Length > 8 &&
                                n.Request.Uri.Segments[1].IndexOf(GlobalConstants.NewUserPrefix) > -1)
                            {
                                userName = n.Request.Uri.Segments[1].Replace(GlobalConstants.NewUserPrefix, "");

                            }
                            if (lenght > 2 && n.Request.Uri.Segments[1].Length > 8 &&
                                n.Request.Uri.Segments[1].IndexOf(GlobalConstants.NewUserPrefix) > -1)
                            {
                                userName = n.Request.Uri.Segments[1].Replace(GlobalConstants.NewUserPrefix, "").Replace("/", "");
                            }
                            if (!string.IsNullOrEmpty(userName))
                            {
                                n.ProtocolMessage.Parameters.Add("user", userName);
                            }
                        }

                        //if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.LogoutRequest)
                        //{
                        //    var idTokenHint = n.OwinContext.Authentication.User.FindFirst("id_token");

                        //    if (idTokenHint != null)
                        //    {
                        //        n.ProtocolMessage.IdTokenHint = idTokenHint.Value;
                        //    }
                        //}
                        return Task.FromResult(0);
                    }
                }
            });
            app.MapSignalR();
        }

        //private async Task SecurityTokenValidated(SecurityTokenValidatedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> n)
        //{
        //    var id = n.AuthenticationTicket.Identity;

        //    // we want to keep first name, last name, subject and roles
        //    var givenName = id.FindFirst(JwtClaimTypes.GivenName);
        //    var familyName = id.FindFirst(JwtClaimTypes.FamilyName);
        //    var sub = id.FindFirst(JwtClaimTypes.Subject);
        //    var email = id.FindAll(JwtClaimTypes.Email);
        //    var roles = id.FindAll(JwtClaimTypes.Role);
        //    var nickname = id.FindAll(JwtClaimTypes.PreferredUserName);

        //    // create new identity and set name and role claim type
        //    var nid = new ClaimsIdentity(
        //        id.AuthenticationType,
        //        JwtClaimTypes.PreferredUserName,
        //        JwtClaimTypes.Role);

        //    nid.AddClaim(givenName);
        //    nid.AddClaim(familyName);
        //    nid.AddClaim(sub);
        //    nid.AddClaims(email);
        //    nid.AddClaims(roles);
        //    nid.AddClaims(nickname);

        //    n.AuthenticationTicket = new AuthenticationTicket(
        //        nid,
        //        n.AuthenticationTicket.Properties);
        //}

        //private static void ApplyRedirect(CookieApplyRedirectContext context)
        //{
        //    Uri absoluteUri;
        //    if (Uri.TryCreate(context.RedirectUri, UriKind.Absolute, out absoluteUri))
        //    {
        //        var domain = ConfigurationManager.AppSettings["ssoDomain"];
        //        if (domain == null)
        //        {
        //            throw new Exception("Не указан сервер авторизации.");
        //        }
        //        string[] host = absoluteUri.Host.Split('.');
        //        string subDomain = "";
        //        if (host.Length == 2 && host[1] == "localhost")
        //        {
        //            subDomain = host[0] + ".";
        //        }
        //        if (host.Length == 3)
        //        {
        //            subDomain = host[0] + ".";
        //        }
        //        var path = PathString.FromUriComponent(absoluteUri);
        //        if (path == context.OwinContext.Request.PathBase + context.Options.LoginPath)
        //            context.RedirectUri = $"http://{subDomain}{domain}/Account/Login" +
        //                new QueryString(
        //                    context.Options.ReturnUrlParameter,
        //                    context.Request.Uri.AbsoluteUri);
        //    }

        //    context.Response.Redirect(context.RedirectUri);
        //}
    }
}