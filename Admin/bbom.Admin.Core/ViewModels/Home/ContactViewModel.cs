using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using bbom.Data.IdentityModel;

namespace bbom.Admin.Core.ViewModels.Home
{
    public class ContactViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Электронная почта")]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Пригласивший пользователь")]
        public string InvitedFio { get; set; }
        public string InvitedId { get; set; }
        public string Link { get; set; }
        public ICollection<UserCommunication> Comms { get; set; }
    }
}