using System;
using System.Web;
using AutoMapper;
using bbom.Admin.Core;
using bbom.Admin.Core.DataExtensions.Helpers.Impl;
using bbom.Admin.Core.DataExtensions.Helpers.Interfaces;
using bbom.Admin.Core.Identity;
using bbom.Admin.Core.Mappers;
using bbom.Admin.Core.Menu;
using bbom.Admin.Core.Services.AccessService;
using bbom.Admin.Core.Services.DiscountService;
using bbom.Admin.Core.Services.EmailService;
using bbom.Admin.Core.Services.PaymentService;
using bbom.Admin.Core.Services.PaySystemService;
using bbom.Data;
using bbom.Data.Repository;
using bbom.Data.Repository.Imp;
using bbom.Data.Repository.Interfaces;
using elcoin.Admin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Web.Infrastructure.DynamicModuleHelper;
using Ninject;
using Ninject.Web.Common;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(NinjectWebCommon), "Stop")]

namespace elcoin.Admin
{
    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
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
            kernel.Bind<IDataContext>().To<Entity>();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>));
            //core
            kernel.Bind<IUsresHelper>().To<UsersHelper>().InRequestScope();
            kernel.Bind<IRegisterHelper>().To<RegisterHelper>().InRequestScope();
            kernel.Bind<ITemplateHelper>().To<TemplateHelper>().InRequestScope();
            //core.services
            kernel.Bind<IDiscountService>().To<DiscountService>().InRequestScope();
            kernel.Bind<IPaymentService>().To<PaymentService>().InRequestScope();
            kernel.Bind<IPaySystemService>().To<YandexMoneyService>().InRequestScope();
            kernel.Bind<IAccessService>().To<AccessServise>().InRequestScope();
            kernel.Bind<IEmailService>().To<ServerEmailService>().InSingletonScope();
            kernel.Bind<IMenuGenerator>().To<MenuGenerator>().InSingletonScope();
            kernel.Bind<IClaimsHelper>().To<ClaimsHelper>().InSingletonScope();
            kernel.Bind<ISettingsHelper>().To<SettingHelper>().InSingletonScope();

            NinjectDataCore.GetInstance().SetKernel(kernel);
            NinjectAdminCoreKernel.GetInstance().SetKernel(kernel);
        }        
    }
}
