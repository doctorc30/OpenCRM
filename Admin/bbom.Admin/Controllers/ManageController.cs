using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using bbom.Admin.Core;
using bbom.Admin.Core.Extensions;
using bbom.Admin.Core.Filters.Block;
using bbom.Admin.Core.Identity;
using bbom.Admin.Core.Notifications;
using bbom.Admin.Core.ViewModels;
using bbom.Admin.Core.ViewModels.Templates;
using bbom.Admin.Core.ViewModels.Users;
using bbom.Data;
using bbom.Data.IdentityModel;
using bbom.Data.Repository.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace bbom.Admin.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        private readonly IRepository<AspNetUser> _usersRepository;
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController(IRepository<AspNetUser> usersRepository, ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            _usersRepository = usersRepository;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        //{
        //    UserManager = userManager;
        //    SignInManager = signInManager;
        //}

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager;
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager;
            }
            private set
            {
                _userManager = value;
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetProfileUserImage(string userId)
        {
            try
            {
                var user = await _usersRepository.GetByIdAsync(userId);
                if (user.Image != null)
                {
                    return File(user.Image, "image/jpg");
                }
                return File("~/content/img/profile/defprof.png", "image/jpg");
            }
            catch
            {
                return File("~/content/img/profile/defprof.png", "image/jpg");
            }
        }

        [HttpGet]
        public ActionResult GetProfileUserImageByName(string userName)
        {
            try
            {
                var user = _usersRepository.GetAll().SingleOrDefault(u=> u.UserName == userName);
                if (user?.Image != null)
                    return File(user.Image, "image/jpg");
                return File("~/content/img/profile/defprof.png", "image/jpg");
            }
            catch
            {
                return File("~/content/img/profile/defprof.png", "image/jpg");
            }
        }

        [HttpPost]
        public async Task<JsonResult> UserImageUpload()
        {
            var user = _usersRepository.GetById(User.GetUserId());
            //todo проверять размер
            foreach (string file in Request.Files)
            {
                var hpf = Request.Files[file];
                if (hpf.ContentLength == 0 || hpf.ContentLength > 3500000)
                    return Json(Alert.Error);
                byte[] imageBytes = new byte[hpf.ContentLength];
                hpf.InputStream.Read(imageBytes, 0, hpf.ContentLength);
                user.Image = imageBytes;
                await _usersRepository.SaveChangesAsync();
            }
            return Json(Alert.Success);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult SaveSettings(TemplateSettings model)
        {
            if (ModelState.IsValid)
            {
                var user = _usersRepository.GetById(User.GetUserId());
                //todo мапить
                user.Name = model.Name;
                user.Suname = model.Suname;
                user.Altname = model.Altname;
                user.PhoneNumber = model.PhoneNumber;
                _usersRepository.SaveChanges();
                return Json(Alert.Success);
            }
            return Json(Alert.Error);
        }

        [HttpPost]
        [OnlyAdminAuthorize]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> SaveRoles(UserMenagerViewModel um)
        {
            if (ModelState.IsValid)
            {
                var user = _usersRepository.GetById(um.UserId);
                var userRoles = user.AspNetRoles.ToList();
                var checkedRoles = um.PostedRoles.RoleIDs;
                var uncheckedRoles =
                    DataFasade.GetRepository<AspNetRole>()
                        .GetAll()
                        .Where(role => !um.PostedRoles.RoleIDs.Contains(role.Name))
                        .Select(role => role.Name)
                        .ToArray();
                var addRoles = checkedRoles.Where(role => !userRoles.Any(netRole => netRole.Name == role)).ToArray();
                var removeRoles = uncheckedRoles.Where(role => userRoles.Any(netRole => netRole.Name == role)).ToArray();
                var res = await
                    CoreFasade.UsersHelper.ActionUserRolesAsync(user.UserName, um.UserId, addRoles, HttpContext,
                        async (id, roles) => await UserManager.AddToRolesAsync(id, roles));
                var res2 = await
                    CoreFasade.UsersHelper.ActionUserRolesAsync(user.UserName, um.UserId, removeRoles, HttpContext,
                        async (id, roles) => await UserManager.RemoveFromRolesAsync(id, roles));
                return Json(Alert.Success);
            }
            return Json(Alert.Error);
        }

        //
        // GET: /Manage/Index
        public async Task<ActionResult> Index(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Ваш пароль изменен."
                : message == ManageMessageId.SetPasswordSuccess ? "Пароль задан."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Настроен поставщик двухфакторной проверки подлинности."
                : message == ManageMessageId.Error ? "Произошла ошибка."
                : message == ManageMessageId.AddPhoneSuccess ? "Ваш номер телефона добавлен."
                : message == ManageMessageId.RemovePhoneSuccess ? "Ваш номер телефона удален."
                : "";

            var userId = User.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = HasPassword(),
                PhoneNumber = await UserManager.GetPhoneNumberAsync(userId),
                TwoFactor = await UserManager.GetTwoFactorEnabledAsync(userId),
                Logins = await UserManager.GetLoginsAsync(userId),
                BrowserRemembered = await AuthenticationManager.TwoFactorBrowserRememberedAsync(userId)
            };
            return View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = await UserManager.RemoveLoginAsync(User.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public ActionResult AddPhoneNumber()
        {
            return View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Создание и отправка маркера
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.GetUserId(), model.Number);
            if (UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Ваш код безопасности: " + code
                };
                await UserManager.SmsService.SendAsync(message);
            }
            return RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EnableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.GetUserId(), true);
            var user = await UserManager.FindByIdAsync(User.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DisableTwoFactorAuthentication()
        {
            await UserManager.SetTwoFactorEnabledAsync(User.GetUserId(), false);
            var user = await UserManager.FindByIdAsync(User.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public async Task<ActionResult> VerifyPhoneNumber(string phoneNumber)
        {
            var code = await UserManager.GenerateChangePhoneNumberTokenAsync(User.GetUserId(), phoneNumber);
            // Отправка SMS через поставщик SMS для проверки номера телефона
            return phoneNumber == null ? View("Error") : View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePhoneNumberAsync(User.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // Это сообщение означает наличие ошибки; повторное отображение формы
            ModelState.AddModelError("", "Не удалось проверить телефон");
            return View(model);
        }

        //
        // GET: /Manage/RemovePhoneNumber
        public async Task<ActionResult> RemovePhoneNumber()
        {
            var result = await UserManager.SetPhoneNumberAsync(User.GetUserId(), null);
            if (!result.Succeeded)
            {
                return RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = await UserManager.FindByIdAsync(User.GetUserId());
            if (user != null)
            {
                await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            }
            return RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            ViewBag.Title = "Смена пароля.";
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET: /Manage/SetPassword
        public ActionResult SetPassword()
        {
            return View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SetPassword(SetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await UserManager.AddPasswordAsync(User.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = await UserManager.FindByIdAsync(User.GetUserId());
                    if (user != null)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                AddErrors(result);
            }

            // Это сообщение означает наличие ошибки; повторное отображение формы
            return View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public async Task<ActionResult> ManageLogins(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "Внешнее имя входа удалено."
                : message == ManageMessageId.Error ? "Произошла ошибка."
                : "";
            var user = await UserManager.FindByIdAsync(User.GetUserId());
            if (user == null)
            {
                return View("Error");
            }
            var userLogins = await UserManager.GetLoginsAsync(User.GetUserId());
            var otherLogins = AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Запрос перенаправления к внешнему поставщику входа для связывания имени входа текущего пользователя
            return new AccountController.ChallengeResult(provider, Url.Action("LinkLoginCallback", "Manage"), User.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(User.GetUserId(), loginInfo.Login);
            return result.Succeeded ? RedirectToAction("ManageLogins") : RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

#region Вспомогательные приложения
        // Используется для защиты от XSRF-атак при добавлении внешних имен входа
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            var user = UserManager.FindById(User.GetUserId());
            if (user != null)
            {
                return user.PhoneNumber != null;
            }
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion

    }
}