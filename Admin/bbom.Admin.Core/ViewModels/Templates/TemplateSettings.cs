using System.ComponentModel.DataAnnotations;
using System.Web;

namespace bbom.Admin.Core.ViewModels.Templates
{
    public class TemplateSettings
    {
        public string templateId { get; set; }

        public string Email { get; set; }

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
        public string PhoneNumber { get; set; }

        public string userVideoLink { get; set; }

        [Display(Name = "Средство связи")]
        public int CommunicationId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Значение")]
        public string Value { get; set; }

        [Display(Name = "Дополнительный параметр")]
        public int ExRegParamId { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Значение")]
        public string ExRegParamValue { get; set; }

        [DataType(DataType.Upload)]
        public HttpPostedFileBase file { get; set; }
    }
}