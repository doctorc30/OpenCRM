using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using bbom.Admin.Core.Extensions;
using bbom.Data;
using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.DataExtensions.Helpers.Impl
{
    public class UserCache
    {
        public static ICollection<string> GetRoles(string userName)
        {
            var httpContext = HttpContext.Current;
            if (userName == null)
            {
                return FillRolesCache();
            }
            var rolesCache = httpContext.Cache[userName + "Roles"] as ICollection<string>;
            if (rolesCache == null)
            {
                return FillRolesCache();
            }
            return rolesCache;
        }

        private static ICollection<string> FillRolesCache()
        {
            var httpContext = HttpContext.Current;
            var user = DataFasade.GetRepository<AspNetUser>().GetById(httpContext.User.GetUserId());
            var roles = user.AspNetRoles.Select(role => role.Name).ToList();
            var userName = user.UserName;
            httpContext.Cache.Add(userName + "Roles", roles, null, DateTime.Now.AddMinutes(3),
                    Cache.NoSlidingExpiration, CacheItemPriority.Default, null);
            return roles;
        }

        public static void Clear(string userName)
        {
            var httpContext2 = HttpContext.Current;
            if (httpContext2.Cache[userName + "Roles"] != null)
                httpContext2.Cache.Remove(userName + "Roles");
            if (httpContext2.Cache[userName + "Menu"] != null)
                httpContext2.Cache.Remove(userName + "Menu");
        }
    }
}