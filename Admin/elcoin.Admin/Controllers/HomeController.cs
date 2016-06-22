using System;
using System.Linq;
using System.Web.Mvc;
using bbom.Admin.Core;
using bbom.Admin.Core.DataExtensions.Helpers;
using bbom.Admin.Core.Extensions;
using bbom.Admin.Core.Filters;
using bbom.Admin.Core.Filters.Block;
using bbom.Admin.Core.ViewModels.Home;
using bbom.Admin.Core.ViewModels.Templates;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using bbom.Data.ModelPartials.Constants;
using bbom.Data.Repository.Interfaces;
using Ninject.Infrastructure.Language;

namespace elcoin.Admin.Controllers
{
    //[SkipAuthorize(Roles = UserRole.NotUser)]
    [Authorize]
    [RemoveSubdomain]
    public class HomeController : Controller
    {
        private readonly IRepository<AspNetUser> _usersRepository;
        private readonly IRepository<EventType> _eventsRepository;
        private readonly IRepository<AspNetRole> _rolesRepository;

        public HomeController(IRepository<AspNetUser> usersRepository, IRepository<EventType> eventsRepository,
            IRepository<AspNetRole> rolesRepository)
        {
            _usersRepository = usersRepository;
            _eventsRepository = eventsRepository;
            _rolesRepository = rolesRepository;
        }

        /// <summary>
        /// Главная страница пользователя с его рекламой, в идеале лединг
        /// временно отключена, идет редирект на Dashboard
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[AllowAnonymous]
        [BlockNotRegistr]
        public ActionResult Index()
        {
            try
            {
                //var userName = (string)RouteData.Values["subdomain"];
                //if (userName == null) 
                //    throw new ArgumentNullException("sub" + "domain");
                //var user = _usersRepository.GetAll().Single(netUser => netUser.UserName == userName);
                //return View(user.ActiveTemplate);
                return RedirectToAction("Dashboard", "Home");
            }
            catch (Exception e)
            {
                //todo выводить ошибку что нельзя перейти на этот домен
                return HttpNotFound(e.Message);
            }
        }

        /// <summary>
        /// Страница статистики (рабочий стол пользователя)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BlockNotRegistr]
        [BlockNotWatch]
        [BlockLowRoles]
        public ActionResult Dashboard()
        {
            var id = User.GetUserId();
            var allUsers = _usersRepository.Include("AspNetRoles");
            var user = _usersRepository.GetById(id);
            var childrenCount = CoreFasade.UsersHelper.GetAllChildren(user)
                .Count(UserFunctionHelper.IsSystemUser)
                .ToString();
            var allUserCount = allUsers
                .Count(UserFunctionHelper.IsSystemUser)
                .ToString();
            var newUsersCount = user.AspNetUsers1.Count(
                userChild => userChild.Foot == null && UserFunctionHelper.IsSystemUser(userChild)).ToString();
            var db = new DashboardViewModel
            {
                AllUsersCount = allUserCount,
                ChildrenUsersCount = childrenCount,
                NewUsersCount = newUsersCount,
                User = user
            };
            return View(db);
        }

        /// <summary>
        /// Страница о компании
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Страница контактов для пользователя
        /// его родитель и связь с тех подержкой
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Contact()
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var invitedUser = user.InvitedAspNetUser;
            string link = "";
            string link2 = "";
            try
            {
                var userExtraRegParam = invitedUser.UserExtraRegParams.FirstOrDefault(uxrp => uxrp.ExtraRegParam.Id == 2);
                if (userExtraRegParam != null)
                    ViewBag.Link = userExtraRegParam.Value;
                var extraRegParam = invitedUser.UserExtraRegParams.FirstOrDefault(uxrp => uxrp.ExtraRegParam.Id == 1);
                if (extraRegParam != null)
                    ViewBag.Link2 = extraRegParam.Value;
            }
            catch
            {
                //ignore
            }
            return View(new ContactViewModel
            {
                Email = user.InvitedAspNetUser.Email,
                InvitedFio = user.InvitedAspNetUser.GetFio(),
                Phone = user.InvitedAspNetUser.PhoneNumber,
                InvitedId = user.InvitedAspNetUser.Id,
                Link = link,
                Comms = user.InvitedAspNetUser.UserCommunications
            });
        }

        /// <summary>
        /// Календарь событий
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BlockNotWatch]
        [BlockLowRoles]
        public ActionResult Calendar()
        {
            ViewBag.EventTypes =
                _eventsRepository.GetAll()
                    .Select(value => new SelectListItem
                    {
                        Text = value.Name,
                        Value = value.Id.ToString()
                    });
            var spikerRole = _rolesRepository.GetAll().SingleOrDefault(role => role.Name == UserRole.Spiker);
            ViewBag.Spikers = spikerRole.AspNetUsers.Select(user => new SelectListItem
            {
                Text = user.UserName + "(" + user.GetIO() + ")",
                Value = user.Id
            });
            return View("Calendar2");
        }

        /// <summary>
        /// Страница ошибки прав доступа
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NotUserInfo()
        {
            return View();
        }

        /// <summary>
        /// Архив всех событий
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BlockNotWatch]
        [BlockLowRoles]
        public ActionResult Archive()
        {
            return View("Archive2");
        }

        /// <summary>
        /// Ближайшие мероприятия 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Events(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Events", new {id = 1});
            }
            ViewBag.TyeId = id;
            return View();
        }

        /// <summary>
        /// Ближайшие трененги 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [OnlyHightRoles]
        public ActionResult Trainings()
        {
            return View();
        }

        // GET: Users
        /// <summary>
        /// Страница структуры пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BlockNotWatch]
        [BlockLowRoles]
        public ActionResult Users()
        {
            return View("Users2");
        }

        // GET: Users
        /// <summary>
        /// Информация для пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Information()
        {
            return View();
        }

        // GET: Settings
        /// <summary>
        /// Настройки аккаунта пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BlockNotWatch]
        [BlockLowRoles]
        public ActionResult Settings()
        {
            var user = _usersRepository.GetById(User.GetUserId());
            ViewBag.Communications =
                CoreFasade.UsersHelper.GetNotAddedUserCommunications(user).Select(uc => new SelectListItem
                {
                    Text = uc.Name,
                    Value = uc.Id.ToString()
                }).ToEnumerable();
            ViewBag.ExRegParams =
                CoreFasade.UsersHelper.GetNotAddedUserExRegParams(user).Select(uc => new SelectListItem
                {
                    Text = uc.Name,
                    Value = uc.Id.ToString()
                }).ToEnumerable();
            return View(new TemplateSettings
            {
                Email = user.Email,
                Name = user.Name,
                Suname = user.Suname,
                Altname = user.Altname,
                PhoneNumber = user.PhoneNumber
            });
        }

        // GET: Profile
        /// <summary>
        /// Профиль пользователя со всей информации о нем
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BlockNotWatch]
        [BlockLowRoles]
        public ActionResult ProfileUser()
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var pvm = new ProfileViewModel
            {
                User = user,
                userName = user.UserName,
                ChildrenUsersCount = CoreFasade.UsersHelper.GetAllChildren(user).Count.ToString(),
                NewUsersCount = user.InvitedAspNetUsers.Count(netUser => netUser.Foot == null).ToString(),
                firstLineCount = CoreFasade.UsersHelper.GetFirstLine(user).Count.ToString()
            };
            return View("Profile", pvm);
        }
    }
}