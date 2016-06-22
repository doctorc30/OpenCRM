using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Threading.Tasks;

namespace bbom.Data.Repository.Interfaces
{
    /// <summary>
    /// Интерфейс, используемый для взаимодействия с конкретной реализации ORM
    /// </summary>
    public interface IDataContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges<TEntity>();
        Task<int> SaveChangesAsync<TEntity>();
        void Dispose();
        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
