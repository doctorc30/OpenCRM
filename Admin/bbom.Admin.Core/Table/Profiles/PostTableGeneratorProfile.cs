using System.Collections.Generic;
using System.Linq;
using bbom.Admin.Core.ViewModels.Common;
using bbom.Data;
using bbom.Data.ContentModel;

namespace bbom.Admin.Core.Table.Profiles
{
    public class PostTableGeneratorProfile : TableGenerator<Post>
    {
        public override void InitButtons()
        {
            Controller = "Posts";
            Columns = new List<string> {"№", "Заголовок", "Автор", "Тип", "Дата"};
            base.InitButtons();
            var btns =
                new List<TableButtonInRow>(
                    BaseTableButtonInRow.Where(
                        btn =>
                            !btn.link.Contains(Action[TableAction.Delete]) &&
                            !btn.link.Contains(Action[TableAction.Details])));
            BaseTableButtonInRow = btns;
        }

        protected override string GetUrl(TableAction action)
        {
            return $"/{Controller}/{Action[action]}";
        }

        public override ICollection<object> GetAll()
        {
            var posts = DataFasade.GetRepository<Post>().GetAll().ToList();
            var data = posts.Select(post => new List<string>
            {
                post.Id.ToString(),
                post.Title,
                "",//_usersRepository.GetById(post.UserId) == null ? "" : _usersRepository.GetById(post.UserId).GetIO(),
                post.PostType.Name,
                post.Date.ToString("d")
            }).Select(dataObject => dataObject.ToArray()).Cast<object>().ToList();
            return data;
        }

        public override void Create(string tableName)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(string tableName)
        {
            throw new System.NotImplementedException();
        }

        public override void Delete(string tableName)
        {
            throw new System.NotImplementedException();
        }

        public override void Details(string tableName)
        {
            throw new System.NotImplementedException();
        }
    }
}