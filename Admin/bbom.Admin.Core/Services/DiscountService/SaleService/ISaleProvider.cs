using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.Services.DiscountService.SaleService
{
    public interface ISaleProvider
    {
        decimal Sale(PaymentPlan plan, Discount discount, string userId);
        void Execute(Payment payment);
    }
}