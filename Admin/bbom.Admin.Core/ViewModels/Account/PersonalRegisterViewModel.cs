using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace bbom.Admin.Core.ViewModels.Account
{
    public class PersonalRegisterViewModel
    {
        //[Required]
        [DataType(DataType.Text)]
        [Display(Name = "Login")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Фамилия")]
        public string Suname { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Отчество")]
        public string Altname { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Телефон")]
        public string Phone { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Скайп")]
        public string Skype { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Ссылка на видео")]
        public string VideoUrl { get; set; }

        [Display(Name = "Параметр")]
        public string SelectParamId { get; set; }

        [Display(Name = "Скидка")]
        public string DiscountId { get; set; }
        public IEnumerable<SelectListItem> Discounts { get; set; }
        public string imagePicker { get; set; }
    }
}