using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using bbom.Admin.Core;
using bbom.Admin.Core.DataExtensions;
using bbom.Admin.Core.DataExtensions.Helpers.Impl;
using bbom.Admin.Core.Extensions;
using bbom.Data;
using bbom.Data.IdentityModel;
using bbom.Data.ModelPartials.Constants;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Models;
using IdentityServer3.Core.Services;
using IdentitySolomon.App_Packages.IdentityServer3.AspNetIdentity;
using IdentitySolomon.AspId;

namespace IdentitySolomon.IdSvr
{
	public static class UserServiceExtensions
	{
		public static void ConfigureUserService(this IdentityServerServiceFactory factory, string connString)
		{
			factory.UserService = new Registration<IUserService, UserService>();
			factory.Register(new Registration<UserManager>());
			factory.Register(new Registration<UserStore>());
			factory.Register(new Registration<Context>(resolver => new Context(connString)));
		}
	}
	
	public class UserService : AspNetIdentityUserService<User, string>
	{
		public UserService(UserManager userMgr)
			: base(userMgr)
		{
		}

		protected override async Task<User> FindUserAsync(string userLogin)
		{
			var emailAttr = new EmailAddressAttribute();
			if (emailAttr.IsValid(userLogin))
				return await userManager.FindByEmailAsync(userLogin);
			return await userManager.FindByNameAsync(userLogin);
		}

		public override Task PreAuthenticateAsync(PreAuthenticationContext context)
		{
			var url = new Uri(context.SignInMessage.ReturnUrl);
			var param = HttpUtility.ParseQueryString(url.Query);
			var userName = param["user"];
			if (!string.IsNullOrEmpty(userName))
			{
				var user = DataFasade.GetRepository<AspNetUser>().GetAll().SingleOrDefault(user1 => userName == user1.UserName);
				if (user != null && user.AspNetRoles.Any(role => role.Name == UserRole.NotUser))
				{
					context.AuthenticateResult = new AuthenticateResult(user.Id, userName);
				}
			}
			return base.PreAuthenticateAsync(context);
		}

		public override Task PostAuthenticateAsync(PostAuthenticationContext context)
		{
			var userId = context.AuthenticateResult.User.GetUserId();
			var repo = DataFasade.GetRepository<AspNetUser>();
			var user = repo.GetById(userId);
			//todo хак для сохранения в бд, пока непонятно почему обьект не создается через ядро ninject
			new ClaimsHelper().SetSingleClaim(user, new Claim
			{
				Value = DateTime.Now.ToString(CultureInfo.InvariantCulture),
				ValueType = ClaimType.LastLogin
			});
			repo.SaveChanges();
			return base.PostAuthenticateAsync(context);
		}

		protected override async Task<IEnumerable<System.Security.Claims.Claim>> GetClaimsFromAccount(User user) {
			var claims = (await base.GetClaimsFromAccount(user)).ToList();
			if (!String.IsNullOrWhiteSpace(user.Name)) {
				claims.Add(new System.Security.Claims.Claim("given_name", user.Name));
			}
			if (!String.IsNullOrWhiteSpace(user.Suname)) {
				claims.Add(new System.Security.Claims.Claim("family_name", user.Suname));
			}
			if (!String.IsNullOrWhiteSpace(user.Altname))
			{
				claims.Add(new System.Security.Claims.Claim("father_name", user.Altname));
			}
			return claims;
		}
	}
}
