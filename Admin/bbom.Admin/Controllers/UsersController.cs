using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using bbom.Admin.Core;
using bbom.Admin.Core.DataExtensions.Helpers;
using bbom.Admin.Core.Extensions;
using bbom.Admin.Core.Filters.Block;
using bbom.Admin.Core.Identity;
using bbom.Admin.Core.Notifications;
using bbom.Admin.Core.ViewModels.Communications;
using bbom.Admin.Core.ViewModels.Users;
using bbom.Data;
using bbom.Data.IdentityModel;
using bbom.Data.ModelPartials.Constants;
using bbom.Data.Repository.Interfaces;
using Microsoft.AspNet.Identity;

namespace bbom.Admin.Controllers
{
    //[HandleJsonError]
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IRepository<AspNetUser> _usersRepository;
        private ApplicationUserManager _userManager;

        public UsersController(IRepository<AspNetUser> usersRepository, ApplicationUserManager userManager)
        {
            _usersRepository = usersRepository;
            _userManager = userManager;
        }

        // GET: MoveUser
        [HttpPost]
        [OnlyAdminAuthorize]
        public ActionResult MoveUser(string targetName, string toName)
        {
            try
            {
                var user = _usersRepository.GetById(_userManager.FindByName(targetName).Id);
                var toUserId = _userManager.FindByName(toName).Id;
                user.InvitedUser = toUserId;
                _usersRepository.SaveChangesAsync();
                return Json(Alert.Success);
            }
            catch (Exception e)
            {
                return Json(Alert.ShowError(e.Message));
            }
        }

