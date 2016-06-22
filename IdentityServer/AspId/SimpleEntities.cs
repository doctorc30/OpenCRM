using System;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using IdSvr3 = IdentityServer3.Core;

namespace IdentitySolomon.AspId
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Suname { get; set; }
        public string Altname { get; set; }
    }

    public class Role : IdentityRole { }

    public class Context : IdentityDbContext<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public Context(string connString)
            : base(connString)
        {
        }
    }

    public class UserStore : UserStore<User, Role, string, IdentityUserLogin, IdentityUserRole, IdentityUserClaim>
    {
        public UserStore(Context ctx)
            : base(ctx)
        {
        }
    }

    public class UserManager : UserManager<User, string>
    {
        public UserManager(UserStore store)
            : base(store)
        {
            this.ClaimsIdentityFactory = new ClaimsFactory();
        }
    }

    public class ClaimsFactory : ClaimsIdentityFactory<User, string>
    {
        public ClaimsFactory()
        {
            this.UserIdClaimType = IdSvr3.Constants.ClaimTypes.Subject;
            this.UserNameClaimType = IdSvr3.Constants.ClaimTypes.PreferredUserName;
            this.RoleClaimType = IdSvr3.Constants.ClaimTypes.Role;
        }

        public override async System.Threading.Tasks.Task<ClaimsIdentity> CreateAsync(UserManager<User, string> manager, User user, string authenticationType)
        {
            var ci = await base.CreateAsync(manager, user, authenticationType);
            if (!String.IsNullOrWhiteSpace(user.Name))
            {
                ci.AddClaim(new Claim("given_name", user.Name));
            }
            if (!String.IsNullOrWhiteSpace(user.Suname))
            {
                ci.AddClaim(new Claim("family_name", user.Suname));
            }
            if (!String.IsNullOrWhiteSpace(user.Altname))
            {
                ci.AddClaim(new Claim("father_name", user.Altname));
            }
            return ci;
        }
    }
    
    public class RoleStore : RoleStore<Role>
    {
        public RoleStore(Context ctx)
            : base(ctx)
        {
        }
    }

    public class RoleManager : RoleManager<Role>
    {
        public RoleManager(RoleStore store)
            : base(store)
        {
        }
    }


}