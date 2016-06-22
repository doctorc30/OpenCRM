using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using bbom.Admin.Core;
using bbom.Admin.Core.Extensions;
using bbom.Admin.Core.Identity;
using bbom.Admin.Core.Notifications;
using bbom.Admin.Core.Services.EmailService;
using bbom.Admin.Core.ViewModels.Common;
using bbom.Admin.Core.ViewModels.Registrator;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using bbom.Data.ModelPartials.Constants;
using bbom.Data.Repository.Interfaces;
using Microsoft.AspNet.Identity;

namespace bbom.Admin.Controllers
{
    [Authorize]
    public class RegistratorsController : Controller
    {
        private readonly IRepository<Registrator> _regsRepository;
        private readonly IRepository<AspNetUser> _usersRepository;
        private ApplicationUserManager _userManager;
        private readonly IEmailService _emailService;
        private readonly IRepository<Setting> _settingsRepository;

        public RegistratorsController(IRepository<Registrator> regsRepository, IRepository<AspNetUser> usersRepository,
            ApplicationUserManager userManager, IEmailService emailService, IRepository<Setting> settingsRepository)
        {
            _regsRepository = regsRepository;
            _usersRepository = usersRepository;
            _userManager = userManager;
            _emailService = emailService;
            _settingsRepository = settingsRepository;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetAllByUser()
        {
            var regs = _regsRepository.GetAll().ToList().Where(registrator => registrator.UserId == User.GetUserId());
            var data = regs.Select(r => new List<string>
            {
                r.Id.ToString(),
                r.Name
            }).Select(dataObject => dataObject.ToArray()).Cast<object>().ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register(int id)
        {
            var reg = _regsRepository.GetById(id);
            var model = new HelloViewModel
            {
                VideoLink = reg.RegistratorSettings.SingleOrDefault(rs => rs.Setting.Name == SettingType.VideoLink).Value,
                Background = reg.RegistratorSettings.SingleOrDefault(rs => rs.Setting.Name == SettingType.Background).Value,
                InvitedUserName = _usersRepository.GetById(reg.UserId).GetIO(),
                PostAction = "Register",
                PostController = "Registrators"
            };
            Session["regId"] = id;
            return View("Hello", model);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Register()
        {
            var id = (int)Session["regId"];
            var registrator = _regsRepository.GetById(id);
            var parentUser = _usersRepository.GetById(registrator.UserId);
            var model = new RegistratorViewModel
            {
                ReferalUserName = parentUser.GetIO(),
                Background = registrator.RegistratorSettings.SingleOrDefault(rs => rs.Setting.Name == SettingType.Background)?.Value
            };
            return View("Registration", model);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Registration(RegistratorViewModel model)
        {
            var id = (int)Session["regId"];
            var registrator = _regsRepository.GetById(id);
            var parentUser = _usersRepository.GetById(registrator.UserId);
            //SetViewBagPersonalPage(parentUser);
            if (ModelState.IsValid)
            {
                model.UserName = CoreFasade.RegisterHelper.GetNewUserName(model.Name);
                var applicationUser = new ApplicationUser
                {
                    UserName = model.UserName
                };
                var result = await _userManager.CreateAsync(applicationUser, model.UserName + GlobalConstants.PasswordPostfix);
                if (result.Succeeded)
                {
                    await CoreFasade.RegisterHelper.ExRegisterParamsAsync(model, applicationUser.Id,
                        GlobalConstants.DefaultTemplate, parentUser,
                        new[] { UserRole.NotUser, UserRole.NotWatch, UserRole.NotChangePassword, UserRole.NotRegister },
                        new string[] { }, HttpContext);
                    return RedirectToAction("Confirm", "Registrators", new { id = model.UserName });
                }
                AddErrors(result);
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.VideoUrls =
                _settingsRepository.GetAll()
                    .Single(s => s.Name == SettingType.VideoLink)
                    .DefaultSettingsValues.Select(value => new SelectListItem
                    {
                        Text = value.Description,
                        Value = value.Value
                    });
            return View();
        }

        [HttpGet]
        public ActionResult Confirm()
        {
            return View();
        }

        [HttpPost]
        public async Task<ViewResult> Create(CreateRegistratorViewModel model)
        {
            var reg = new Registrator
            {
                Name = model.Name,
                UserId = User.GetUserId()
            };
            reg.RegistratorSettings.Add(new RegistratorSetting
            {
                SettingId = _settingsRepository.GetAll()
                    .Single(s => s.Name == SettingType.VideoLink).Id,
                Value = model.VideoUrl
            });
            reg.RegistratorSettings.Add(new RegistratorSetting
            {
                SettingId = _settingsRepository.GetAll()
                    .Single(s => s.Name == SettingType.Background).Id,
                Value = model.imagePicker
            });
            await _regsRepository.InsertAsync(reg);
            return View("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var reg = _regsRepository.GetById(id);
            if (reg == null)
            {
                ModelState.AddModelError("", "Регистратор не найден.");
                return View();
            }
            ViewBag.VideoUrls =
                _settingsRepository.GetAll()
                    .Single(s => s.Name == SettingType.VideoLink)
                    .DefaultSettingsValues.Select(value => new SelectListItem
                    {
                        Text = value.Description,
                        Value = value.Value
                    });
            return View(new EditRegistratorViewModel
            {
                Id = id,
                Name = reg.Name,
                VideoUrl = reg.RegistratorSettings.SingleOrDefault(rs => rs.Setting.Name == SettingType.VideoLink)?.Value,
                imagePicker = reg.RegistratorSettings.SingleOrDefault(rs => rs.Setting.Name == SettingType.Background)?.Value
            });
        }

        [HttpPost]
        public async Task<ViewResult> Edit(EditRegistratorViewModel model)
        {
            var reg = _regsRepository.GetById(model.Id);
            reg.Name = model.Name;
            var setting = _settingsRepository.GetAll().SingleOrDefault(s => s.Name == SettingType.VideoLink);
            if (setting == null)
            {
                ModelState.AddModelError("", $"Настройка {SettingType.VideoLink} не найдена.");
                return View();
            }
            var rs = reg.RegistratorSettings.SingleOrDefault(s => s.SettingId == setting.Id);
            if (rs == null)
            {
                ModelState.AddModelError("", $"Настройка регистратора {SettingType.VideoLink} не найдена.");
                return View();
            }
            rs.Value = model.VideoUrl;
            await _regsRepository.EditAsync(reg);
            return View("Index");
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            var reg = _regsRepository.GetById(id);
            await _regsRepository.DeleteAsync(reg);
            return Json(Alert.Success);
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