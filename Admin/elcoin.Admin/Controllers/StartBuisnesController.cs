using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using bbom.Admin.Core;
using bbom.Admin.Core.Extensions;
using bbom.Admin.Core.Filters;
using bbom.Admin.Core.Filters.Block;
using bbom.Admin.Core.Identity;
using bbom.Admin.Core.Services.EmailService;
using bbom.Admin.Core.ViewModels.StartBuisnes;
using bbom.Data.IdentityModel;
using bbom.Data.ModelPartials.Constants;
using bbom.Data.Repository.Interfaces;

namespace elcoin.Admin.Controllers
{
    [Authorize]
    [BlockNotRegistr]
    [BlockNotWatch]
    public class StartBuisnesController : Controller
    {
        private readonly IRepository<AspNetUser> _usersRepository;
        private ApplicationUserManager _userManager;
        private readonly IEmailService _emailService;

        public StartBuisnesController(IRepository<AspNetUser> usersRepository, ApplicationUserManager userManager,
            IEmailService emailService)
        {
            _usersRepository = usersRepository;
            _userManager = userManager;
            _emailService = emailService;
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager; }
            set { _userManager = value; }
        }

        // GET: VideoStep
        /// <summary>
        /// Страница с отображением видео о регистрации
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult VideoStep()
        {
            var user = _usersRepository.GetById(User.GetUserId());
            string link = "";
            string linkName = "";
            try
            {
                var parametr =
                    user.ReceivedExtraRegParams.FirstOrDefault()
                        .UserExtraRegParams.FirstOrDefault(param => param.AspNetUser == user.InvitedAspNetUser);
                linkName = parametr.ExtraRegParam.Name;
                link = parametr.Value;
            }
            catch 
            {
                //ignore
            }
            return View(new VideoStepViewModel {Link = link, LinkName = linkName});
        }

        // POST: VideoStep
        /// <summary>
        /// Перенаправление на 1 шаг
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VideoStep(VideoStepViewModel model)
        {
            return RedirectToAction("FirstStep");
        }

        // GET: FirstStep
        //Подтверждение емаил
        /// <summary>
        /// Страница с 1 шагом
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [StartBuisnesValidate]
        public ActionResult FirstStep()
        {
            return View();
        }

        // POST: FirstStep
        /// <summary>
        /// Изменяет почту пользователя
        /// </summary>
        /// <param name="fs"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> FirstStep(FirstStepViewModel fs)
        {
            if (ModelState.IsValid)
            {
                if (Request.Url != null)
                {
                    //_userManager = HttpContext.GetOwinContext().Get<ApplicationUserManager>();
                    var user = _usersRepository.GetById(User.GetUserId());
                    user.Email = fs.Email;
                    await _usersRepository.SaveChangesAsync();
                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(User.GetUserId());
                    CoreFasade.RegisterHelper.SendConfimMail(fs.Email,
                        Url.Action("ConfirmEmail", "Account",
                            new {userId = User.GetUserId(), code = code},
                            Request.Url.Scheme));
                    return RedirectToAction("SecondStep");
                }
            }
            return View(fs);
        }

        // GET: SecondStep
        // Смена пароля
        /// <summary>
        /// Переводит на форму смены пароля
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [StartBuisnesValidate]
        public ActionResult SecondStep()
        {
            var user = _usersRepository.GetById(User.GetUserId());
            if (user.EmailConfirmed)
            {
                return View();
            }
            var ss = new SecondStepViewModel();
            ModelState.AddModelError("",
                $"Email не подтвержден, для подтверждения вашей почты войдите на {user.Email} и пройдите по ссылке в  письме. Это нужно для дальнейшей работы с сайтом");
            return View(ss);
        }

        // GET: SecondStep
        /// <summary>
        /// Применяет изменения смены пароля
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> SecondStep(SecondStepViewModel model)
        {
            var user = _usersRepository.GetById(User.GetUserId());
            if (string.IsNullOrEmpty(user.Email))
            {
                return RedirectToAction("FirstStep");
            }
            if (!user.EmailConfirmed)
            {
                ModelState.AddModelError("",
                    $"Email не подтвержден, для подтверждения вашей почты войдите на {user.Email} и пройдите по ссылке в  письме. Это нужно для дальнейшей работы с сайтом");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result =
                await
                    UserManager.ChangePasswordAsync(User.GetUserId(),
                        User.GetUserName() + GlobalConstants.PasswordPostfix, model.NewPassword);
            if (result.Succeeded)
            {

                await
                    CoreFasade.UsersHelper.ActionUserRoleAsync(user.UserName, user.Id, UserRole.NotChangePassword,
                        HttpContext,
                        async (id, role) => await UserManager.RemoveFromRoleAsync(id, role));
                _emailService.SendMail(user.Email, "Смена пароля", $"Ваш новый пароль {model.NewPassword}\n" +
                                                                   $"Ваш логин {user.UserName}\n" +
                                                                   $"Сайт для входа {Constants.DomainUrl}", false);
                return RedirectToAction("TherdStep");
            }
            ModelState.AddModelError("", result.Errors.First());
            return View(model);
        }

        // GET: TherdStep
        // Оплата
        /// <summary>
        /// Перводит в контроллер оплаты
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [StartBuisnesValidate]
        public ActionResult TherdStep()
        {
            return RedirectToAction("Index", "Payment");
        }
    }
}