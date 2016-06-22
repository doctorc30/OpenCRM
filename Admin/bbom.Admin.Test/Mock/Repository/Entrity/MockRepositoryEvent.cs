using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bbom.Data.ContentModel;
using Moq;

namespace bbom.Admin.Test.Mock.Repository.Entrity
{
    public class MockRepositoryEvent : MockRepository<Event>
    {
        protected override void Generate()
        {
            base.Generate();
            var e = new Event
            {
                Id = 1,
                StartDate = DateTime.Today.AddDays(1),
                EndDate = DateTime.Today,
                Title = "test"
            };
            List = new List<Event> {e};
            Setup(repository => repository.GetAll()).Returns(List.AsQueryable());
            Setup(repository => repository.EditAsync(It.IsAny<Event>())).Returns(Task.FromResult(1));
            Setup(repository => repository.InsertAsync(It.IsAny<Event>())).Returns(Task.FromResult(1));
        }
    }
}