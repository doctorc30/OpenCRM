using System.Collections.Generic;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using bbom.Admin.Controllers;
using bbom.Admin.Core;
using bbom.Admin.Core.DataExtensions.Helpers.Interfaces;
using bbom.Admin.Core.Identity;
using bbom.Admin.Core.Menu;
using bbom.Admin.Core.Services.AccessService;
using bbom.Admin.Core.Services.DiscountService;
using bbom.Admin.Core.Services.PaymentService;
using bbom.Admin.Core.Services.PaySystemService;
using bbom.Admin.Core.ViewModels;
using bbom.Data;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using bbom.Data.Repository.Interfaces;
using Microsoft.Owin.Security;
using Moq;
using Ninject;
using Solomon.Test.Mock.Controller;
using Solomon.Test.Mock.Repository;

namespace Solomon.Test.TestTools
{
    public class UnitTestHelper
    {
        private static UnitTestHelper _unitTestHelper;
        private IKernel Kernel { get; set; }
        private IKernel MockKernel { get; set; }
        public static UnitTestHelper GetInstance()
        {
            if (_unitTestHelper == null)
            {
                _unitTestHelper = new UnitTestHelper();
                return _unitTestHelper;
            }
            return _unitTestHelper;
        }

        public void Init()
        {
            Kernel = new StandardKernel();
            MockKernel = new StandardKernel();

            InitKernelMock(MockKernel);
            InitKernel(Kernel);
            NinjectAdminCoreKernel.GetInstance().SetBindings(Kernel);
            //NinjectDataCore.GetInstance().SetBindings(Kernel);

            NinjectDataCore.GetInstance().SetKernel(MockKernel);
            NinjectAdminCoreKernel.GetInstance().SetKernel(MockKernel);
        }

        public T GetService<T>()
        {
            if (typeof(T).BaseType == typeof(Controller))
            {
                T controller = (T)MockKernel.GetService(typeof(T));
                return controller;
            }
            return (T) Kernel.GetService(typeof(T));
        }

