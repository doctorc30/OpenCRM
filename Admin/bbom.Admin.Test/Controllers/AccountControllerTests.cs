using System.Threading.Tasks;
using System.Web.Mvc;
using bbom.Admin.Controllers;
using bbom.Admin.Core.ViewModels.Account;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace bbom.Admin.Test.Controllers
{
    [TestClass]
    public class AccountControllerTests : BaseTests
    {
        [TestMethod]
        public void AccountControllerTest()
        {

        }

        [TestMethod]
        public void AccountControllerTest1()
        {

        }

        [TestMethod]
        public void LoginTest_Get()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<AccountController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.Non);

            // Act
            ViewResult result = controller.Login("") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task LoginTest_Success_Post()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<AccountController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.Non);
            controller.SignInManager = UnitTestControllerHelper.GetApplicationSignInManager();
            controller.ViewData.ModelState.Clear();

            // Act
            RedirectToRouteResult result = await controller.Login(new LoginViewModel
            {
                UserName = "www",
                Password = "",
                RememberMe = false
            }, "") as RedirectToRouteResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task LoginTest_LockedOut_Post()
        {
            // Arrange
            var controller = DependencyResolver.Current.GetService<AccountController>();
            UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.Non);
            controller.SignInManager = UnitTestControllerHelper.GetApplicationSignInManager();
            controller.ViewData.ModelState.Clear();

            // Act
            ViewResult result = await controller.Login(new LoginViewModel
            {
                UserName = "qwe8",
                Password = "",
                RememberMe = false
            }, "") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void VerifyCodeTest()
        {

        }

        [TestMethod]
        public void VerifyCodeTest1()
        {

        }

        [TestMethod]
        public void RegisterTest()
        {
            //// Arrange
            //var controller = DependencyResolver.Current.GetService<AccountController>();
            //UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.Non);

            //// Act
            //ViewResult result = controller.Register() as ViewResult;

            //// Assert
            //Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public async Task RegisterTest_Post()
        //{
        //    //// Arrange
        //    //var controller = DependencyResolver.Current.GetService<AccountController>();
        //    //UnitTestControllerHelper.SetContext(controller, UnitTestControllerHelper.UserRole.Non);
        //    //controller.SignInManager = UnitTestControllerHelper.GetApplicationSignInManager();
        //    //controller.UserManager = UnitTestControllerHelper.GetApplicationUserManager();
        //    //controller.ViewData.ModelState.Clear();

        //    //// Act
        //    //RedirectToRouteResult result = await controller.Register(new RegisterViewModel
        //    //{
        //    //    UserName = "www",
        //    //    Email = "",
        //    //    Password = "",
        //    //    ConfirmPassword = ""
        //    //}) as RedirectToRouteResult;

        //    //// Assert
        //    //Assert.IsNotNull(result);
        //}

        //[TestMethod()]
        //public void ConfirmTest()
        //{

        //}

        //[TestMethod()]
        //public void ConfirmEmailTest()
        //{

        //}

        //[TestMethod()]
        //public void ForgotPasswordTest()
        //{

        //}

        //[TestMethod()]
        //public void ForgotPasswordTest1()
        //{

        //}

        //[TestMethod()]
        //public void ForgotPasswordConfirmationTest()
        //{

        //}

        //[TestMethod()]
        //public void ResetPasswordTest()
        //{

        //}

        //[TestMethod()]
        //public void ResetPasswordTest1()
        //{

        //}

        //[TestMethod()]
        //public void ResetPasswordConfirmationTest()
        //{

        //}

        //[TestMethod()]
        //public void ExternalLoginTest()
        //{

        //}

        //[TestMethod()]
        //public void SendCodeTest()
        //{

        //}

        //[TestMethod()]
        //public void SendCodeTest1()
        //{

        //}

        //[TestMethod()]
        //public void ExternalLoginCallbackTest()
        //{

        //}

        //[TestMethod()]
        //public void ExternalLoginConfirmationTest()
        //{

        //}

        //[TestMethod()]
        //public void LogOffTest()
        //{

        //}

        //[TestMethod()]
        //public void ExternalLoginFailureTest()
        //{

        //}

        //[TestMethod()]
        //public void GetRolesJsonTest()
        //{

        //}
    }
}