using System.Collections.Generic;
using bbom.Admin.Core.ViewModels.Common;
using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.ViewModels.Users
{
    public class UserMenagerViewModel
    {
        public UserMenagerViewModel()
        {
            SelectedRoles = new List<AspNetRole>();
            PostedRoles = new PostedRoles();
        }

        public IList<AspNetRole> AvailableRoles { get; set; }
        public IList<AspNetRole> SelectedRoles { get; set; }
        public PostedRoles PostedRoles { get; set; }
        public AspNetUser User { get; set; }
        public string UserId { get; set; }
    }
}