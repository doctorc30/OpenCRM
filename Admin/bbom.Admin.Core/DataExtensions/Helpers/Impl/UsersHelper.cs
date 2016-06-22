using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using bbom.Admin.Core.DataExtensions.Helpers.Interfaces;
using bbom.Admin.Core.Extensions;
using bbom.Admin.Core.Services.AccessService;
using bbom.Data;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using bbom.Data.ModelPartials.Constants;
using IdentityModel;
using IdentityModel.Client;
using Microsoft.AspNet.Identity;
using Ninject.Activation;

namespace bbom.Admin.Core.DataExtensions.Helpers.Impl
{
    public class UsersHelper : IUsresHelper
    {
        private readonly IAccessService _accessService;

        public delegate Task<IdentityResult> RoleActionCallBack(string userId, string role);
        public delegate Task<IdentityResult> RolesActionCallBack(string userId, string[] roles);

        public UsersHelper(IAccessService accessService)
        {
            _accessService = accessService;
        }

        /// <summary>
        /// Возвращает всех потомков пользователя (рекурсивно)
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ICollection<AspNetUser> GetAllChildren(AspNetUser user)
        {
            try
            {
                var result = new List<AspNetUser>();
                result.AddRange(user.AspNetUsers1);
                foreach (var userChildren in user.AspNetUsers1)
                {
                    result.AddRange(GetAllChildren(userChildren));
                }
                return result;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Возвращает всех потомков по определенной линии
        /// </summary>
        /// <param name="user"></param>
        /// <param name="line"></param>
        /// <returns></returns>
        public ICollection<AspNetUser> GetAllChildren(AspNetUser user, int line)
        {
            var result = GetFirstLine(user);
            line--;
            while (line != 0)
            {
                var result2 = new List<AspNetUser>();
                foreach (var children in result)
                {
                    result2.AddRange(GetFirstLine(children));
                }
                result = result2;
                line--;
            }
            return result;
        }

        /// <summary>
        /// Возвращает первую линию
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ICollection<AspNetUser> GetFirstLine(AspNetUser user)
        {
            var childrens = GetAllChildren(user);
            return childrens.Where(children => children.InvitedAspNetUser == user).ToList();
            
        }

        /// <summary>
        /// Возвращает последнего пользователя по определенной стороне дерева
        /// </summary>
        /// <param name="user"></param>
        /// <param name="foot">лево или право (0/1)</param>
        /// <returns></returns>
        public AspNetUser GetLastUserByFoot(AspNetUser user, int foot)
        {
            AspNetUser children = null;
            foreach (var childrenUser in user.ChildrenAspNetUsers)
            {
                if (childrenUser.Foot == foot)
                    children = childrenUser;
            }
            if (children != null)
            {
                children = GetLastUserByFoot(children, foot);
            }
            else
            {
                return user;
            }
            return children;
        }


        public AspNetUser GetUser(string userName)
        {
            var httpContext = HttpContext.Current;
            if (httpContext.User.Identity.IsAuthenticated)
            {
                return DataFasade.GetRepository<AspNetUser>().GetById(httpContext.User.GetUserId());
            }
            if (userName != null)
            {
                return DataFasade.GetUserByName(userName);
            }
            return null;
        }

        /// <summary>
        /// Возвращает роли пользователя (кеш)
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        public IEnumerable<string> GetUserRoles(string userName)
        {
            return UserCache.GetRoles(userName);
        }

        public void ClearUserCache(string userName, HttpContextBase httpContext)
        {
            UserCache.Clear(userName);
        }

        public async Task UpdateCookie()
        {
            await Task.FromResult(0);
        }

        /// <summary>
        /// Возвращает недобавленные связи пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ICollection<Communicatio> GetNotAddedUserCommunications(AspNetUser user)
        {
            var com = DataFasade.GetRepository<Communicatio>().GetAll();
            var userComs = user.UserCommunications.Select(uc => uc.Communicatio);
            var allowComs = new List<Communicatio>();
            foreach (var communicatio in com)
            {
                if (!userComs.Contains(communicatio))
                {
                    allowComs.Add(communicatio);
                }

            }
            return allowComs;
        }

        /// <summary>
        /// Возвращает недобавленные расширенные настройки пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ICollection<ExtraRegParam> GetNotAddedUserExRegParams(AspNetUser user)
        {
            var exParams = DataFasade.GetRepository<ExtraRegParam>().GetAll();
            var userComs = user.UserExtraRegParams.Select(uc => uc.ExtraRegParam);
            var allowComs = new List<ExtraRegParam>();
            foreach (var exParam in exParams)
            {
                if (!userComs.Contains(exParam))
                {
                    allowComs.Add(exParam);
                }

            }
            return allowComs;
        }

        public Task<IdentityResult> ActionUserRoleAsync(string userName, string userId, string role, HttpContextBase httpContext, RoleActionCallBack roleActionCallBack)
        {
            var task = roleActionCallBack(userId, role);
            ClearUserCache(userName, httpContext);
            return task;
        }

        public Task<IdentityResult> ActionUserRolesAsync(string userName, string userId, string[] roles, HttpContextBase httpContext, RolesActionCallBack roleActionCallBack)
        {
            var task = roleActionCallBack(userId, roles);
            ClearUserCache(userName, httpContext);
            return task;
        }

        /// <summary>
        /// Возвращает все разрещенные планы оплаты
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ICollection<PaymentPlan> GetAllAlowPaymentPlans(AspNetUser user)
        {
            var plans =
                _accessService.GetUserAlowPaymentPlans(user)
                    .Where(plan => plan.Status != null && plan.Status == PaymentPlanStatus.Active)
                    .ToList();
            return plans;
        }

        public Template GetUserActiveTemplate(AspNetUser user)
        {
            var templId = CoreFasade.ClaimsHelper.GetSingleClaim(user, ClaimType.ActiveTemplate).Value;
            if (templId != null)
            {
                int tmId = Convert.ToInt32(templId);
                return DataFasade.GetRepository<Template>().GetById(tmId);
            }
            throw new Exception("шаблон не найден");
        }

        public void SetUserActiveTemplate(AspNetUser user, int templateId)
        {
            var claim = new Claim
            {
                Value = templateId.ToString(),
                ValueType = ClaimType.ActiveTemplate
            };
            CoreFasade.ClaimsHelper.SetSingleClaim(user, claim);
        }
    }
}