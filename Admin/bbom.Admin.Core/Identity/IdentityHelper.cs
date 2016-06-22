using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using IdentityModel;
using IdentityModel.Client;

namespace bbom.Admin.Core.Identity
{
    public static class IdentityHelper
    {
        public static async Task RefreshToken()
        {
            var client = new TokenClient(
                BbomAdminConstants.TokenEndpoint,
                "codeclient",
                "secret");
            var user = HttpContext.Current.User;
            var principal = user as ClaimsPrincipal;
            if (principal != null)
            {
                var refreshToken = principal.FindFirst("refresh_token").Value;

                var response = await client.RequestRefreshTokenAsync(refreshToken);
                UpdateCookie(response);
            }
        }

        private static void UpdateCookie(TokenResponse response)
        {
            if (response.IsError)
            {
                throw new Exception(response.Error);
            }
            var user = HttpContext.Current.User;
            var claimsPrincipal = user as ClaimsPrincipal;
            if (claimsPrincipal != null)
            {
                var identity = claimsPrincipal.Identities.First();
                var result = from c in identity.Claims
                    where c.Type != "access_token" &&
                          c.Type != "refresh_token" &&
                          c.Type != "expires_at"
                    select c;

                var claims = result.ToList();

                claims.Add(new Claim("access_token", response.AccessToken));
                claims.Add(new Claim("expires_at", (DateTime.UtcNow.ToEpochTime() + response.ExpiresIn).ToDateTimeFromEpoch().ToString()));
                claims.Add(new Claim("refresh_token", response.RefreshToken));

                var newId = new ClaimsIdentity(claims, "Cookies");
                HttpContext.Current.Request.GetOwinContext().Authentication.SignIn(newId);
            }
        }
    }
}