using System.Linq.Expressions;
using Framework.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Framework.Core.Entities;

public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IAudit, ISoftDelete, IEntity<Guid>
{
    private readonly DbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public BaseRepository(DbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = _context.Set<TEntity>();
    }

    public IQueryable<TEntity> GetQuery(Func<DbSet<TEntity>, IQueryable<TEntity>>? func = null)
    {
        return func != null ? func(_dbSet) : _dbSet;
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbSet.AsNoTracking();
    }

    public async Task<List<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<TEntity?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.AnyAsync(predicate);
    }

    public async Task AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public async Task UpdateSync(TEntity entity)
    {
        _dbSet.Attach(entity);
        await Task.CompletedTask;
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }

    public void DeleteRange(IEnumerable<TEntity> entities)
    {
        _dbSet.RemoveRange(entities);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}