using CacheExamples.Repositories.Common;

using ZiggyCreatures.Caching.Fusion;

namespace CacheExamples.Repositories.Examples;

public sealed class FusionCacheRepository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly IRepository<TEntity> _repository;
    private readonly IFusionCache _cache;

    public FusionCacheRepository(
        IRepository<TEntity> repository,
        IFusionCache cache)
    {
        _repository = repository;
        _cache = cache;
    }

    public async ValueTask<TEntity?> GetAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _cache.GetOrSetAsync(
            key: $"{id}",
            async entry => await _repository.GetAsync(id, cancellationToken),
            token: cancellationToken);
    }

    public async ValueTask<IReadOnlyCollection<TEntity>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        // TODO: how might you handle caching here?
        return await _repository.GetAllAsync(cancellationToken);
    }

    public async ValueTask CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
        // TODO: invalidate or set in cache on write?
        //await _cache.RemoveAsync(entity.Id.ToString(), token: cancellationToken);
        await _cache.SetAsync(entity.Id.ToString(), entity, token: cancellationToken);

        await _repository.CreateAsync(entity, cancellationToken);
    }

    public async ValueTask<bool> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
        // TODO: invalidate or set in cache on write?
        //await _cache.RemoveAsync(entity.Id.ToString(), token: cancellationToken);
        await _cache.SetAsync(entity.Id.ToString(), entity, token: cancellationToken);

        await _repository.UpdateAsync(entity, cancellationToken);
        return true;
    }

    public async ValueTask<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(id.ToString(), token: cancellationToken);
        return await _repository.DeleteAsync(id, cancellationToken);
    }
}