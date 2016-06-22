using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using bbom.Admin.Test.Controllers;

namespace bbom.Admin.Test.Mock.Controller
{
    public class IdentityControllerDecorator : TestControllerDecorator
    {
        private readonly UnitTestControllerHelper.UserRole _role;

        public IdentityControllerDecorator(UnitTestControllerHelper.UserRole role)
        {
            _role = role;
        }

        public new void SetComponent(System.Web.Mvc.Controller controller)
        {
            base.SetComponent(controller);
            var user = UnitTestControllerHelper.Users[_role];
            //identity  
            List<Claim> claims = new List<Claim>
            {
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name", user.Name),
                new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier", user.Id)
            };
            var genericIdentity = new GenericIdentity("");
            genericIdentity.AddClaims(claims);
            var genericPrincipal = new GenericPrincipal(genericIdentity, new [] { "Asegurado" });

            if (!string.IsNullOrEmpty(user.Name))
                MockControllerContext.SetupGet(x => x.HttpContext.User).Returns(genericPrincipal);
            Component.ControllerContext = MockControllerContext.Object;
        }
    }
}