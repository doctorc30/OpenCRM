using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace bbom.Admin.Test.Controllers
{
    public class BaseTests
    {
        [TestInitialize]
        public void Init()
        {
            UnitTestControllerHelper.GetInstance().Init();
        }
    }
}
