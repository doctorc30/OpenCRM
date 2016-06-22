using System.Collections.Generic;
using bbom.Admin.Core.ViewModels;

namespace bbom.Admin.Core.Menu
{
    public interface IMenuGenerator
    {
        ICollection<MenuJson> GetMenu();
    }
}