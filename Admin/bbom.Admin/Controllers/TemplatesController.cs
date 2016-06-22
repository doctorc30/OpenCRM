using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using bbom.Admin.Core;
using bbom.Admin.Core.Extensions;
using bbom.Admin.Core.Notifications;
using bbom.Admin.Core.ViewModels.Templates;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using bbom.Data.ModelPartials.Constants;
using bbom.Data.Repository.Interfaces;

namespace bbom.Admin.Controllers
{
    [Authorize]
    public class TemplatesController : Controller
    {
        private readonly IRepository<AspNetUser> _usersRepository;
        private readonly IRepository<Template> _templatesRepository;

        public TemplatesController(IRepository<AspNetUser> usersRepository, IRepository<Template> templatesRepository)
        {
            _usersRepository = usersRepository;
            _templatesRepository = templatesRepository;
        }

        // GET: GetTemplates
        public ActionResult GetTemplates()
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var data = CoreFasade.TemplateHelper.GetTemplatesJson(user);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        //[BlockNotWatch]
        //[BlockLowRoles]
        //public ActionResult Site()
        //{
        //    //todo мапить настройки когда их станет много
        //    var user = _usersRepository.GetById(User.GetUserId());
        //    return
        //        View(new TemplateSettings
        //        {
        //            templateId = user.ActiveTemplate.Id.ToString(),
        //            userVideoLink =
        //                CoreFasade.TemplateHelper.GetTemplateSetting(user.ActiveTemplate, user,
        //                    Data.ModelPartials.SettingEx.VideoLink).Value
        //        });
        //}

        [HttpPost]
        public async Task<JsonResult> SaveActiveTemplate([Bind(Include = "templateId,userVideoLink")]TemplateSettings settings)
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var template = _templatesRepository.GetById(settings.templateId);
            CoreFasade.UsersHelper.SetUserActiveTemplate(user, template.Id);
            CoreFasade.TemplateHelper.SetTemplateSetting(template, user, SettingType.VideoLink, settings.userVideoLink);
            int r = await _usersRepository.SaveChangesAsync();
            if (r == 1)
            {
                return Json(Alert.Success);
            }
            return Json(Alert.Error);
        }

        //todo возможно стоит перенести в home
        [AllowAnonymous]
        public JsonResult GetUserVideoLink()
        {
            var user = _usersRepository.GetById(User.GetUserId());
            if (user == null)
            {
                var userName = RouteData.Values["subdomain"];
                user = _usersRepository.GetAll().Single(netUser => netUser.UserName == (string) userName);
                if (user == null)
                    return Json("", JsonRequestBehavior.AllowGet);
            }
            var videoLink =
                CoreFasade.TemplateHelper.GetTemplateSetting(CoreFasade.UsersHelper.GetUserActiveTemplate(user), user,
                    SettingType.VideoLink);
            return Json(videoLink.Value, JsonRequestBehavior.AllowGet);
        }
    }
}