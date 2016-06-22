using System.Linq;
using System.Web;
using System.Web.Mvc;
using bbom.Admin.Core.Exceptions;
using bbom.Admin.Core.Extensions;
using bbom.Data.ModelPartials.Constants;

namespace bbom.Admin.Core.Filters.Block
{
    public class OnlyRoleAttribute : AuthorizeAttribute
    {
        public string Role { get; set; }
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext.Request.IsAuthenticated && httpContext.User.IsRole(Role))
            {
                return true;
            }
            throw new NoPermisionException();
        }
    }
}