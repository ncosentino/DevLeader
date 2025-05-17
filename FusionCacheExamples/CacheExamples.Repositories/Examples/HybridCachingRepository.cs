using CacheExamples.Repositories.Common;

using Microsoft.Extensions.Caching.Hybrid;

namespace CacheExamples.Repositories.Examples;

public sealed class HybridCachingRepository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly IRepository<TEntity> _repository;
    private readonly HybridCache _cache;

    public HybridCachingRepository(
        IRepository<TEntity> repository,
        HybridCache memoryCache)
    {
        _repository = repository;
        _cache = memoryCache;
    }

    public async ValueTask<TEntity?> GetAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _cache.GetOrCreateAsync(
            key: $"{id}",
            async entry => await _repository.GetAsync(id, cancellationToken),
            cancellationToken: cancellationToken);
    }

    public async ValueTask<IReadOnlyCollection<TEntity>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        // How might you handle caching here?
        return await _repository.GetAllAsync(cancellationToken);
    }

    public async ValueTask CreateAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(entity.Id.ToString(), cancellationToken);
        await _repository.CreateAsync(entity, cancellationToken);
    }

    public async ValueTask<bool> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(entity.Id.ToString(), cancellationToken);
        await _repository.UpdateAsync(entity, cancellationToken);
        return true;
    }

    public async ValueTask<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken)
    {
        await _cache.RemoveAsync(id.ToString(), cancellationToken);
        return await _repository.DeleteAsync(id, cancellationToken);
    }
}