using System.Web.Routing;
using bbom.Data.IdentityModel;

namespace Solomon.Test.Mock.Controller
{
    public class RouteDataControllerDecorator : TestControllerDecorator
    {
        private AspNetUser _user;

        public RouteDataControllerDecorator(AspNetUser user)
        {
            _user = user;
        }

        public new void SetComponent(System.Web.Mvc.Controller controller)
        {
            base.SetComponent(controller);
            var user = _user;
            var routeData = new RouteData();
            routeData.Values.Add("subdomain", user.Name == "" ? "www" : user.Name);
            MockControllerContext.Setup(x => x.RouteData).Returns(routeData);
            Component.ControllerContext = MockControllerContext.Object;
        }
    }
}