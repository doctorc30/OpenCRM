using System.ComponentModel.DataAnnotations;
using bbom.Admin.Core.ViewModels.Tables;

namespace bbom.Admin.Core.ViewModels.Post
{
    public class PostCreateViewModel : CreateViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Заголовок")]
        public string Title { get; set; }

        [Display(Name = "Текст")]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }

        [Display(Name = "Тип")]
        public int TypeId { get; set; }
    }
}