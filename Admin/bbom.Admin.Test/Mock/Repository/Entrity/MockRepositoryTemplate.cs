using System.Collections.Generic;
using System.Linq;
using bbom.Data.ContentModel;
using Moq;

namespace bbom.Admin.Test.Mock.Repository.Entrity
{
    public class MockRepositoryTemplate : MockRepository<Template>
    {
        public static Template template = new Template
        {
            Id = 1,
            Body = "",
            Header = "",
            Footer = ""
        };
        protected override void Generate()
        {
            base.Generate();
            List = new List<Template> {template};
            Setup(repository => repository.GetAll()).Returns(List.AsQueryable());
            Setup(repository => repository.GetById(It.IsAny<int>())).Returns(template);            
        }
    }
}