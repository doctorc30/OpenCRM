using System;
using System.Collections.Generic;
using System.Linq;
using bbom.Admin.Core.ViewModels;
using bbom.Data;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using bbom.Data.ModelPartials;
using bbom.Data.Repository.Interfaces;

namespace bbom.Admin.Core.Services.AccessService
{
    public class AccessServise : IAccessService
    {
        public ICollection<MenuJson> GetUserAlowMenus(AspNetUser user)
        {
            //var menuCahce = HttpContext.Current.Cache[user.UserName + "Menu"] as ICollection<MenuJson>;
            //if (menuCahce != null)
            //{
            //    Trace.TraceInformation("Menu-Cache");
            //    return menuCahce;
            //}
            var menu = CoreFasade.MenuGenerator.GetMenu();
            var accessMenusId = new List<int>();
            var userRolesIds = new List<string>();
            foreach (var role in user.AspNetRoles)
            {
                accessMenusId.AddRange(role.AccessToMenu.Select(menu1 => menu1.Id));
                userRolesIds.Add(role.Id);
            }
            foreach (var menuJson in menu)
            {
                if (menuJson.id == 0)
                {
                    CheckCustomRight(menuJson, userRolesIds);
                }
                else
                {
                    CheckRights(menuJson, accessMenusId, false);
                }
            }
            //HttpContext.Current.Cache.Add(user.UserName + "Menu", menu, null, DateTime.MaxValue,
            //        TimeSpan.FromMinutes(3), CacheItemPriority.Default, null);
            //Trace.TraceInformation("Menu-BD");
            return menu;
        }

        private void CheckRights(MenuJson menuJson, ICollection<int> accessIds, bool isParentAccess)
        {
            if (accessIds.Contains(menuJson.id) || isParentAccess)
            {
                menuJson.isActive = true;
            }
            else
            {
                menuJson.isActive = false;
                menuJson.controller = "";
                menuJson.action = "";
            }
            foreach (var subMenusJson in menuJson.SubMenusJsons)
            {
                CheckRights(subMenusJson, accessIds, isParentAccess || menuJson.isActive);
            }
        }

        private void CheckCustomRight(MenuJson menuJson, ICollection<string> userRolesIds)
        {
            dynamic accesRepo = DataFasade.GetRepository(menuJson.accessType).GetAll();
            var accessTypes = new List<int>();
            foreach (var access in accesRepo)
            {
                if (userRolesIds.Contains((string)access.RoleId))
                {
                    //todo сдулать id одинаковым в таблицах
                    accessTypes.Add((int)access.EventTypeId);
                }
            }
            if (accessTypes.Contains(Convert.ToInt32(menuJson.exParam)))
            {
                menuJson.isActive = true;
                menuJson.accessType = null;
                menuJson.destinationType = null;
            }
            else
            {
                menuJson.isActive = false;
                menuJson.controller = "";
                menuJson.action = "";
                menuJson.accessType = null;
                menuJson.destinationType = null;
            }
        }

        public ICollection<Event> GetUserAlowEvents(AspNetUser user)
        {
            var accessEtIds = new List<int>();
            foreach (var role in user.AspNetRoles)
            {
                accessEtIds.AddRange(role.AccessToEventType.Select(et => et.Id));
            }
            var events = DataFasade.GetRepository<Event>().GetAll().Where(e => accessEtIds.Contains(e.EventType.Id)).ToList();
            return events;
        }

        public ICollection<DiscountType> GetUserAlowDiscountTypes(AspNetUser user)
        {
            var accessDTs = new List<DiscountType>();
            foreach (var role in user.AspNetRoles)
            {
                accessDTs.AddRange(
                    role.AccessToDiscountType);
            }
            return accessDTs;
        }

        public ICollection<Discount> GetUserDiscounts(AspNetUser user, int discountTypeStatus)
        {
            var accessDtIds = new List<int>();
            foreach (var role in user.AspNetRoles)
            {
                accessDtIds.AddRange(
                    role.AccessToDiscountType.Where(type => type.Status != null && type.Status == discountTypeStatus)
                        .Select(dt => dt.Id));
            }
            var dis = DataFasade.GetRepository<Discount>().GetAll().Where(d => accessDtIds.Contains(d.DiscountTypeId)).ToList();
            return dis;
        }

        public ICollection<PaymentPlan> GetUserAlowPaymentPlans(AspNetUser user)
        {
            return DataFasade.GetRepository<PaymentPlan>().GetAll().ToList();
        }

        public void UpdateAccessToEntity<T>(string[] ids, string roleId, IRepository<T> repository,
            AccessObjectInsertCallBack accessObjectInsertCallBack) where T : class, IAccessSecurity
        {
            var ac = repository.GetAll().Where(service => service.RoleId == roleId);
            foreach (var accessToMenu in ac)
            {
                repository.Delete(accessToMenu);
            }
            if(ids == null)
                ids = new string[0];
            foreach (var id in ids)
            {
                repository.Insert((T) accessObjectInsertCallBack(roleId, id));
            }
            repository.SaveChanges();
        }
    }
}