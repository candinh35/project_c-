using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Framework.Core.Abstractions;

public interface IUnitOfWork : IDisposable
{
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : class, IAudit, ISoftDelete, IEntity<Guid>;
    Task<int> SaveChangesAsync();
    void BeginTransaction();
    void Commit();
    void Rollback();
}
