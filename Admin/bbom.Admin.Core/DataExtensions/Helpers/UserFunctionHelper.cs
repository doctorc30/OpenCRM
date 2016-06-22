using System.Linq;
using bbom.Data.IdentityModel;
using bbom.Data.ModelPartials.Constants;

namespace bbom.Admin.Core.DataExtensions.Helpers
{
    public static class UserFunctionHelper
    {
        public static bool IsSystemUser(AspNetUser u)
        {
            return u.AspNetRoles.Any(r => r.Name == UserRole.User || r.Name == UserRole.Admin);
        }
    }
}