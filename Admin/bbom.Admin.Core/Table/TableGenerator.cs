using System.Collections.Generic;
using System.Linq;
using bbom.Admin.Core.ViewModels.Common;

namespace bbom.Admin.Core.Table
{
    public abstract class TableGenerator<T> : ITableGenerator<T> where T : class
    {
        protected ICollection<TableButton> BaseTableButton;

        protected ICollection<TableButtonInRow> BaseTableButtonInRow;
        public string Controller { get; set; }
        public Dictionary<TableAction, string> Action { get; set; }

        public ICollection<string> Columns { get; set; }

        protected TableGenerator()
        {
            Controller = "Tables";
            Action = new Dictionary<TableAction, string>
            {
                {TableAction.Create, "Create"},
                {TableAction.Delete, "Delete"},
                {TableAction.Details, "Details"},
                {TableAction.Update, "Update"},
                {TableAction.GetAll, "GetAll"}
            };
            InitButtons();
        }

        protected virtual string GetUrl(TableAction action)
        {
            return $"/{Controller}/{Action[action]}" + "?table=" + typeof(T).Name;
        }

        public TableModel GetTableModel()
        {
            string tableName = typeof(T).Name;
            var tm = new TableModel
            {
                name = tableName,
                columns = Columns.ToArray(),
                link = GetUrl(TableAction.GetAll),
                buttons = BaseTableButton.ToArray(),
                select = false
            };
            if (BaseTableButtonInRow.Count > 0)
            {
                var columnActionIndex = Columns.Count;
                Columns.Add("Действия");
                tm.columns = Columns.ToArray();
                tm.buttonsInRow = new[]
                {
                    new TableButtonGroup
                    {
                        columnIndex = columnActionIndex,
                        style = null,
                        buttons = BaseTableButtonInRow.ToArray()
                    }
                };
            }
            return tm;
        }

        public abstract ICollection<object> GetAll();

        public abstract void Create(string tableName);

        public abstract void Update(string tableName);

        public abstract void Delete(string tableName);

        public abstract void Details(string tableName);

        public virtual void InitButtons()
        {
            BaseTableButton = new[]
            {
                new TableButton
                {
                    name = "Создать",
                    inRow = false,
                    link = GetUrl(TableAction.Create),
                    needId = false
                }
            };
            BaseTableButtonInRow = new[]
            {
                new TableButtonInRow
                {
                    name = "Изменить",
                    link = GetUrl(TableAction.Update),
                    needId = true,
                    _class = "edit",
                    ajax = false,
                    inRow = true
                },
                new TableButtonInRow
                {
                    name = "Удалить",
                    link = GetUrl(TableAction.Delete),
                    needId = true,
                    _class = "delete",
                    ajax = false,
                    inRow = true
                },
                new TableButtonInRow
                {
                    name = "Подробнее",
                    link = GetUrl(TableAction.Details),
                    needId = true,
                    _class = "watch",
                    ajax = false,
                    inRow = true
                }
            };
        }
    }
}