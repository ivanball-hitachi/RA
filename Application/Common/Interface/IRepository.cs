using Domain.Common;
using System.Linq.Expressions;

namespace Application.Common.Interface
{
    public interface IRepository<T, TIdentifier> where T : BaseEntity<TIdentifier>
    {
        IEnumerable<T> GetAll(string[] includes);
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where, string[] includes);
        Task<T> Get(TIdentifier id, string[] includes);
        Task<T> Get(Expression<Func<T, bool>> where, string[] includes);
        Task<int> Count();
        Task<int> Count(Expression<Func<T, bool>> where);
        Task<bool> Exists(TIdentifier id);
        Task<bool> Exists(Expression<Func<T, bool>> where);
        Task AddAsync(T entity);
        void Update(T entity);
        Task<bool> Delete(TIdentifier id);
        bool Delete(T entity);
        bool Delete(Expression<Func<T, bool>> where);
        #region Properties

        /// <summary>
        /// Gets a table
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// Gets a table with "no tracking" enabled (EF feature) Use it only when you load record(s) only for read-only operations
        /// </summary>
        IQueryable<T> TableNoTracking { get; }

        #endregion
    }
}