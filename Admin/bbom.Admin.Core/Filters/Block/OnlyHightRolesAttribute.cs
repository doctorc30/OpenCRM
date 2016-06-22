using System.Linq;
using System.Web;
using System.Web.Mvc;
using bbom.Admin.Core.Exceptions;
using bbom.Admin.Core.Extensions;
using bbom.Data.ModelPartials.Constants;

namespace bbom.Admin.Core.Filters.Block
{
    public class OnlyHightRolesAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return httpContext.Request.IsAuthenticated && Role(httpContext);
        }

        private bool Role(HttpContextBase httpContext)
        {
            string[] alowRoles = UserRole.HightRoles.Split(',');
            for (int i = 0; i < alowRoles.Length; i++)
            {
                alowRoles[i] = alowRoles[i].Trim();
            }
            if (alowRoles.Any(alowRole => httpContext.User.IsRole(alowRole)))
            {
                return true;
            }
            throw new NoPermisionException();
        }
    }
}