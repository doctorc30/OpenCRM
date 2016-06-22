using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.ViewModels.Home
{
    public class DashboardViewModel
    {
        public AspNetUser User { get; set; }
        public string PostCount { get; set; }
        public string CategoryCount { get; set; }
        public string ChildrenUsersCount { get; set; }
        public string AllUsersCount { get; set; }
        public string NewUsersCount { get; set; }

    }
}