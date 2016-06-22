using bbom.Admin.Core.Services.PaymentService;
using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.Common
{
    public class AbstractFactory : IAbstractFactory
    {
        public virtual IPaymentService CreatePaymentService()
        {
            return null;
        }

        public virtual IPaymentService CreatePaymentService(int paymentId)
        {
            return null;
        }
    }
}