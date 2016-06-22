using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using bbom.Admin.Core.Extensions;
using bbom.Admin.Core.Filters.Block;
using bbom.Admin.Core.Notifications;
using bbom.Admin.Core.Services.AccessService;
using bbom.Admin.Core.ViewModels.Access;
using bbom.Admin.Core.ViewModels.Common;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using bbom.Data.Repository.Interfaces;

namespace bbom.Admin.Controllers
{
    [Authorize]
    public class AccessController : Controller
    {
        private readonly IAccessService _accessService;
        private readonly IRepository<AspNetUser> _usersRepository;
        private readonly IRepository<AspNetRole> _rolesRepository;
        private readonly IRepository<Menu> _menusRepository;
        private readonly IRepository<AccessToMenu> _accessToMenusRepository;
        private readonly IRepository<AccessToEventType> _accessToEtRepository;
        private readonly IRepository<EventType> _eventsTypeRepository;

        public AccessController(IAccessService accessService, IRepository<AspNetUser> usersRepository,
            IRepository<AspNetRole> rolesRepository, IRepository<Menu> menusRepository,
            IRepository<AccessToMenu> accessToMenusRepository, IRepository<AccessToEventType> accessToEtRepository,
            IRepository<EventType> eventsTypeRepository)
        {
            _accessService = accessService;
            _usersRepository = usersRepository;
            _rolesRepository = rolesRepository;
            _menusRepository = menusRepository;
            _accessToMenusRepository = accessToMenusRepository;
            _accessToEtRepository = accessToEtRepository;
            _eventsTypeRepository = eventsTypeRepository;
        }

        // GET: Access
        [HttpGet]
        public ActionResult GetUserAlowMenu()
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var menus = _accessService.GetUserAlowMenus(user);
            return Json(menus, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [OnlyAdminAuthorize]
        public ActionResult Security()
        {
            ViewBag.Roles = _rolesRepository.GetAll().ToList();
            return View();
        }

        [HttpPost]
        public ActionResult SaveMenu(RoleIdsJson filter)
        {
            _accessService.UpdateAccessToEntity(filter.idsList, filter.role, _accessToMenusRepository,
                (id, s) => new AccessToMenu
                {
                    RoleId = id,
                    MenuId = Convert.ToInt32(s)
                });
            return Json(Alert.Success);
        }

        [HttpPost]
        public ActionResult SaveEvents(RoleIdsJson filter)
        {
            _accessService.UpdateAccessToEntity(filter.idsList, filter.role, _accessToEtRepository,
                (id, s) => new AccessToEventType
                {
                    RoleId = id,
                    EventTypeId = Convert.ToInt32(s)
                });
            return Json(Alert.Success);
        }

        [HttpPost]
        public JsonResult GetMenuByRole(string role)
        {
            var roleBd = _rolesRepository.GetById(role);
            var checkMenus = roleBd.AccessToMenu;
            var cbList = new List<CheckBoxJson>();
            foreach (var menu in _menusRepository.GetAll())
            {
                cbList.Add(new CheckBoxJson
                {
                    name = menu.Name,
                    value = menu.Id.ToString(),
                    isChecked = checkMenus.Contains(menu)
                });
            }
            return Json(cbList);
        }

        [HttpPost]
        public JsonResult GetEventsByRole(string role)
        {
            var roleBd = _rolesRepository.GetById(role);
            var checkMenus = roleBd.AccessToEventType;
            var cbList = new List<CheckBoxJson>();
            foreach (var et in _eventsTypeRepository.GetAll())
            {
                cbList.Add(new CheckBoxJson
                {
                    name = et.Name,
                    value = et.Id.ToString(),
                    isChecked = checkMenus.Contains(et)
                });
            }
            return Json(cbList);
        }
    }
}