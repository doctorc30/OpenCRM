using System;
using bbom.Data.IdentityModel;
using bbom.Data.Repository.Interfaces;

namespace bbom.Data
{
    public static class DataFasade
    {
        public static IRepository<T> GetRepository<T>() where T : class
        {
            return (IRepository<T>) NinjectDataCore.GetInstance().Kernel.GetService(typeof (IRepository<T>));
        }

        public static dynamic GetRepository(Type type)
        {
            return NinjectDataCore.GetInstance().Kernel.GetService(type);
        }

        public static AspNetUser GetUserByName(string name)
        {
            var users = GetRepository<AspNetUser>().GetAll();
            foreach (var user in users)
            {
                if (user.UserName == name)
                    return user;
            }
            throw new Exception($"Пользователя {name} не существует.");
        }
    }
}