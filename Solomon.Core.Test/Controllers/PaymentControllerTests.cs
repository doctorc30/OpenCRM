using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using bbom.Admin.Controllers;
using bbom.Admin.Core.ViewModels.Pay;
using bbom.Data.IdentityModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Solomon.Test.Mock.Controller;
using Solomon.Test.Mock.Repository;
using Solomon.Test.TestTools;

namespace Solomon.Test.Controllers
{
    [TestClass]
    public class PaymentControllerTests : BaseTest
    {
        [TestMethod]
        public void IndexTest()
        {
            // Arrange
            var controller = UnitTestHelper.GetInstance().GetService<PaymentController>();
            var user = new MockObjectCreator<AspNetUser>().Create();
            UnitTestHelper.SetContext(controller, user);

            // Act
            ActionResult result = controller.Index();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetPaymentDiscountsJsonTest()
        {
            // Arrange
            var controller = UnitTestHelper.GetInstance().GetService<PaymentController>();
            var user = new MockObjectCreator<AspNetUser>().Create();
            var paymentId = new MockObjectCreator<PaymentPlan>().Create().Id;
            UnitTestHelper.SetContext(controller, user);

            // Act
            ActionResult result = controller.GetPaymentDiscountsJson(paymentId.ToString());

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task StartPayTestWithDiscount()
        {
            // Arrange
            var controller = UnitTestHelper.GetInstance().GetService<PaymentController>();
            var user = new MockObjectCreator<AspNetUser>().Create();
            UnitTestHelper.SetContext(controller, user);

            // Act
            ActionResult result = await controller.StartPay(new PayViewModel
            {
                Discounts = null,
                PaymentPlans = new List<PaymentPlan> { new MockObjectCreator<PaymentPlan>().Create()},
                selectDiscountId = new MockObjectCreator<Discount>().Create().Id,
                selectPlanId = new MockObjectCreator<PaymentPlan>().Create().Id
            });

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task StartPayTestWithoutDiscount()
        {
            // Arrange
            var controller = UnitTestHelper.GetInstance().GetService<PaymentController>();
            var user = new MockObjectCreator<AspNetUser>().Create();
            UnitTestHelper.SetContext(controller, user);

            // Act
            ActionResult result = await controller.StartPay(new PayViewModel
            {
                Discounts = null,
                PaymentPlans = new List<PaymentPlan> { new MockObjectCreator<PaymentPlan>().Create() },
                selectDiscountId = null,
                selectPlanId = new MockObjectCreator<PaymentPlan>().Create().Id
            });

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PaidTest()
        {
            // Arrange
            var controller = UnitTestHelper.GetInstance().GetService<PaymentController>();
            var user = new MockObjectCreator<AspNetUser>().Create();
            var payment = new MockObjectCreator<Payment>().Create();
            UnitTestHelper.SetContext(controller, user);

            // Act
            ActionResult result = controller.Paid("", "", payment.Id.ToString(), "", payment.Amount.ToString(),
                payment.Amount.ToString(), "", "", "", "", "", "");

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void PayTest()
        {
            // Arrange
            var controller = UnitTestHelper.GetInstance().GetService<PaymentController>();
            var user = new MockObjectCreator<AspNetUser>().Create();
            var payment = new MockObjectCreator<Payment>().Create();
            UnitTestHelper.SetContext(controller, user);
            HttpRuntime.Cache[user.Name + "PaymentId"] = payment.Id;

            // Act
            ActionResult result = controller.Pay();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ConfimPaid()
        {
            // Arrange
            var controller = UnitTestHelper.GetInstance().GetService<PaymentController>();
            var payment = new MockObjectCreator<Payment>().Create();

            // Act
            ActionResult result = controller.ConfimPaid(payment.Id);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}