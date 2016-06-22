using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.Services.DiscountService.SaleService
{
    public class DefaulSaleProvider : ISaleProvider
    {
        public virtual decimal Sale(PaymentPlan plan, Discount discount, string userId)
        {
            return plan.Amount - (plan.Amount * (discount.DiscountAmount / 100));
        }

        public virtual void Execute(Payment payment)
        {
        }
    }
}