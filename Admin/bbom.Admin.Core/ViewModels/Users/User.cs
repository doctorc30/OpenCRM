using System.Collections.Generic;
using bbom.Admin.Core.ViewModels.Communications;

namespace bbom.Admin.Core.ViewModels.Users
{
    public class User
    {
        public User()
        {
        }

        public User(string userName, string email, string parentUserName)
        {
            this.userName = userName;
            this.email = email;
            this.parentUserName = parentUserName;
        }

        public string userName { get; set; }
        public string email { get; set; }
        public string foot { get; set; }
        public string parentUserName { get; set; }
        public string firstLineCount { get; set; }
        public string allChildrensCount { get; set; }
        public string itCanBeUpgraded { get; set; }
        public string ConnectionId { get; set; }
        public string itNotWatched { get; set; }
        public ICollection<CommunicationJson> communications{ get; set; }
        public ICollection<string> roles{ get; set; }
        public string lastLogin { get; set; }
        public string confimInvite { get; set; }
        public string fio { get; set; }
        public string isUser { get; set; }
        public string isWatch { get; set; }
    }
}