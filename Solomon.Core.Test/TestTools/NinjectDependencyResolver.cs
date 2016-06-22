using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;

namespace Solomon.Test.TestTools
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private readonly IKernel _kernel;

        public NinjectDependencyResolver(IKernel kernel)
        {
            _kernel = kernel;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.BaseType == typeof (Controller))
            {
                Controller controller = (Controller) _kernel.TryGet(serviceType);
                return controller;
            }
            return _kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _kernel.GetAll(serviceType);
            }
            catch (Exception)
            {
                return new List<object>();
            }
        }
    }
}
