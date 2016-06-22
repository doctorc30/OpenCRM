using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.ViewModels.Registrator
{
    public class RegViewModel
    {
        public AspNetUser ParentUser { get; set; }
        public string Background { get; set; }
        public string VideoUrl { get; set; }
        public string InvitedUserName { get; set; }
    }
}