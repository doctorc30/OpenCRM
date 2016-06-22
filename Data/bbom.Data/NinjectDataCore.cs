using bbom.Data.Repository;
using bbom.Data.Repository.Imp;
using bbom.Data.Repository.Interfaces;
using Ninject;
using Ninject.Web.Common;

namespace bbom.Data
{
    public class NinjectDataCore
    {
        private IKernel _kernel;
        private static NinjectDataCore _ninjectDataCore;

        private NinjectDataCore()
        {

        }

        public static NinjectDataCore GetInstance()
        {
            if (_ninjectDataCore == null)
            {
                _ninjectDataCore = new NinjectDataCore();
                return _ninjectDataCore;
            }
            return _ninjectDataCore;
        }

        public void SetBindings(IKernel kernel)
        {
            kernel.Bind<ContextMenager>().ToSelf().InRequestScope();
            kernel.Bind<IDataContext>().To<Entity>().InRequestScope();
            kernel.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
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