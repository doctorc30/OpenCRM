using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using bbom.Admin.Core;
using bbom.Admin.Core.Extensions;
using bbom.Admin.Core.Filters;
using bbom.Admin.Core.Notifications;
using bbom.Admin.Core.ViewModels.ExRegParams;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using bbom.Data.Repository.Interfaces;

namespace bbom.Admin.Controllers
{
    [HandleJsonError]
    public class ExRegParamsController : Controller
    {
        private readonly IRepository<AspNetUser> _usersRepository;
        private readonly IRepository<ExtraRegParam> _exRegParamsRepository;
        private readonly IRepository<UserExtraRegParam> _userExRegPatamsRepository;

        public ExRegParamsController(IRepository<AspNetUser> usersRepository, IRepository<ExtraRegParam>  exRegParamsRepository,
           IRepository<UserExtraRegParam> userExRegPatamsRepository)
        {
            _usersRepository = usersRepository;
            _exRegParamsRepository = exRegParamsRepository;
            _userExRegPatamsRepository = userExRegPatamsRepository;
        }

        // POST: Add
        public async Task<JsonResult> Add(ExRegParamJson data)
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var uerp = new UserExtraRegParam
            {
                Value = data.value,
                UserId = user.Id,
                ExtraRegParamId = Convert.ToInt32(data.objectId)
            };
            await _userExRegPatamsRepository.InsertAsync(uerp);
            return Json(Alert.Success);
        }

        // POST: Edit
        public async Task<JsonResult> Edit(ExRegParamJson data)
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var newParam = user.UserExtraRegParams.SingleOrDefault(uerp => uerp.ExtraRegParamId == data.objectId);
            if (newParam != null)
            {
                newParam.Value = data.value;
                await _userExRegPatamsRepository.SaveChangesAsync();
                return Json(Alert.Success);
            }
            var oldParam = user.UserExtraRegParams.SingleOrDefault(u => u.ExtraRegParamId == data.id);
            var uerpEdit = new UserExtraRegParam
            {
                Value = data.value,
                UserId = user.Id,
                ExtraRegParamId = Convert.ToInt32(data.objectId)
            };
            await _userExRegPatamsRepository.InsertAsync(uerpEdit);
            await _userExRegPatamsRepository.DeleteAsync(oldParam);
            return Json(Alert.Success);
        }

        [HttpGet]
        public ActionResult GetExRegParamsArray()
        {
            var exRegParams = _usersRepository.GetById(User.GetUserId()).UserExtraRegParams;
            //todo преобразование в масив
            var data = exRegParams.Select(uc => new List<string>
            {
                uc.ExtraRegParamId.ToString(),
                uc.ExtraRegParam.Name,
                uc.Value
            }).Select(dataObject => dataObject.ToArray()).Cast<object>().ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetUserNotAddedExRegParams()
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var exRegParams = CoreFasade.UsersHelper.GetNotAddedUserExRegParams(user);
            var data = exRegParams.Select(uc => new ExRegParamSelect2Json
            {
                id = uc.Id.ToString(),
                text = uc.Name
            });
            return Json(data.ToArray(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllExRegParams()
        {
            var exRegParams = _exRegParamsRepository.GetAll();
            var data = exRegParams.Select(c => new ExRegParamSelect2Json
            {
                id = c.Id.ToString(),
                text = c.Name
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}