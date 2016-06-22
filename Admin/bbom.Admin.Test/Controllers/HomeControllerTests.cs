using System.Web.Mvc;
using bbom.Admin.Controllers;
using bbom.Admin.Core.ViewModels.Home;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace bbom.Admin.Test.Controllers
{
    [TestClass]
    public class HomeControllerTests : BaseTests
    {
        [TestMethod]
        public void HomeControllerTest()
        {

        }

        [TestMethod]
        public void IndexTest()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<HomeController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.Non);
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void DashboardTest()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<HomeController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.User);
            // Act
            ViewResult result = controller.Dashboard() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.ViewData.Model, typeof(DashboardViewModel));
        }

        [TestMethod]
        public void CalendarTest()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<HomeController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.User);
            // Act
            ViewResult result = controller.Dashboard() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void NotUserInfoTest()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<HomeController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.User);
            // Act
            ViewResult result = controller.Dashboard() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UsersTest()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<HomeController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.User);

            // Act
            ViewResult result = controller.Users() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}