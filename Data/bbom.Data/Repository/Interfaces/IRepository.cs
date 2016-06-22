using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace bbom.Data.Repository.Interfaces
{
    /// <summary>
    /// Определяет основные методы доступа к хранилищам
    /// </summary>
    /// <typeparam name="TEntity">Тип репозитория</typeparam>
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        DbSet<TEntity> Entities { get; }
        IQueryable<TEntity> GetAll();
        TEntity GetById(object id);
        void Insert(TEntity entity);
        void Delete(TEntity entity);
        void SaveChanges();
        Task<int> SaveChangesAsync();
        Task<TEntity> GetByIdAsync(object id);
        IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate);
        Task EditAsync(TEntity entity);
        void Edit(TEntity entity);
        Task InsertAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        DbEntityEntry<TEntity> Entry(TEntity entity);
        IQueryable<TEntity> Include(string path);
    }
}
