using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.ViewModels.Pay
{
    public class PayViewModel
    {
        public ICollection<PaymentPlan> PaymentPlans { get; set; }
        public ICollection<Discount> Discounts { get; set; }

        [Required]
        public int selectPlanId { get; set; }
        public int? selectDiscountId { get; set; }

    }
}