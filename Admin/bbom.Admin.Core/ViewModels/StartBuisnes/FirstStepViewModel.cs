using System.ComponentModel.DataAnnotations;

namespace bbom.Admin.Core.ViewModels.StartBuisnes
{
    public class FirstStepViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}