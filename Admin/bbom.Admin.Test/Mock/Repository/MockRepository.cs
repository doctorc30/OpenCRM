using System.Collections.Generic;
using System.Linq;
using bbom.Data.Repository.Interfaces;
using Moq;

namespace bbom.Admin.Test.Mock.Repository
{
    public class MockRepository<T> : Mock<IRepository<T>> where T : class
    {
        protected List<T> List { get; set; }

        protected MockRepository()
        {
            Generate();
        }

        protected virtual void Generate()
        {
            List = new List<T>();
            Setup(repository => repository.SaveChangesAsync()).ReturnsAsync(1);
        }
    }
}