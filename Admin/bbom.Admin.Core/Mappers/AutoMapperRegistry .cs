using System;
using System.Linq;
using AutoMapper;

namespace bbom.Admin.Core.Mappers
{
    public class AutoMapperRegistry
    {
        public IMapper CreateMapper()
        {
            var profiles =
                from t in typeof(AutoMapperRegistry).Assembly.GetTypes()
                where typeof(Profile).IsAssignableFrom(t)
                select (Profile)Activator.CreateInstance(t);

            var configuration = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles)
                {
                    cfg.AddProfile(profile);
                }
            });
            return configuration.CreateMapper();
        }
    }
}