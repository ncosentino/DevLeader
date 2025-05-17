using CacheExamples.Repositories.Common;

using Microsoft.EntityFrameworkCore;

namespace CacheExamples.Repositories.Examples.EfCrud;

public interface IEntityRepository
{
    ValueTask CreateAsync(
        Entity entity, 
        CancellationToken cancellationToken);

    ValueTask<bool> DeleteAsync(
        int Id,
        CancellationToken cancellationToken);

    ValueTask<IReadOnlyCollection<Entity>> GetAllAsync(
        CancellationToken cancellationToken);

    ValueTask<Entity?> GetAsync(
        int id, 
        CancellationToken cancellationToken);

    ValueTask<bool> UpdateAsync(
        Entity entity,
        CancellationToken cancellationToken);
}

public sealed class EfCoreRepository : IEntityRepository
{
    private readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public EfCoreRepository(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async ValueTask<Entity?> GetAsync(
        int id,
        CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(cancellationToken);
        return await dbContext
            .Set<Entity>()
            .FirstOrDefaultAsync(
                b => b.Id == id,
                cancellationToken: cancellationToken);
    }

    public async ValueTask<IReadOnlyCollection<Entity>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(cancellationToken);
        return await dbContext
            .Set<Entity>()
            .ToArrayAsync(cancellationToken);
    }

    public async ValueTask CreateAsync(
        Entity entity,
        CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(cancellationToken);
        await dbContext
            .Set<Entity>()
            .AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async ValueTask<bool> UpdateAsync(
        Entity entity,
        CancellationToken cancellationToken)
    {
        using var dbContext = await _dbContextFactory
            .CreateDbContextAsync(cancellationToken);
        var exists = await dbContext
            .Set<Entity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == entity.Id,
                cancellationToken: cancellationToken) 
            is not null;
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
            .Set<Entity>()
            .FindAsync(
                [Id],
                cancellationToken: cancellationToken);
        if (entity is null)
        {
            return false;
        }

        dbContext
            .Set<Entity>()
            .Remove(entity);
        await dbContext.SaveChangesAsync(cancellationToken);
        return true;
    }
}