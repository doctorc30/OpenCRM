using System;
using bbom.Admin.Core.DataExtensions.Helpers.Interfaces;
using bbom.Admin.Core.Identity;
using bbom.Admin.Core.Menu;
using bbom.Admin.Core.TreeCreator;

namespace bbom.Admin.Core
{
    public static class CoreFasade
    {
        public static IUsresHelper UsersHelper { get; } = GetService<IUsresHelper>();
        public static ISettingsHelper SettingsHelper { get; } = GetService<ISettingsHelper>();
        public static IClaimsHelper ClaimsHelper { get; } = GetService<IClaimsHelper>();
        public static IRegisterHelper RegisterHelper { get; } = GetService<IRegisterHelper>();
        public static ITemplateHelper TemplateHelper { get; } = GetService<ITemplateHelper>();
        //public static ApplicationUserManager ApplicationUserManager { get; } = GetService<ApplicationUserManager>();
        //public static IAuthenticationManager AuthenticationManager { get; } = GetService<IAuthenticationManager>();
        public static IMenuGenerator MenuGenerator { get; } = GetService<IMenuGenerator>();

        public static ApplicationSignInManager CreateApplicationSignInManager()
        {
            //return new ApplicationSignInManager(ApplicationUserManager, AuthenticationManager);
            throw new Exception("Ошибка создания ApplicationSignInManager");
        }

        public static ITreeUsers TreeUsers { get; } = new TreeUsers();

        public static void WriteInHosts(string source, string local)
        {
            //if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(local))
            //    throw new Exception("не верный формат данных");
            //string path = @"c:\Windows\System32\drivers\etc\hosts";
            //File.AppendAllText(path,"\n" + source + " " + local);
        }

        public static T GetService<T>()
        {
            try
            {
                return (T)NinjectAdminCoreKernel.GetInstance().Kernel.GetService(typeof(T));
            }
            catch
            {
                throw new Exception("Сервис не найден");
            }
        }
    }
}