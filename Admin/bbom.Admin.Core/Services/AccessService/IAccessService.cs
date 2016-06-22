using System.Collections.Generic;
using bbom.Admin.Core.ViewModels;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using bbom.Data.ModelPartials;
using bbom.Data.Repository.Interfaces;

namespace bbom.Admin.Core.Services.AccessService
{
    public delegate object AccessObjectInsertCallBack(string roleId, string id);
    public interface IAccessService
    {
        ICollection<MenuJson> GetUserAlowMenus(AspNetUser user);
        ICollection<Event> GetUserAlowEvents(AspNetUser user);
        ICollection<DiscountType> GetUserAlowDiscountTypes(AspNetUser user);
        ICollection<Discount> GetUserDiscounts(AspNetUser user, int discountTypeStatus);
        ICollection<PaymentPlan> GetUserAlowPaymentPlans(AspNetUser user);

        void UpdateAccessToEntity<T>(string[] ids, string roleId, IRepository<T> repository,
            AccessObjectInsertCallBack accessObjectInsertCallBack) where T : class, IAccessSecurity;
    }
}