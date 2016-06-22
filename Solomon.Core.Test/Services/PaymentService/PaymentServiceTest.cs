using System.Threading.Tasks;
using bbom.Admin.Core.Services.PaymentService;
using bbom.Data.IdentityModel;
using bbom.Data.ModelPartials.Constants;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solomon.Test.Mock.Repository;
using Solomon.Test.TestTools;

namespace Solomon.Test.Services.PaymentService
{
    [TestClass]
    public class PaymentServiceTest : BaseTest
    {
        [TestMethod]
        public async Task CreatePaymentTestNotDiscount()
        {
            // Arrange
            var service = UnitTestHelper.GetInstance().GetService<IPaymentService>();
            var userId = new MockObjectCreator<AspNetUser>().Create().Id;
            var planId = new MockObjectCreator<PaymentPlan>().Create().Id;

            // Act
            var result = await service.CreatePayment(userId, null, planId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Status, PaymentStatus.Created);
            Assert.IsNotNull(result.AspNetUser);
            Assert.IsNotNull(result.PaymentPlanId);
        }

        [TestMethod]
        public async Task CreatePaymentTestWithDiscount()
        {
            // Arrange
            var service = UnitTestHelper.GetInstance().GetService<IPaymentService>();
            var userId = new MockObjectCreator<AspNetUser>().Create().Id;
            var planId = new MockObjectCreator<PaymentPlan>().Create().Id;
            var discountId = new MockObjectCreator<Discount>().Create().Id;

            // Act
            var result = await service.CreatePayment(userId, discountId, planId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Status, PaymentStatus.Created);
            Assert.IsNotNull(result.AspNetUser);
            Assert.IsNotNull(result.PaymentPlanId);
            Assert.IsNotNull(result.DiscountId);
        }

        [TestMethod]
        public void ConfimPayment()
        {
            // Arrange
            var service = UnitTestHelper.GetInstance().GetService<IPaymentService>();
            var paymentId = new MockObjectCreator<Payment>().Create().Id;
            var amount = new MockObjectCreator<PaymentPlan>().Create().Amount;

            // Assert
            service.OnPayComplited += id => { Assert.AreEqual(paymentId, id); };
            service.OnPayError += id => { Assert.IsFalse(true); };

            // Act
            service.ConfimPayment(amount, paymentId);
        }
    }
}