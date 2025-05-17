namespace CacheExamples.Repositories.Common;

public interface IRepository<TEntity>
{
    ValueTask CreateAsync(
        TEntity entity, 
        CancellationToken cancellationToken);

    ValueTask<bool> DeleteAsync(
        int id,
        CancellationToken cancellationToken);

    ValueTask<IReadOnlyCollection<TEntity>> GetAllAsync(
        CancellationToken cancellationToken);

    ValueTask<TEntity?> GetAsync(
        int id, 
        CancellationToken cancellationToken);

    ValueTask<bool> UpdateAsync(
        TEntity entity, 
        CancellationToken cancellationToken);
}