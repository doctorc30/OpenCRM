using System.Web;
using AutoMapper;
using bbom.Admin.Core.DataExtensions.Helpers.Impl;
using bbom.Admin.Core.DataExtensions.Helpers.Interfaces;
using bbom.Admin.Core.Identity;
using bbom.Admin.Core.Mappers;
using bbom.Admin.Core.Menu;
using bbom.Admin.Core.Services.AccessService;
using bbom.Admin.Core.Services.DiscountService;
using bbom.Admin.Core.Services.EmailService;
using bbom.Admin.Core.Services.PaySystemService;
using bbom.Data.Repository;
using bbom.Data.Repository.Imp;
using bbom.Data.Repository.Interfaces;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Ninject;
using Ninject.Web.Common;

namespace bbom.Admin.Core
{
    public class NinjectAdminCoreKernel
    {
        private IKernel _kernel;
        private static NinjectAdminCoreKernel _ninjectDataCore;

        private NinjectAdminCoreKernel()
        {

        }

        public static NinjectAdminCoreKernel GetInstance()
        {
            if (_ninjectDataCore == null)
            {
                _ninjectDataCore = new NinjectAdminCoreKernel();
                return _ninjectDataCore;
            }
            return _ninjectDataCore;
        }

        public void SetBindings(IKernel kernel)
        {
            //Identity
            //kernel.Bind<ApplicationUserManager>().ToSelf().InRequestScope();
            kernel.Bind<ApplicationUserManager>()
                .ToMethod(x => HttpContext.Current.GetOwinContext()
                .Get<ApplicationUserManager>()).InRequestScope();
            kernel.Bind<ApplicationSignInManager>().ToSelf().InRequestScope();
            kernel.Bind<ApplicationDbContext>().ToSelf().InRequestScope();
            kernel.Bind<IUserStore<ApplicationUser>>()
                .ToMethod(x => new UserStore<ApplicationUser>(
                    x.Kernel.Get<ApplicationDbContext>())).InRequestScope();
            kernel.Bind<IAuthenticationManager>()
                .ToMethod(x => HttpContext.Current.GetOwinContext().Authentication)
                .InRequestScope();
            //web
            kernel.Bind<IMapper>().ToMethod(x => new AutoMapperRegistry().CreateMapper()).InSingletonScope();
            //data
            kernel.Bind<ContextMenager>().ToSelf().InRequestScope();
            kernel.Bind<IDataContext>().To<Entity>().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
            //core
            kernel.Bind<IUsresHelper>().To<UsersHelper>().InRequestScope();
            kernel.Bind<IRegisterHelper>().To<RegisterHelper>().InRequestScope();
            kernel.Bind<ITemplateHelper>().To<TemplateHelper>().InRequestScope();
            //core.services
            kernel.Bind<IDiscountService>().To<DiscountService>().InRequestScope();
            kernel.Bind<IPaySystemService>().To<YandexMoneyService>().InRequestScope();
            kernel.Bind<IAccessService>().To<AccessServise>().InRequestScope();
            kernel.Bind<IEmailService>().To<ServerEmailService>();
            kernel.Bind<IMenuGenerator>().To<MenuGenerator>().InSingletonScope();
            kernel.Bind<IClaimsHelper>().To<ClaimsHelper>().InSingletonScope();
            kernel.Bind<ISettingsHelper>().To<SettingHelper>().InSingletonScope();
        }

        public void SetKernel(IKernel kernel)
        {
            _kernel = kernel;
        }

        public IKernel Kernel
        {
            get
            {
                if (_kernel == null)
                {
                    _kernel = new StandardKernel();
                    SetBindings(_kernel);
                    return _kernel;
                }
                return _kernel;
            }
        }
    }
}