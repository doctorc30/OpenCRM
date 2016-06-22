using System.Web;
using System.Web.Mvc;
using bbom.Admin.Core.Exceptions;
using bbom.Admin.Core.Extensions;
using bbom.Data.ModelPartials;
using bbom.Data.ModelPartials.Constants;

namespace bbom.Admin.Core.Filters.Block
{
    public class OnlyAdminAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Request.IsAuthenticated && Role(httpContext);
        }

        private bool Role(HttpContextBase httpContext)
        {
            if (httpContext.User.IsRole(UserRole.Admin))
            {
                return true;
            }
            throw new NoPermisionException();
        }
    }
}