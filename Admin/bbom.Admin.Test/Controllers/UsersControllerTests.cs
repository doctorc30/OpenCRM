using System.Threading.Tasks;
using System.Web.Mvc;
using bbom.Admin.Controllers;
using bbom.Admin.Core.Notifications;
using bbom.Admin.Core.ViewModels.Users;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace bbom.Admin.Test.Controllers
{
    [TestClass]
    public class UsersControllerTests : BaseTests
    {
        [TestMethod]
        public void UsersControllerTest()
        {
            
        }

        [TestMethod]
        public void GetNewUsersJsonTest()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<UsersController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.User);

            // Act
            JsonResult result = controller.GetNewUsersJson();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetUserLineJsonTest()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<UsersController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.User);

            // Act
            JsonResult result = controller.GetUserLineJson(1, null, null, null);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetTreeUsersJsonTest()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<UsersController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.User);

            // Act
            JsonResult result = controller.GetTreeUsersJson(1, null);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetUserInfoJsonTest()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<UsersController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.User);

            // Act
            JsonResult result = controller.GetUserInfoJson(UnitTestControllerHelper.Users[UnitTestControllerHelper.UserRole.User].Name);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public async Task UpgradeRoleTest()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<UsersController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.Admin);

            // Act
            JsonResult result = await controller.UpgradeRole(UnitTestControllerHelper.Users[UnitTestControllerHelper.UserRole.NotUser].Name);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
        }

        [TestMethod]
        public void SetUsersFootTest()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<UsersController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.User);

            // Act
            JsonResult result = controller.SetUsersFoot(new UsersJson
            {
                users = new[]
                {
                    new User
                    {
                        foot = "1",
                        userName = UnitTestControllerHelper.Users[UnitTestControllerHelper.UserRole.User].Name
                    }
                }
            });

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Data);
            Assert.AreEqual(Alert.Success.type, ((Alert)result.Data).type);
        }
    }
}