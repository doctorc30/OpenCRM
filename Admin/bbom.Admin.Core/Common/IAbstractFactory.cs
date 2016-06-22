using bbom.Admin.Core.Services.PaymentService;
using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.Common
{
    public interface IAbstractFactory
    {
        IPaymentService CreatePaymentService();
        IPaymentService CreatePaymentService(int paymentId);
    }
}