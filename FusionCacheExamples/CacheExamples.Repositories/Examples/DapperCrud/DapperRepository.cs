using CacheExamples.Repositories.Common;

using Dapper;

using Microsoft.EntityFrameworkCore;

using System.Data;

public sealed class DapperRepository : IRepository<Entity>
{
    private readonly Func<IDbConnection> _connectionFactory;

    public DapperRepository(Func<IDbConnection> connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async ValueTask<Entity?> GetAsync(
        int id,
        CancellationToken cancellationToken)
    {
        using var dbConnection = _connectionFactory.Invoke();
        return await dbConnection.QueryFirstOrDefaultAsync<Entity>(new CommandDefinition(
            "SELECT Id, Value FROM Entities WHERE Id = @Id",
            new { Id = id },
            cancellationToken: cancellationToken));
    }

    public async ValueTask<IReadOnlyCollection<Entity>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        using var dbConnection = _connectionFactory.Invoke();
        return (await dbConnection
            .QueryAsync<Entity>(new CommandDefinition(
                "SELECT Id, Value FROM Entities",
                cancellationToken: cancellationToken)))
            .ToList();
    }

    public async ValueTask CreateAsync(
        Entity entity,
        CancellationToken cancellationToken)
    {
        using var dbConnection = _connectionFactory.Invoke();
        await dbConnection.ExecuteAsync(new CommandDefinition(
            "INSERT INTO Entities (Id, Value) VALUES (@Id, @Value)",
            new { entity.Id, entity.Value },
            cancellationToken: cancellationToken));
    }

    public async ValueTask<bool> UpdateAsync(
        Entity entity,
        CancellationToken cancellationToken)
    {
        using var dbConnection = _connectionFactory.Invoke();
        var rows = await dbConnection.ExecuteAsync(new CommandDefinition(
            "UPDATE Entities SET Value = @Value WHERE Id = @Id",
            new { entity.Id, entity.Value },
            cancellationToken: cancellationToken));
        return rows > 0;
    }

    public async ValueTask<bool> DeleteAsync(
        int Id,
        CancellationToken cancellationToken)
    {
        using var dbConnection = _connectionFactory.Invoke();
        var rows = await dbConnection.ExecuteAsync(new CommandDefinition(
            "DELETE FROM Entities WHERE Id = @Id",
            new { Id },
            cancellationToken: cancellationToken));
        return rows > 0;
    }
}