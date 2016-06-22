using System;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using IdentityModel;
using Microsoft.AspNet.Identity;

namespace bbom.Admin.Core.Extensions
{
    public static class IdentityExtensions
    {
        public static string GetUserRoles(this IIdentity identity)
        {
            try
            {
                var u = HttpContext.Current.User as ClaimsPrincipal;
                var roles = "";
                roles = CoreFasade.UsersHelper.GetUserRoles(identity.Name).Aggregate("", (s, s1) => s + "," + s1);
                //if (u != null && u.Claims.Any(claim => claim.Type == "role"))
                //{
                //    roles = u.Claims.Where(claim => claim.Type == "role")
                //        .Aggregate("", (s, i) => s + "," + i.Value);
                //}
                //else
                //{
                //    roles = CoreFasade.UsersHelper.GetUserRoles(identity.Name).Aggregate("", (s, s1) => s + "," + s1);
                //}
                return roles;
            }
            catch
            {
                return "";
            }
        }

        public static string GetUserName(this IPrincipal user)
        {
            try
            {
                var u = HttpContext.Current.User as ClaimsPrincipal;
                return u.FindFirst(JwtClaimTypes.PreferredUserName).Value;
            }
            catch
            {
                return user.Identity.Name;
            }
        }

        public static string GetUserIO(this IPrincipal user)
        {
            try
            {
                var u = HttpContext.Current.User as ClaimsPrincipal;
                return u.FindFirst(JwtClaimTypes.FamilyName).Value + " " + u.FindFirst(JwtClaimTypes.GivenName).Value;
            }
            catch
            {
                return "";
            }
        }

        public static string GetUserFIO(this IIdentity identity)
        {
            try
            {
                var u = HttpContext.Current.User as ClaimsPrincipal;
                return u.FindFirst(JwtClaimTypes.FamilyName).Value + " " + u.FindFirst(JwtClaimTypes.GivenName).Value;
            }
            catch
            {
                return "";
            }
        }

        public static string GetUserId(this IPrincipal user)
        {
            var userId = "";
            var ic = user as ClaimsPrincipal;
            try
            {
                userId = ic.FindFirst(JwtClaimTypes.Subject).Value;
            }
            catch
            {
                userId = user.Identity.GetUserId();
            }
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Пользователь ненайден");
            }
            return userId;
        }

        public static bool IsRole(this IPrincipal user, string r)
        {
            var u = user as ClaimsPrincipal;
            var roles = u.Claims.Where(claim => claim.Type == "role").ToList();
            if (roles.Any())
            {
                if (roles.Any(role => role.Value == r))
                {
                    return true;
                }
            }
            else
            {
                return user.IsInRole(r);
            }
            return false;
        }
    }
}