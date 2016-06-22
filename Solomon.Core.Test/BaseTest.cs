using Microsoft.VisualStudio.TestTools.UnitTesting;
using Solomon.Test.TestTools;

namespace Solomon.Test
{
    public class BaseTest
    {
        [TestInitialize]
        public void Init()
        {
            UnitTestHelper.GetInstance().Init();
        }
    }
}