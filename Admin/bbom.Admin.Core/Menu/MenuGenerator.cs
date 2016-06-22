using System.Collections.Generic;
using System.Linq;
using bbom.Admin.Core.ViewModels;
using bbom.Data;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModelPartials.Comparers;

namespace bbom.Admin.Core.Menu
{
    public class MenuGenerator : IMenuGenerator
    {
        protected string ValidateSetting;

        protected bool Validate()
        {
            try
            {
                var setVal =
                    DataFasade.GetRepository<Setting>()
                        .GetAll()
                        .SingleOrDefault(setting => setting.Name == ValidateSetting)
                        .DefaultSettingsValues.FirstOrDefault().Value;
                if (setVal == "1")
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public virtual ICollection<MenuJson> GetMenu()
        {
            List<IMenuGenerator> menuGenerators = new List<IMenuGenerator>
            {
                new EventsMenuGenerator()
            };
            var menus = DataFasade.GetRepository<Data.ContentModel.Menu>().GetAll().ToList();
            var mainMenus = GetSubMenu(null, menus).ToList();
            foreach (var generator in menuGenerators)
            {
                mainMenus.AddRange(generator.GetMenu());
            }
            return mainMenus.OrderBy(json => json.order).ToList();
        }

        private ICollection<MenuJson> GetSubMenu(int? menuId, ICollection<Data.ContentModel.Menu> allMenus)
        {
            var subMenus = allMenus.Where(menu1 => menu1.ParentId == menuId).ToList();
            subMenus.Sort(new MenuComparer());
            var subMenusJson = new List<MenuJson>();
            foreach (var subMenu in subMenus)
            {
                if (subMenu.Enable == 1)
                    //todo мапить (MenuJson 2)
                    subMenusJson.Add(new MenuJson
                    {
                        id = subMenu.Id,
                        name = subMenu.Name,
                        discription = subMenu.Discription,
                        icon = subMenu.Icon,
                        action = subMenu.Action,
                        controller = subMenu.Controller,
                        order = subMenu.OrderNum,
                        SubMenusJsons = GetSubMenu(subMenu.Id, allMenus)
                    });
            }
            return subMenusJson;
        }
    }
}