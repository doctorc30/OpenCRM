using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;
using bbom.Data.Repository.Interfaces;

namespace bbom.Data.Repository.Imp
{
    public class Entity: IDataContext
    {
        private readonly ContextMenager _contextMenager;

        public Entity(ContextMenager contextMenager)
        {
            _contextMenager = contextMenager;
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return _contextMenager.GetContext<TEntity>().Set<TEntity>();
        }

        public int SaveChanges<TEntity>()
        {
            return _contextMenager.GetContext<TEntity>().SaveChanges();
        }

        public Task<int> SaveChangesAsync<TEntity>()
        {
            return _contextMenager.GetContext<TEntity>().SaveChangesAsync();
        }

        public void Dispose()
        {
            //_contextMenager.Current.Dispose();
        }

        public DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            return _contextMenager.GetContext<TEntity>().Entry(entity);
        }

    }
}