using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;

namespace Solomon.Test.Mock.Controller
{
    public class HttpContextControllerDecorator : TestControllerDecorator
    {
        public new void SetComponent(System.Web.Mvc.Controller controller)
        {
            base.SetComponent(controller);
            //request
            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x.Headers).Returns(new NameValueCollection());

            //context
            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);
            MockControllerContext.SetupGet(x => x.HttpContext).Returns(context.Object);

            //url
            var routes = new RouteCollection();
            Component.Url = new UrlHelper(new RequestContext(context.Object, new RouteData()), routes);

            Component.ControllerContext = MockControllerContext.Object;
        }
    }
}
