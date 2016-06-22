using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using bbom.Admin.Controllers;
using bbom.Admin.Core.Notifications;
using bbom.Admin.Core.ViewModels.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace bbom.Admin.Test.Controllers
{
    [TestClass]
    public class EventsControllerTests : BaseTests
    {
        [TestMethod]
        public void EventsControllerTest()
        {
           
        }

        [TestMethod]
        public async Task AddTest()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<EventsController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.Admin);
            // Act
            JsonResult result = await controller.Add(new EventJson
            {
                end = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"),
                start = DateTime.Today.ToString("yyyy-MM-dd"),
                url = "",
                title = "test"
            });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(Alert.Success.type, ((Alert)result.Data).type);
        }

        [TestMethod]
        public async Task EditTest()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<EventsController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.Admin);
            var e = new EventJson
            {
                end = DateTime.Today.AddDays(1).ToString("yyyy-MM-dd"),
                start = DateTime.Today.ToString("yyyy-MM-dd"),
                url = "",
                title = "test",
                name = "1"
            };
            // Act
            JsonResult result = await controller.Edit(e);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(Alert.Success.type, ((Alert)result.Data).type);
        }

        [TestMethod]
        public void GetAllJsonTest()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<EventsController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.NotUser);

            // Act
            JsonResult result = controller.GetAllJson() as JsonResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetLastJsonTest()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<EventsController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.NotUser);

            // Act
            JsonResult result = controller.GetLastJson() as JsonResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}