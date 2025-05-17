using CacheExamples.Repositories.Common;
using CacheExamples.Repositories.Examples;
using CacheExamples.Repositories.Examples.EfCrudGeneric;

using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Caching.Hybrid;

using System.Data;

public static class BuilderExtensionsDapperCrud
{
    public static void SetupDapperCrud(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IRepository<Entity>, DapperRepository>(_ =>
            CreateDapperRepository());
    }

    public static void SetupDapperCrudWithHybridCacheDecorator(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IRepository<Entity>, HybridCachingRepository<Entity>>(provider =>
            new HybridCachingRepository<Entity>(
                CreateDapperRepository(),
                provider.GetRequiredService<HybridCache>()));
    }

    private static DapperRepository CreateDapperRepository()
    {
        Func<IDbConnection> newConnectionCallback = () =>
        {
            var connection = new SqliteConnection("Data Source=test.db");
            connection.Open();
            return connection;
        };
        return new DapperRepository(newConnectionCallback);
    }
}