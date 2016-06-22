using System.Collections.Generic;
using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.Services.DiscountService
{
    public interface IDiscountService
    {
        ICollection<Discount> GetAllAlowDiscounts(AspNetUser user);
        ICollection<Discount> GetAllAlowDiscounts(AspNetUser user, ICollection<PaymentPlan> paymentPlans);
        void SetUserDiscountOfInvite(string user);
    }
}