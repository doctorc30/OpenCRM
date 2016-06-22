using bbom.Admin.Core.ViewModels.Users;
using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.ViewModels.Home
{
    public class ProfileViewModel: User
    {
        public AspNetUser User { get; set; }

        public string ChildrenUsersCount { get; set; }

        public string NewUsersCount { get; set; }
    }
}