using System.Collections.Generic;
using bbom.Admin.Core.ViewModels.Common;

namespace bbom.Admin.Core.Table
{
    public interface ITableGenerator<T>
    {
        TableModel GetTableModel();
        ICollection<object> GetAll();
        void Create(string tableName);
        void Update(string tableName);
        void Delete(string tableName);
        void Details(string tableName);
    }
}