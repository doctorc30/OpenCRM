using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using bbom.Data.IdentityModel;

namespace Solomon.Test.Mock.Controller
{
    public class IdentityControllerDecorator : TestControllerDecorator
    {
        private readonly AspNetUser _user;

        public IdentityControllerDecorator(AspNetUser user)
        {
            _user = user;
        }

        public new void SetComponent(System.Web.Mvc.Controller controller)
        {
            base.SetComponent(controller);
            var user = _user;
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