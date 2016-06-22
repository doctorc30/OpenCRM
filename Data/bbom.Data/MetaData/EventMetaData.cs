using System;
using System.ComponentModel.DataAnnotations;
using bbom.Data.IdentityModel;

namespace bbom.Data.MetaData
{
    public class EventMetaData
    {
        public int Id { get; set; }

        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Начальная дата")]
        public Nullable<System.DateTime> StartDate { get; set; }

        [Display(Name = "Конечная дата")]
        public System.DateTime EndDate { get; set; }

        [Display(Name = "Ссылка")]
        public string Url { get; set; }

        [Display(Name = "Вид события")]
        public string TypeId { get; set; }

        [Display(Name = "Пользователь")]
        public virtual AspNetUser AspNetUser { get; set; }

        [Display(Name = "Спикер")]
        public string Spiker { get; set; }

        [Display(Name = "Иконка")]
        public string Icon { get; set; }

        [Display(Name = "Подробное описание")]
        public string Description { get; set; }

        [Display(Name = "Спикер")]
        public string SpikerId { get; set; }
    }
}