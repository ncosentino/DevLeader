using CacheExamples.Repositories.Common;

using Microsoft.Extensions.Caching.Memory;

namespace CacheExamples.Repositories.Examples;

public sealed class CachingRepository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMemoryCache _memoryCache;

    public CachingRepository(
        IRepository<TEntity> repository,
        IMemoryCache memoryCache)
    {
        _repository = repository;
        _memoryCache = memoryCache;
    }

    public async ValueTask<TEntity?> GetAsync(
        int id,
        CancellationToken cancellationToken)
    {
        return await _memoryCache.GetOrCreateAsync(
            key: $"{id}",
            async entry => await _repository.GetAsync(id, cancellationToken));
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
        _memoryCache.Remove(entity.Id.ToString());
        await _repository.CreateAsync(entity, cancellationToken);
    }

    public async ValueTask<bool> UpdateAsync(
        TEntity entity,
        CancellationToken cancellationToken)
    {
        _memoryCache.Remove(entity.Id.ToString());
        await _repository.UpdateAsync(entity, cancellationToken);
        return true;
    }

    public async ValueTask<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken)
    {
        _memoryCache.Remove(id.ToString());
        return await _repository.DeleteAsync(id, cancellationToken);
    }
}
