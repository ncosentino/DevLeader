using CacheExamples.Repositories.Common;
using CacheExamples.Repositories.Examples;
using CacheExamples.Repositories.Examples.EfCrudGeneric;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Caching.Memory;

public static class BuilderExtensionsEfCrudGeneric
{
    public static void SetupEfCrudGeneric(
        this WebApplicationBuilder builder)
    {
        builder.SetupEfCoreDataSource();
        builder.Services.AddSingleton<IRepository<Entity>, EfCoreRepository<Entity>>();
    }

    public static void SetupEfCrudWithCache(
        this WebApplicationBuilder builder)
    {
        builder.SetupEfCoreDataSource();
        builder.Services.AddSingleton<IRepository<Entity>, CachingEfCoreRepository<Entity>>();
    }

    public static void SetupEfCrudWithCacheDecorator(
        this WebApplicationBuilder builder)
    {
        builder.SetupEfCoreDataSource();
        builder.Services.AddSingleton<EfCoreRepository<Entity>>();
        builder.Services.AddSingleton<IRepository<Entity>, CachingRepository<Entity>>(provider =>
            new CachingRepository<Entity>(
                provider.GetRequiredService<EfCoreRepository<Entity>>(),
                provider.GetRequiredService<IMemoryCache>()));
    }

    public static void SetupEfCrudWithHybridCacheDecorator(
        this WebApplicationBuilder builder)
    {
        builder.SetupEfCoreDataSource();
        builder.Services.AddSingleton<EfCoreRepository<Entity>>();
        builder.Services.AddSingleton<IRepository<Entity>, HybridCachingRepository<Entity>>(provider =>
            new HybridCachingRepository<Entity>(
                provider.GetRequiredService<EfCoreRepository<Entity>>(),
                provider.GetRequiredService<HybridCache>()));
    }

    private static void SetupEfCoreDataSource(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContextFactory<AppDbContext>(options =>
        {
            options.UseSqlite("Data Source=test.db");
        });
    }
}