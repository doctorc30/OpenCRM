using System.Threading.Tasks;
using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.Services.PaymentService
{
    public delegate void EventContainer(int paymentId);
    public interface IPaymentService
    {
        event EventContainer OnPayComplited;
        event EventContainer OnPayError;
        Task<Payment> CreatePayment(string userId, int? discountId, int paymentPlanId);
        void ConfimPayment(decimal payAmount, int id);
    }
}