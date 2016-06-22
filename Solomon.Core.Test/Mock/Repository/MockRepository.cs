using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using bbom.Data.IdentityModel;
using bbom.Data.Repository.Interfaces;
using Moq;

namespace Solomon.Test.Mock.Repository
{
    public class MockRepository<T> : IRepository<T> where T : class
    {
        protected List<T> List { get; set; }
        private Mock<IRepository<T>> _mock;

        public MockRepository()
        { 
            List = new List<T>();
            _mock = new Mock<IRepository<T>>();
            _mock.Setup(repository => repository.SaveChangesAsync()).ReturnsAsync(1);
            _mock.Setup(repository => repository.SaveChanges());
            _mock.Setup(repository => repository.EditAsync(It.IsAny<T>())).Returns(Task.FromResult(1));
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }

        public DbSet<T> Entities { get; }
        public IQueryable<T> GetAll()
        {
            List.Add(new MockObjectCreator<T>().Create());
            return List.AsQueryable();
        }

        public T GetById(object id)
        {
            if (typeof(T) == typeof(AspNetUser))
            {
                var o = new MockObjectCreator<T>().Create();
                var user = o as AspNetUser;
                user.Id = "" + id;
                return o;
            }
            return new MockObjectCreator<T>().Create();
        }

        public void Insert(T entity)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new System.NotImplementedException();
        }

        public void SaveChanges()
        {
            
        }

        public Task<int> SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<T> GetByIdAsync(object id)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            throw new System.NotImplementedException();
        }

        public Task EditAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public void Edit(T entity)
        {
            throw new System.NotImplementedException();
        }

        public Task InsertAsync(T entity)
        {
            return Task.FromResult(1);
        }

        public Task DeleteAsync(T entity)
        {
            throw new System.NotImplementedException();
        }

        public DbEntityEntry<T> Entry(T entity)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<T> Include(string path)
        {
            throw new System.NotImplementedException();
        }
    }
}