using System.Web.Mvc;
using Moq;

namespace bbom.Admin.Test.Mock.Controller
{
    public abstract class TestControllerDecorator : System.Web.Mvc.Controller
    {
        protected System.Web.Mvc.Controller Component;
        protected Mock<ControllerContext> MockControllerContext;

        public void SetComponent(System.Web.Mvc.Controller controller)
        {
            var decorator = controller as TestControllerDecorator;
            if (decorator != null)
            {
                MockControllerContext = decorator.MockControllerContext;
                Component = decorator.Component;
            }
            else
            {
                Component = controller;
                MockControllerContext = new Mock<ControllerContext>();
            }
        }
    }
}