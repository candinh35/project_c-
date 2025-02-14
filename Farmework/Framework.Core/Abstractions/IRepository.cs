using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Framework.Core.Abstractions
{
    public interface IRepository<TEntity> where TEntity : class, IAudit, ISoftDelete, IEntity<Guid>
    {
        IQueryable<TEntity> GetQuery(Func<DbSet<TEntity>, IQueryable<TEntity>>? func = null);
        IQueryable<TEntity> GetAll();
        Task<List<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> GetByIdAsync(Guid id);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(IEnumerable<TEntity> entities);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteRange(IEnumerable<TEntity> entities);
        Task<int> SaveChangesAsync();
    }
}
