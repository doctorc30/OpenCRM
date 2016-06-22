using System.ComponentModel.DataAnnotations;
using bbom.Admin.Core.ViewModels.Account;

namespace bbom.Admin.Core.ViewModels.Registrator
{
    public class RegistratorViewModel : PersonalRegisterViewModel
    {
        [EmailAddress]
        [Display(Name = "Адрес электронной почты")]
        public string Email { get; set; }
        public string ReferalUserName { get; set; }
        public string Background { get; set; }
    }
}