using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;
using bbom.Admin.Core.Identity;
using bbom.Admin.Test.Mock.Controller;
using bbom.Admin.Test.Mock.Repository.Entrity;
using bbom.Data.ContentModel;
using bbom.Data.IdentityModel;
using bbom.Data.Repository.Interfaces;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Moq;
using Ninject;
using Solomon.Test.Mock.Repository;

namespace bbom.Admin.Test.Controllers
{
    public class UnitTestControllerHelper
    {
        public enum UserRole
        {
            Admin, User, NotUser, Non
        }
        public struct User
        {
            public string Name { get; set; }     
            public string Id { get; set; }
        }

        private static UnitTestControllerHelper _unitTestControllerHelper;
        public static UnitTestControllerHelper GetInstance()
        {
            if (_unitTestControllerHelper == null)
            {
                _unitTestControllerHelper = new UnitTestControllerHelper();
                return _unitTestControllerHelper;
            }
            else
            {
                return _unitTestControllerHelper;
            }
        }

        private static Dictionary<UserRole, User> _users = new Dictionary<UserRole, User>
        {
            {
                UserRole.Admin, new User
                {
                    Id = "f4bef59a-0ba8-4c22-8094-a0221ad7d7df",
                    Name = "www"
                }
            },
            {
                UserRole.User, new User
                {
                    Id = "11318fcb-e375-4fdb-a4ed-8ba24e3e3279",
                    Name = "qwe8"
                }
            },
            {
                UserRole.NotUser, new User
                {
                    Id = "",
                    Name = ""
                }
            },
            {
                UserRole.Non, new User
                {
                    Id = "",
                    Name = ""
                }
            }
        };

        public static Dictionary<UserRole, User> Users => _users;

        public UnitTestControllerHelper()
        {
        }

        public void Init()
        {
            var kernel = new StandardKernel();
            DependencyResolver.SetResolver(new Tools.NinjectDependencyResolver(kernel));

            InitRepository(kernel);
        }

        public static void SetContext(Controller controller, UserRole role)
        {
            IdentityControllerDecorator icd = new IdentityControllerDecorator(role);
            RouteDataControllerDecorator rdcd = new RouteDataControllerDecorator(role);
            HttpContextControllerDecorator hccd = new HttpContextControllerDecorator();

            hccd.SetComponent(controller);
            icd.SetComponent(hccd);
            rdcd.SetComponent(icd);
        }

        public static ApplicationSignInManager GetApplicationSignInManager()
        {
            var authenticationManager = new Mock<IAuthenticationManager>();

            var signInManager = new Mock<ApplicationSignInManager>(GetApplicationUserManager(), authenticationManager.Object);
            signInManager.Setup(
                x => x.PasswordSignInAsync(It.Is<string>(s => s == "www"), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(SignInStatus.Success));

            signInManager.Setup(
                x => x.PasswordSignInAsync(It.Is<string>(s => s == "qwe8"), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>()))
                .Returns(Task.FromResult(SignInStatus.LockedOut));

            return signInManager.Object;
        }

        public static ApplicationUserManager GetApplicationUserManager()
        {
//            var userStore = new Mock<IUserStore<ApplicationUser>>();
//            var userManager = new Mock<ApplicationUserManager>(userStore.Object);
//            userManager.Setup(u => u.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
//                .Returns(Task.FromResult(IdentityResult.Success));
//            userManager.Setup(u => u.AddToRoleAsync(It.IsAny<string>(), It.Is<string>(s => s == "notUser"))).ReturnsAsync(IdentityResult.Success);
            return null; /*userManager.Object;*/
        }

        private static void InitRepository(IKernel kernel)
        {
            //kernel.Bind(typeof(IRepository<>)).To(typeof(MockRepository<>));
            //Event
            kernel.Bind<MockRepositoryEvent>().To<MockRepositoryEvent>();
            kernel.Bind<IRepository<Event>>().ToMethod(p => kernel.Get<MockRepositoryEvent>().Object);

            //AspNetUser
            kernel.Bind<MockRepositoryUser>().To<MockRepositoryUser>();
            kernel.Bind<IRepository<AspNetUser>>().ToMethod(p => kernel.Get<MockRepositoryUser>().Object);

            //Template
            kernel.Bind<MockRepositoryTemplate>().To<MockRepositoryTemplate>();
            kernel.Bind<IRepository<Template>>().ToMethod(p => kernel.Get<MockRepositoryTemplate>().Object);
        }
    }
}
