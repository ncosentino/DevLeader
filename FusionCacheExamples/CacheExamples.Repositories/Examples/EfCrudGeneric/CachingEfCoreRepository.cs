using CacheExamples.Repositories.Common;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace CacheExamples.Repositories.Examples.EfCrudGeneric;

public sealed class CachingEfCoreRepository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;
    private readonly IMemoryCache _memoryCache;

    public CachingEfCoreRepository(
        IDbContextFactory<AppDbContext> dbContextFactory,
        IMemoryCache memoryCache)
    {
        _dbContextFactory = dbContextFactory;
        _memoryCache = memoryCache;
    }

    public async ValueTask<TEntity?> GetAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync(
            key: $"{id}",
            async entry =>
            {
                using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
                return await dbContext.Set<TEntity>().FirstOrDefaultAsync(
                    b => b.Id == id,
                    cancellationToken: cancellationToken);
            });
    }

    public async ValueTask<IReadOnlyCollection<TEntity>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        // How might you handle caching here?

        using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async ValueTask CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
        _memoryCache.Remove(entity.Id.ToString());
        using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        await dbContext.Set<TEntity>().AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask<bool> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        var exists = await dbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(
            x => x.Id == entity.Id,
            cancellationToken: cancellationToken) is not null;
        if (!exists)
        {
            return false;
        }

        _memoryCache.Remove(entity.Id.ToString());

        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync(cancellationToken);

        //_memoryCache.Set(entity.Id.ToString(), entity);

        return true;
    }

    public async ValueTask<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(cancellationToken);
        var entity = await dbContext
            .Set<TEntity>()
            .FindAsync(
                [id],
                cancellationToken: cancellationToken);
        if (entity is null)
        {
            return false;
        }

        _memoryCache.Remove(entity.Id.ToString());

        dbContext.Set<TEntity>().Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}
