using System.Threading.Tasks;
using System.Web.Mvc;
using bbom.Admin.Controllers;
using bbom.Admin.Core.ViewModels.Templates;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace bbom.Admin.Test.Controllers
{
    [TestClass]
    public class TemplatesControllerTests : BaseTests
    {
        [TestMethod]
        public void TemplatesControllerTest()
        {
            
        }

        [TestMethod]
        public void GetTemplatesTest()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<TemplatesController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.User);
            // Act
            JsonResult result = controller.GetTemplates() as JsonResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SiteTest()
        {
            //// Arrange
            //var controller = DependencyResolver.Current.GetService<TemplatesController>();
            //UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.User);
            //// Act
            //ViewResult result = controller.Site() as ViewResult;

            //// Assert
            //Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public async Task SaveActiveTemplateTest()
        //{
        //    //// Arrange
        //    //var controller = DependencyResolver.Current.GetService<TemplatesController>();
        //    //UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.User);
        //    //// Act
        //    //JsonResult result = await controller.SaveActiveTemplate(new TemplateSettings
        //    //{
        //    //    templateId = "1",
        //    //    userVideoLink = ""
        //    //});

        //    //// Assert
        //    //Assert.IsNotNull(result);
        //}

        [TestMethod]
        public void GetUserVideoLinkTest()
        {
            //// Arrange
            //var controller = DependencyResolver.Current.GetService<TemplatesController>();
            //UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.User);
            //// Act
            //ViewResult result = controller.Site() as ViewResult;

            //// Assert
            //Assert.IsNotNull(result);
        }
    }
}