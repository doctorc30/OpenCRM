using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using bbom.Admin.Core.ViewModels.Account;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.DataExtensions.Helpers.Interfaces
{
    public interface IRegisterHelper
    {
        void FillParams(PersonalRegisterViewModel model, AspNetUser user, Template template, AspNetUser parentUser);

        Task ExRegisterParamsAsync(PersonalRegisterViewModel model, string userId, int template, AspNetUser parentUser,
            ICollection<string> addRoles, ICollection<string> removeRoles, HttpContextBase httpContext);
        void SendConfimMail(string email, string confimUrl);
        string GetNewUserName(string userNameBase);
    }
}