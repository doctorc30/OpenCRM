using System.Web.Mvc;
using bbom.Admin.Controllers;
using bbom.Data.IdentityModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solomon.Test;
using Solomon.Test.Mock.Repository;
using Solomon.Test.TestTools;

namespace bbom.Admin.Test.Controllers
{
    [TestClass]
    public class PaymentControllerTests : BaseTests
    {
        [TestMethod]
        public void IndexTest()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<PaymentController>();
            var user = new MockObjectCreator<AspNetUser>().Create();
            UnitTestHelper.SetContext(controller, user);

            // Act
            ActionResult result = controller.Index() as ActionResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}