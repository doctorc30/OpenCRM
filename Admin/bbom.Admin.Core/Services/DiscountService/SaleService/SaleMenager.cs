using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.Services.DiscountService.SaleService
{
    public static class SaleMenager
    {
        public static ISaleProvider GetSaleProvider(DiscountType type)
        {
            switch (type.Id)
            {
                case  Data.ModelPartials.Constants.DiscountType.InviteDiscountType:
                {
                    return new InviteSaleProvider();
                }
                default:
                    return new DefaulSaleProvider();
            }
        }
    }
}