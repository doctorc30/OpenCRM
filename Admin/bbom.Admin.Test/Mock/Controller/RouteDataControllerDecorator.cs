using System.Web.Routing;
using bbom.Admin.Test.Controllers;

namespace bbom.Admin.Test.Mock.Controller
{
    public class RouteDataControllerDecorator : TestControllerDecorator
    {
        private readonly UnitTestControllerHelper.UserRole _role;

        public RouteDataControllerDecorator(UnitTestControllerHelper.UserRole role)
        {
            _role = role;
        }

        public new void SetComponent(System.Web.Mvc.Controller controller)
        {
            base.SetComponent(controller);
            var user = UnitTestControllerHelper.Users[_role];
            var routeData = new RouteData();
            routeData.Values.Add("subdomain", user.Name == "" ? "www" : user.Name);
            MockControllerContext.Setup(x => x.RouteData).Returns(routeData);
            Component.ControllerContext = MockControllerContext.Object;
        }
    }
}