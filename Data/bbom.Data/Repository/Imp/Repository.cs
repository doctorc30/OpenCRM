using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using bbom.Data.Repository.Interfaces;

namespace bbom.Data.Repository.Imp
{
    /// <summary>
    /// Базовый класс для всех классов-наследников модели приложения, предоставляющие основные методы доступа к хранилищам
    /// </summary>
    /// <param name="TEntity">Тип репозитория</param>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Экземпляр класса InvestNetworkEntities, предоставляет доступ к хранилищу данных
        /// </summary>
        private IDataContext dataContext;

        /// <summary>
        /// Экземпляр класса InvestNetworkEntities, предоставляет доступ к хранилищу данных
        /// </summary>
        /// <param name="TEntity">Класс, являющийся типом модели, к которому будет запрошен доступ</param>
        /// <returns>Объект IDbSet, представляющий собой набор записей заданного типа</returns>
        public DbSet<TEntity> Entities
        {
            get { return this.dataContext.Set<TEntity>(); }
        }

        /// <summary>  
        /// Инициализирует новый экземпляр Repository с внедрением зависемостей к хранилищу данных.
        /// </summary>  
        /// <param name="context">Экземпляр класса InvestNetworkEntities, предоставляющий доступ к хранилищу данных приложения.</param>
        /// <returns>Новый экземпляр ProjectController.</returns>
        public Repository(IDataContext context)
        {
            this.dataContext = context;
        }

        /// <summary>  
        /// Метод отвечающий за предоставление набора записей заданного типа.</summary>
        /// <returns>Экземпляр IQueryable, набор записей.</returns>
        public IQueryable<TEntity> GetAll()
        {
            return Entities;
        }

        public DbEntityEntry<TEntity> Entry(TEntity entity)
        {
            return this.dataContext.Entry(entity);
        }

        public IQueryable<TEntity> Include(string path)
        {
            return Entities.Include(path);
        }

        /// <summary>  
        /// Метод отвечающий за предоставление объекта с заданным идентификатором и типом.</summary>
        public TEntity GetById(object id)
        {
            return Entities.Find(id);
        }

        /// <summary>  
        /// Метод отвечающий за вставку данного объекта.</summary>
        public void Insert(TEntity entity)
        {
            Entities.Add(entity);
        }

        /// <summary>  
        /// Метод отвечающий за удаление данного объекта.</summary>
        public void Delete(TEntity entity)
        {
            Entities.Remove(entity);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.dataContext != null)
                {
                    this.dataContext.Dispose();
                    this.dataContext = null;
                }
            }
        }

        /// <summary>  
        /// Сохраняет изменения в базу данных.</summary>
        public void SaveChanges()
        {
            try
            {
            this.dataContext.SaveChanges<TEntity>();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            try
            {
                return await dataContext.SaveChangesAsync<TEntity>();
            }
            catch (DbEntityValidationException dbEx)
            {
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        Trace.TraceInformation("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                    }
                }
            }
            return 0;
        }

        /// <summary>  
        /// Метод отвечающий за предоставление объекта с заданным идентификатором и типом.</summary>
        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await Entities.FindAsync(id);
        }


        public IQueryable<TEntity> SearchFor(Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate);
        }

        /// <summary>  
        /// Метод отвечающий за обновление данного объекта.</summary>
        public async Task EditAsync(TEntity entity)
        {
            dataContext.Entry(entity).State = EntityState.Modified;
            await dataContext.SaveChangesAsync<TEntity>();
        }

        /// <summary>  
        /// Метод отвечающий за обновление данного объекта.</summary>
        public void Edit(TEntity entity)
        {
            dataContext.Entry(entity).State = EntityState.Modified;
            dataContext.SaveChanges<TEntity>();
        }

        /// <summary>  
        /// Метод отвечающий за вставку данного объекта.</summary>
        public async Task InsertAsync(TEntity entity)
        {
            Entities.Add(entity);
            await dataContext.SaveChangesAsync<TEntity>();
        }

        /// <summary>  
        /// Метод отвечающий за удаление данного объекта.</summary>
        public async Task DeleteAsync(TEntity entity)
        {
            Entities.Remove(entity);
            await dataContext.SaveChangesAsync<TEntity>();
        }
    }
}