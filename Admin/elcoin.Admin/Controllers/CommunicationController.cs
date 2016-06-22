using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using bbom.Admin.Core;
using bbom.Admin.Core.Extensions;
using bbom.Admin.Core.Filters;
using bbom.Admin.Core.Notifications;
using bbom.Admin.Core.ViewModels.Communications;
using bbom.Data.IdentityModel;
using bbom.Data.Repository.Interfaces;

namespace elcoin.Admin.Controllers
{
    [HandleJsonError]
    public class CommunicationController : Controller
    {
        private readonly IRepository<AspNetUser> _usersRepository;
        private readonly IRepository<Communicatio> _comRepository;

        public CommunicationController(IRepository<AspNetUser> usersRepository, IRepository<Communicatio>  comRepository)
        {
            _usersRepository = usersRepository;
            _comRepository = comRepository;
        }

        // POST: Add
        public ActionResult Add(CommunicationJson data)
        {
            var user = _usersRepository.GetById(User.GetUserId());
            user.UserCommunications.Add(new UserCommunication
            {
                Value = data.value,
                AspNetUser = user,
                CommunicationId = Convert.ToInt32(data.objectId)
            });
            _usersRepository.SaveChanges();
            return Json(Alert.Success);
        }

        // POST: Edit
        public async Task<JsonResult> Edit(CommunicationJson data)
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var newParam = user.UserCommunications.SingleOrDefault(u => u.CommunicationId == data.objectId);
            if (newParam != null)
            {
                newParam.Value = data.value;
                var res = await _usersRepository.SaveChangesAsync();
                return Json(Alert.Success);
            }
            var oldParam = user.UserCommunications.SingleOrDefault(u => u.CommunicationId == data.id);
            user.UserCommunications.Add(new UserCommunication
            {
                Value = data.value,
                AspNetUser = user,
                CommunicationId = Convert.ToInt32(data.objectId)
            });
            user.UserCommunications.Remove(oldParam);
            var res2 = await _usersRepository.SaveChangesAsync();
            return Json(Alert.Success);
        }

        // POST: Delete
        public async Task<JsonResult> Delete(CommunicationJson data)
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var param = user.UserCommunications.SingleOrDefault(communication => communication.CommunicationId == data.objectId);
            if (param == null)
                return Json(Alert.ShowError("Не найдено"));
            user.UserCommunications.Remove(param);
            await _usersRepository.SaveChangesAsync();
            return Json(Alert.Success);
        }

        [HttpGet]
        public ActionResult GetCommunicationsArray()
        {
            var coomunications = _usersRepository.GetById(User.GetUserId()).UserCommunications;
            //todo преобразование в масив
            var data = coomunications.Select(uc => new List<string>
            {
                uc.CommunicationId.ToString(),
                uc.Communicatio.Name,
                uc.Value
            }).Select(dataObject => dataObject.ToArray()).Cast<object>().ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetUserNotAddedCommunications()
        {
            var user = _usersRepository.GetById(User.GetUserId());
            var coomunications = CoreFasade.UsersHelper.GetNotAddedUserCommunications(user);
            var data = coomunications.Select(uc => new CommunicationsSelect2Json
            {
                id = uc.Id.ToString(),
                text = uc.Name
            });
            return Json(data.ToArray(), JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetAllCommunications()
        {
            var coomunications = _comRepository.GetAll();
            var data = coomunications.Select(c => new CommunicationsSelect2Json
            {
                id = c.Id.ToString(),
                text = c.Name
            });
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}