        private void InitKernelMock(IKernel kernel)
        {
            //Services
            kernel.Bind<IDiscountService>().ToMethod(delegate
            {
                var moq = new Mock<IDiscountService>();
                moq.Setup(service => service.GetAllAlowDiscounts(It.IsAny<AspNetUser>()))
                    .Returns(new List<Discount> {new MockObjectCreator<Discount>().Create()});
                moq.Setup(service => service.GetAllAlowDiscounts(It.IsAny<AspNetUser>(), It.IsAny<ICollection<PaymentPlan>>()))
                    .Returns(new List<Discount> { new MockObjectCreator<Discount>().Create() });
                moq.Setup(s => s.SetUserDiscountOfInvite(It.IsAny<string>()));
                return moq.Object;
            });
            kernel.Bind<IPaySystemService>().ToMethod(delegate
            {
                var moq = new Mock<IPaySystemService>();
                moq.Setup(
                    service => service.ProcessingRespons(It.IsAny<IPaySystemOptions>(), It.IsAny<string>()))
                    .Returns(true);
                return moq.Object;
            });
            kernel.Bind<IPaymentService>().ToMethod(delegate
            {
                var moq = new Mock<IPaymentService>();
                moq.Setup(service => service.ConfimPayment(It.IsAny<decimal>(), It.IsAny<int>()));
                moq.Setup(service => service.CreatePayment(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>()))
                    .ReturnsAsync(new MockObjectCreator<Payment>().Create());
                return moq.Object;
            });
            kernel.Bind<IAccessService>().ToMethod(delegate
            {
                var moq = new Mock<IAccessService>();
                moq.Setup(service => service.GetUserAlowDiscountTypes(It.IsAny<AspNetUser>()))
                    .Returns(new List<DiscountType> {new MockObjectCreator<DiscountType>().Create()});
                moq.Setup(service => service.GetUserAlowEvents(It.IsAny<AspNetUser>()))
                    .Returns(new List<Event> { new MockObjectCreator<Event>().Create() });
                moq.Setup(service => service.GetUserAlowMenus(It.IsAny<AspNetUser>()))
                    .Returns(new List<MenuJson> { new MockObjectCreator<MenuJson>().Create() });
                moq.Setup(service => service.GetUserAlowPaymentPlans(It.IsAny<AspNetUser>()))
                    .Returns(new List<PaymentPlan> { new MockObjectCreator<PaymentPlan>().Create() });
                moq.Setup(service => service.GetUserDiscounts(It.IsAny<AspNetUser>(), It.IsAny<int>()))
                    .Returns(new List<Discount> { new MockObjectCreator<Discount>().Create() });
                return moq.Object;
            });

            //Helpers
            kernel.Bind<IUsresHelper>().ToMethod(delegate
            {
                var moq = new Mock<IUsresHelper>();
                moq.Setup(service => service.GetAllAlowPaymentPlans(It.IsAny<AspNetUser>()))
                    .Returns(new List<PaymentPlan> { new MockObjectCreator<PaymentPlan>().Create() });
                return moq.Object;
            });
            kernel.Bind<ISettingsHelper>().ToMethod(delegate
            {
                var moq = new Mock<ISettingsHelper>();
                return moq.Object;
            });
            kernel.Bind<IClaimsHelper>().ToMethod(delegate
            {
                var moq = new Mock<IClaimsHelper>();
                return moq.Object;
            });
            kernel.Bind<IRegisterHelper>().ToMethod(delegate
            {
                var moq = new Mock<IRegisterHelper>();
                return moq.Object;
            });
            kernel.Bind<ITemplateHelper>().ToMethod(delegate
            {
                var moq = new Mock<ITemplateHelper>();
                return moq.Object;
            });

            kernel.Bind<ApplicationUserManager>().ToMethod(delegate
            {
                var moq = new Mock<ApplicationUserManager>();
                return moq.Object;
            });
            kernel.Bind<IAuthenticationManager>().ToMethod(delegate
            {
                var moq = new Mock<IAuthenticationManager>();
                return moq.Object;
            });
            kernel.Bind<IMenuGenerator>().ToMethod(delegate
            {
                var moq = new Mock<IMenuGenerator>();
                return moq.Object;
            });

            //Controllers
            kernel.Bind<PaymentController>().ToSelf();

            //Ropository
            kernel.Bind(typeof(IRepository<>)).To(typeof(MockRepository<>));
        }

        private void InitKernel(IKernel kernel)
        {
            kernel.Bind<IPaymentService>().To<PaymentService>().InTransientScope();
        }

        public static void SetContext(Controller controller, AspNetUser user)
        {
            //IdentityControllerDecorator icd = new IdentityControllerDecorator(user);
            //RouteDataControllerDecorator rdcd = new RouteDataControllerDecorator(user);
            //HttpContextControllerDecorator hccd = new HttpContextControllerDecorator();

            //hccd.SetComponent(controller);
            //icd.SetComponent(hccd);
            //rdcd.SetComponent(icd);

            var mockControllerContext = new Mock<ControllerContext>();
            var context = new Mock<HttpContextBase>();
            var request = new Mock<HttpRequestBase>();
            var response = new Mock<HttpResponseBase>();
            var session = new Mock<HttpSessionStateBase>();
            var server = new Mock<HttpServerUtilityBase>();
            var userMock = new Mock<IPrincipal>();
            var identity = new Mock<IIdentity>();

            context.Setup(c => c.Request).Returns(request.Object);
            context.Setup(c => c.Response).Returns(response.Object);
            context.Setup(c => c.Session).Returns(session.Object);
            context.Setup(c => c.Server).Returns(server.Object);
            context.Setup(c => c.User).Returns(userMock.Object);
            context.SetupGet(x => x.Cache).Returns(HttpRuntime.Cache);
            userMock.Setup(c => c.Identity).Returns(identity.Object);
            identity.Setup(i => i.IsAuthenticated).Returns(true);
            identity.Setup(i => i.Name).Returns(user.Name);
            mockControllerContext.SetupGet(cc => cc.HttpContext).Returns(context.Object);

            controller.ControllerContext = mockControllerContext.Object;
        }
    }
}