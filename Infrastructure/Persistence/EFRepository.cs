using Application.Common.Interface;
using Domain.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Persistence
{
    internal class EFRepository<TEntity, TIdentifier> : IRepository<TEntity, TIdentifier> where TEntity : BaseEntity<TIdentifier>
    {
        #region Properties
        protected readonly ApplicationDBContext _context;

        protected DbSet<TEntity>? _entities;
        #endregion
        #region Ctor

        public EFRepository(ApplicationDBContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region Repository Methods
        /// <summary>
        /// Gets an entity set
        /// </summary>
        protected virtual DbSet<TEntity> Entities
        {
            get
            {
                if (_entities is null)
                    _entities = _context.Set<TEntity>();

                return _entities;
            }
        }

        public virtual IEnumerable<TEntity> GetAll(string[] includes)
        {
            try
            {
                IQueryable<TEntity> entityQuery = Entities;

                foreach (var include in includes)
                {
                    entityQuery = entityQuery.Include(include);
                }

                return entityQuery;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual IEnumerable<TEntity> GetMany(Expression<Func<TEntity, bool>> where, string[] includes)
        {
            IQueryable<TEntity> entityQuery = Entities;

            foreach (var include in includes)
            {
                entityQuery = entityQuery.Include(include);
            }

            return entityQuery.Where(where);
        }

        public virtual async Task<TEntity> Get(TIdentifier id, string[] includes)
        {
            try
            {
                IQueryable<TEntity> entityQuery = Entities;

                foreach (var include in includes)
                {
                    entityQuery = entityQuery.Include(include);
                }

                return (await entityQuery.FirstOrDefaultAsync(x => x.Id!.Equals(id)))!;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<TEntity> Get(Expression<Func<TEntity, bool>> where, string[] includes)
        {
            try
            {
                IQueryable<TEntity> entityQuery = Entities;

                foreach (var include in includes)
                {
                    entityQuery = entityQuery.Include(include);
                }

                return (await entityQuery.FirstOrDefaultAsync(where))!;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public virtual async Task<int> Count()
        {
            return await Entities.CountAsync();
        }

        public virtual async Task<int> Count(Expression<Func<TEntity, bool>> where)
        {
            return await Entities.CountAsync(where);
        }

        public virtual async Task<bool> Exists(TIdentifier id)
        {
            try
            {
                return await Entities.AnyAsync(e => e.Id!.Equals(id));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task<bool> Exists(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                return await Entities.AnyAsync(where);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            try
            {
                if (entity is null)
                    throw new ArgumentNullException(nameof(entity));

                await Entities.AddAsync(entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual void Update(TEntity entity)
        {
            if (entity is null)
                throw new ArgumentNullException(nameof(entity));
            try
            {
                Entities.Update(entity);
            }
            catch (Exception)
            {
                throw;
            }
            //catch (DbUpdateException exception)
            //{
            //    //ensure that the detailed error text is saved in the Log
            //    throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            //}
        }

        public virtual async Task<bool> Delete(TIdentifier id)
        {
            try
            {
                var entity = await Entities.FindAsync(id);
                if (entity is null)
                    return false;

                Entities.Remove(entity);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual bool Delete(TEntity entity)
        {
            try
            {
                if (entity is null)
                    return false;

                Entities.Remove(entity);
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual bool Delete(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                var entities = Entities.Where(where);
                Entities.RemoveRange(entities);
                return true;
            }
            catch (DbUpdateException exception)
            {
                //ensure that the detailed error text is saved in the Log
                throw new Exception(GetFullErrorTextAndRollbackEntityChanges(exception), exception);
            }
        }

        public virtual void Save()
        {
            _context.SaveChanges();
        }

        public virtual void SaveAsync()
        {
            _context.SaveChangesAsync();
        }

        /// <summary>
        /// Gets a table
        /// </summary>
        public virtual IQueryable<TEntity> Table => Entities;

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        public virtual IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();
        #endregion

        #region Utility
        protected string GetFullErrorTextAndRollbackEntityChanges(DbUpdateException exception)
        {
            //rollback entity changes
            if (_context is DbContext dbContext)
            {
                var entries = dbContext.ChangeTracker.Entries()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified).ToList();

                entries.ForEach(entry =>
                {
                    try
                    {
                        entry.State = EntityState.Unchanged;
                    }
                    catch (InvalidOperationException)
                    {
                        // ignored
                    }
                });
            }

            try
            {
                _context.SaveChanges();
                return exception.ToString();
            }
            catch (Exception ex)
            {
                //if after the rollback of changes the context is still not saving,
                //return the full text of the exception that occurred when saving
                return ex.ToString();
            }
        }
        #endregion
    }
}