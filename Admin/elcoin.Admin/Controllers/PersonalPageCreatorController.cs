using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using bbom.Admin.Core;
using bbom.Admin.Core.DataExtensions;
using bbom.Admin.Core.Extensions;
using bbom.Admin.Core.Filters;
using bbom.Admin.Core.Filters.Block;
using bbom.Admin.Core.Identity;
using bbom.Admin.Core.Notifications;
using bbom.Admin.Core.Services.AccessService;
using bbom.Admin.Core.ViewModels.Account;
using bbom.Admin.Core.ViewModels.PersonalPage;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using bbom.Data.ModelPartials;
using bbom.Data.ModelPartials.Constants;
using bbom.Data.Repository.Interfaces;
using Microsoft.AspNet.Identity;

namespace elcoin.Admin.Controllers
{
    [Authorize]
    [RemoveSubdomain]
    public class PersonalPageCreatorController : Controller
    {
        private readonly IRepository<AspNetUser> _usersRepository;
        private readonly IRepository<Setting> _settingsRepository;
        private ApplicationUserManager _userManager;
        private readonly IAccessService _accessService;

        public PersonalPageCreatorController(IRepository<AspNetUser> usersRepository, 
            IRepository<Setting> settingsRepository,
            ApplicationUserManager userManager,
            IAccessService accessService)
        {
            _usersRepository = usersRepository;
            _settingsRepository = settingsRepository;
            _userManager = userManager;
            _accessService = accessService;
        }

        // GET: PersonalPageCreator
        [OnlyHightRoles]
        public ActionResult Index()
        {
            var user = _usersRepository.GetById(User.GetUserId());
            SetViewBagPersonalPage(user);
            return View();
        }

        //
        // POST: /PersonalPageCreator/Register
        /// <summary>
        /// Новая регистрация для персональной страницы
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(PersonalRegisterViewModel model)
        {
            var parentUser = _usersRepository.GetById(User.GetUserId());
            SetViewBagPersonalPage(parentUser);
            if (ModelState.IsValid)
            {
                model.UserName = CoreFasade.RegisterHelper.GetNewUserName(model.Name);
                
                var applicationUser = new ApplicationUser
                {
                    UserName = model.UserName,
                };
                var result = await _userManager.CreateAsync(applicationUser, model.UserName + GlobalConstants.PasswordPostfix);
                if (result.Succeeded)
                {
                    await CoreFasade.RegisterHelper.ExRegisterParamsAsync(model, applicationUser.Id,
                        GlobalConstants.DefaultTemplate, parentUser,
                        new[] {UserRole.NotUser, UserRole.NotWatch, UserRole.NotChangePassword, UserRole.NotRegister},
                        new string[] {}, HttpContext);
                    return RedirectToAction("Confirm", "Account", new {id = model.UserName});
                }
                AddErrors(result);
            }
            return View("Index", model);
        }

        [NonAction]
        private void SetViewBagPersonalPage(AspNetUser user)
        {
            //todo во ViewBag класть генерируемые имена для пользователя
            ViewBag.VideoUrls =
                _settingsRepository.GetAll()
                    .Single(s => s.Name == SettingType.VideoLink)
                    .DefaultSettingsValues.Select(value => new SelectListItem
                    {
                        Text = value.Description,
                        Value = value.Value
                    });
            ViewBag.Params =
                user.UserExtraRegParams.Select(
                    param => new SelectListItem {Value = param.ExtraRegParamId.ToString(), Text = param.ExtraRegParam.Name});
            var dis = _accessService.GetUserDiscounts(user, DiscountTypeStatus.Received)
                .Select(discount => new SelectListItem { Value = discount.Id.ToString(), Text = discount.Name }).ToList();
            dis.Add(new SelectListItem { Text = "", Value = "" });
            ViewBag.Discounts = dis;
        }

        //
        // GET: /PersonalPageCreator/PersonalPage
        [AllowAnonymous]
        [HandleJsonError]
        public ActionResult PersonalPage()
        {
            var userName = User.GetUserName();
            var user = _usersRepository.GetAll().SingleOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                throw new Exception("Пользователя не существует");
            }
            if (user.AspNetRoles.All(role => role.Name != UserRole.NotRegister))
            {
                return RedirectToAction("Index", "Home");
            }
            return
                View(new PersonalPageViewModel
                {
                    Name = user.Name,
                    Id = user.Id,
                    Suname = user.Suname,
                    Altname = user.Altname,
                    InvitedUserName = user.ParentAspNetUser.GetIO(),
                    Password = userName + "qQwe1*",
                    VideoLink =
                        CoreFasade.TemplateHelper.GetTemplateSetting(
                            CoreFasade.UsersHelper.GetUserActiveTemplate(user), user, SettingType.VideoLink).Value,
                    Background =
                        CoreFasade.TemplateHelper.GetTemplateSetting(
                            CoreFasade.UsersHelper.GetUserActiveTemplate(user), user, SettingType.Background)
                            .Value
                });
        }

        //
        // POST: /PersonalPageCreator/Confim
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Confim(PersonalPageViewModel model)
        {
            try
            {
                var userName = User.GetUserName();
                var userId = _usersRepository.GetAll().Single(u => u.UserName == userName).Id;
                //await UserManager.RemoveFromRoleAsync(userId, UserRole.NotRegister);
                await CoreFasade.UsersHelper.ActionUserRoleAsync(userName, userId, UserRole.NotRegister, HttpContext,
                    async (id, role) => await _userManager.RemoveFromRoleAsync(id, role));
                var claim = new Claim
                {
                    ValueType = ClaimType.ConfimInviteDate,
                    Value = DateTime.Now.ToString(CultureInfo.InvariantCulture)
                };
                CoreFasade.ClaimsHelper.SetSingleClaim(_usersRepository.GetById(userId), claim);
                return RedirectToAction("Dashboard", "Home");
            }
            catch (Exception e)
            {
                return Json(Alert.ShowError(e.Message));
            }
        }

        //
        // POST: /PersonalPageCreator/GetImages
        [HttpGet]
        public ActionResult GetImages()
        {
            string root = System.Web.HttpContext.Current.Server.MapPath(GlobalConstants.ImageBackgroundPath);
            var di = new DirectoryInfo(root);
            var files = di.GetFiles().Select(fi => new ImageJson {name = fi.Name}).ToList();
            return Json(files, JsonRequestBehavior.AllowGet);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}