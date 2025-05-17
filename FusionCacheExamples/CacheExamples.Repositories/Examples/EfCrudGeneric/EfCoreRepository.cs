using CacheExamples.Repositories.Common;

using Microsoft.EntityFrameworkCore;

namespace CacheExamples.Repositories.Examples.EfCrudGeneric;

public sealed class EfCoreRepository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public EfCoreRepository(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async ValueTask<TEntity?> GetAsync(
        int id,
        CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.Set<TEntity>().FirstOrDefaultAsync(
            b => b.Id == id,
            cancellationToken: cancellationToken);
    }

    public async ValueTask<IReadOnlyCollection<TEntity>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);
        return await dbContext.Set<TEntity>().ToListAsync(cancellationToken);
    }

    public async ValueTask CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
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

        dbContext.Entry(entity).State = EntityState.Modified;
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async ValueTask<bool> DeleteAsync(
        int Id,
        CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(cancellationToken);
        var entity = await dbContext
            .Set<TEntity>()
            .FindAsync(
                [Id],
                cancellationToken: cancellationToken);
        if (entity is null)
        {
            return false;
        }

        dbContext.Set<TEntity>().Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}