        // GET: GetUsersJson
        public JsonResult GetNewUsersJson()
        {
            var findUser = _usersRepository.GetById(User.GetUserId());
            var treeUsers =
                findUser.AspNetUsers1.Where(
                    userChild => userChild.Foot == null && UserFunctionHelper.IsSystemUser(userChild));
            var data = treeUsers.Select(user => new List<string>
            {
                user.GetFullName(),
                user.Email
            }).Select(dataObject => dataObject.ToArray()).Cast<object>().ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: GetUserLineJson
        public JsonResult GetUserLineJson(int line, string userName, DateTime? dateFrom, DateTime? dateTo)
        {
            var findUser =
                userName == null
                    ? _usersRepository.GetById(User.GetUserId())
                    : _usersRepository.GetAll().Single(netUser => netUser.UserName == userName);
            var users = CoreFasade.UsersHelper.GetAllChildren(findUser, line);
            if (dateFrom != null && dateTo != null)
            {
                users =
                    users.Where(
                        user =>
                            user.DateRegistration != null &&
                            user.DateRegistration.Value.IsBeetwen(dateFrom.Value, dateTo.Value)).ToList();
            }
            var data = users.Select(user => new List<string>
            {
                user.UserName,
                user.GetFullName(),
                user.Email,
                user.AspNetRoles.Any(role => role.Name == UserRole.NotRegister) ? "нет" : "да",
                user.AspNetRoles.Any(role => role.Name == UserRole.NotWatch) ? "нет" : "да",
                user.AspNetRoles.All(role => role.Name != UserRole.User || role.Name == UserRole.NotPay) ? "нет" : "да",
                user.AspNetRoles.All(role => role.Name != UserRole.PayFirm) ? "нет" : "да",
                user.DateRegistration.Value.ToString("g")
            }).Select(dataObject => dataObject.ToArray()).Cast<object>().ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        // GET: GetTreeUsersJson;
        public JsonResult GetTreeUsersJson(int line, string userName)
        {
            var findUser =
                string.IsNullOrEmpty(userName)
                ? _usersRepository.GetById(User.GetUserId())
                : _usersRepository.GetAll().Single(netUser => netUser.UserName == userName);
            var json = line == 0 ? CoreFasade.TreeUsers.GetTreeUsersJson(findUser) : CoreFasade.TreeUsers.GetTreeUsersWithLineJson(findUser, line);
            return Json(json, JsonRequestBehavior.AllowGet);
        }

        // GET: GetUserInfoJson;
        [AllowAnonymous]
        public JsonResult GetUserInfoJson(string userName)
        {
            //todo у "www" нету родительского пользователя "user.InvitedAspNetUser.UserName" упадет nullPointer
            var user = _usersRepository.GetAll().Single(netUser => netUser.UserName == userName);
            var userJson = new User(user.UserName, user.Email, user.InvitedAspNetUser.UserName)
            {
                firstLineCount = CoreFasade.UsersHelper.GetAllChildren(user, 1).ToString(),
                allChildrensCount = CoreFasade.UsersHelper.GetAllChildren(user).ToString(),
                itCanBeUpgraded =
                    user.AspNetRoles.Any(role => role.Name == UserRole.User && role.Name != UserRole.NotPay).ToString(),
                itNotWatched = user.AspNetRoles.Any(role => role.Name == UserRole.NotWatch).ToString(),
                email = user.Email,
                fio = user.GetIO(),
                lastLogin = CoreFasade.ClaimsHelper.GetSingleClaim(user, ClaimType.LastLogin).Value,
                confimInvite = CoreFasade.ClaimsHelper.GetSingleClaim(user, ClaimType.ConfimInviteDate).Value,
                communications =
                    user.UserCommunications.Select(
                        uc => new CommunicationJson {name = uc.Communicatio.Name, value = uc.Value}).ToList(),
                isUser = user.AspNetRoles.All(role => role.Name != UserRole.User || role.Name == UserRole.NotPay) ? "нет" : "да",
                isWatch = user.AspNetRoles.Any(role => role.Name == UserRole.NotWatch) ? "нет" : "да",
            };
            return Json(userJson, JsonRequestBehavior.AllowGet);
        }

        // POST: UpgradeRole;
        [HttpPost]
        [OnlyAdminAuthorize]
        public async Task<JsonResult> UpgradeRole(string userName)
        {
            try
            {
                var user = _usersRepository.GetAll().Single(netUser => netUser.UserName == userName);
                await CoreFasade.UsersHelper.ActionUserRoleAsync(user.UserName, user.Id,
                    UserRole.User,
                    HttpContext, async (id, role) => await UserManager.AddToRoleAsync(id, role));
                await CoreFasade.UsersHelper.ActionUserRoleAsync(user.UserName, user.Id,
                    UserRole.NotUser,
                    HttpContext, async (id, role) => await UserManager.RemoveFromRoleAsync(id, role));
                return Json(Alert.Success);
            }
            catch (Exception e)
            {
                return Json(Alert.ShowError(e.Message));
            }
        }

        // POST: UpgradeRole;
        [HttpPost]
        [OnlyHightRoles]
        public async Task<JsonResult> UpgradeRoleFirm(string userName)
        {
            try
            {
                var user = _usersRepository.GetAll().Single(netUser => netUser.UserName == userName);
                await CoreFasade.UsersHelper.ActionUserRoleAsync(user.UserName, user.Id,
                    UserRole.PayFirm,
                    HttpContext, async (id, role) => await UserManager.AddToRoleAsync(id, role));
                //await CoreFasade.UsersHelper.ActionUserRoleAsync(user.UserName, user.Id,
                //    UserRole.User,
                //    HttpContext, async (id, role) => await UserManager.AddToRoleAsync(id, role));
                await CoreFasade.UsersHelper.ActionUserRoleAsync(user.UserName, user.Id,
                    UserRole.NotUser,
                    HttpContext, async (id, role) => await UserManager.RemoveFromRoleAsync(id, role));
                return Json(Alert.Success);
            }
            catch (Exception e)
            {
                return Json(Alert.ShowError(e.Message));
            }
        }

        // POST: UpgradeRole;
        [HttpPost]
        public async Task<JsonResult> SetWatchRole(string userName)
        {
            try
            {
                var user = _usersRepository.GetAll().Single(netUser => netUser.UserName == userName);
                await CoreFasade.UsersHelper.ActionUserRoleAsync(user.UserName, user.Id, UserRole.NotWatch, HttpContext,
                    (id, role) => UserManager.RemoveFromRoleAsync(id, role));
                return Json(Alert.Success);
            }
            catch (Exception e)
            {
                return Json(Alert.ShowError(e.Message));
            }
        }

        // POST: SetUsersFoot
        [HttpPost]
        public JsonResult SetUsersFoot(UsersJson json)
        {
            var userFoots = json.users;
            var currentUser = _usersRepository.GetById(User.GetUserId());
            var users = _usersRepository.GetAll();
            try
            {
                foreach (var userFoot in userFoots)
                {
                    if (userFoot.foot == null)
                        continue;
                    int foot = int.Parse(userFoot.foot);
                    var user = users.Single(netUser => netUser.UserName == userFoot.userName);
                    var lastUser = CoreFasade.UsersHelper.GetLastUserByFoot(currentUser, foot);
                    if (lastUser.Equals(currentUser))
                        user.Foot = foot;
                    else
                    {
                        user.parent_id = lastUser.Id;
                        user.Foot = foot;
                    }
                }
                _usersRepository.SaveChanges();
            }
            catch (Exception e)
            {
                return Json(Alert.ShowError(e.Message));
            }
            return Json(Alert.Success);
        }

        [HttpGet]
        [OnlyAdminAuthorize]
        public ActionResult UserMenager(string id)
        {
            if (string.IsNullOrEmpty(id))
                return View();
            var roles = DataFasade.GetRepository<AspNetRole>().GetAll();
            var user = DataFasade.GetUserByName(id);
            var um = new UserMenagerViewModel
            {
                AvailableRoles = roles.ToList(),
                User = user,
                SelectedRoles = user.AspNetRoles.ToList()
            };
            ViewBag.Dis = user.ReceiveDiscounts;
            ViewBag.Payments = user.Payments;
            ViewBag.Events = user.CreateEvents;
            ViewBag.Spiker = user.SpikEvents;
            return View(um);
        }

        [HttpGet]
        [OnlyAdminAuthorize]
        public ActionResult AllUsers()
        {
            var users = _usersRepository.GetAll();
            return View(users);
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
    }
}