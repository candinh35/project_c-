using Framework.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Framework.Core.Entities;

public class BaseUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
{
    private readonly TContext _context;
    private Dictionary<Type, object> _repositories;
    private bool _disposed = false;

    public BaseUnitOfWork(TContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _repositories = new Dictionary<Type, object>();
    }

    public IRepository<TContext> GetRepository<TContext>() where TContext : class, IAudit, ISoftDelete, IEntity<Guid>
    {
        if (!_repositories.ContainsKey(typeof(TContext)))
        {
            _repositories[typeof(TContext)] = new BaseRepository<TContext>(_context);
        }
        return (IRepository<TContext>)_repositories[typeof(TContext)];
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void BeginTransaction()
    {
        _context.Database.BeginTransaction();
    }

    public void RollBack()
    {
        var currentTransaction = _context.Database.CurrentTransaction;
        if (currentTransaction != null)
            _context.Database.RollbackTransaction();
    }

    public void Commit()
    {
        _context.Database.CommitTransaction();
    }

    public void Rollback()
    {
        var curentTransaction = _context.Database.CurrentTransaction;

        if (curentTransaction != null)
            _context.Database.RollbackTransaction();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}