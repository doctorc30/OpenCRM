using System.Linq;
using System.Security.Claims;
using bbom.Data.ModelPartials.Constants;
using Glimpse.AspNet.Extensions;
using Glimpse.Core.Extensibility;

namespace elcoin.Admin
{
	public class GlimpseSecurityPolicy:IRuntimePolicy
	{
		public RuntimePolicy Execute(IRuntimePolicyContext policyContext)
		{
			var httpContext = policyContext.GetHttpContext();
		    var user = httpContext.User as ClaimsPrincipal;
		    if (user != null)
		    {
		        var roles = user.Claims.Where(claim => claim.Type == "role");
		        if (roles.Any(role => role.Value == UserRole.Admin))
		        {
		            return RuntimePolicy.On;
		        }
		    }
		    //if (user != null && !user.IsInRole(UserRole.Admin))
			//{
			//	return RuntimePolicy.Off;
			//}

			return RuntimePolicy.Off;
		}

		public RuntimeEvent ExecuteOn
		{
			get { return RuntimeEvent.EndRequest | RuntimeEvent.ExecuteResource; }
		}
	}
}