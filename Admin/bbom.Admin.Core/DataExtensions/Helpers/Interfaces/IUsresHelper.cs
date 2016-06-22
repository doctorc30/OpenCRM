using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using bbom.Admin.Core.DataExtensions.Helpers.Impl;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using Microsoft.AspNet.Identity;

namespace bbom.Admin.Core.DataExtensions.Helpers.Interfaces
{
    public interface IUsresHelper
    {
        ICollection<AspNetUser> GetAllChildren(AspNetUser user);
        ICollection<AspNetUser> GetAllChildren(AspNetUser user, int line);
        ICollection<AspNetUser> GetFirstLine(AspNetUser user);
        AspNetUser GetLastUserByFoot(AspNetUser user, int foot);
        IEnumerable<string> GetUserRoles(string userName);
        AspNetUser GetUser(string userName);
        void ClearUserCache(string userName, HttpContextBase httpContext);
        ICollection<Communicatio> GetNotAddedUserCommunications(AspNetUser user);
        Task UpdateCookie();
        ICollection<ExtraRegParam> GetNotAddedUserExRegParams(AspNetUser user);
        ICollection<PaymentPlan> GetAllAlowPaymentPlans(AspNetUser user);
        Template GetUserActiveTemplate(AspNetUser user);
        void SetUserActiveTemplate(AspNetUser user, int templateId);
        Task<IdentityResult> ActionUserRoleAsync(string userName, string userId, string role,
            HttpContextBase httpContext, UsersHelper.RoleActionCallBack roleActionCallBack);
        Task<IdentityResult> ActionUserRolesAsync(string userName, string userId, string[] roles,
            HttpContextBase httpContext, UsersHelper.RolesActionCallBack roleActionCallBack);
    }
}
