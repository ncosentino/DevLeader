using CacheExamples.Repositories.Common;

using Microsoft.Extensions.Caching.StackExchangeRedis;

using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

var builder = WebApplication.CreateBuilder(args);
builder.SetupDapperCrudWithHybridCacheDecorator();
builder.Services
    .AddFusionCache()
    .WithSerializer(new FusionCacheSystemTextJsonSerializer())
    .WithDistributedCache(new RedisCache(new RedisCacheOptions
    {
        Configuration = builder.Configuration.GetConnectionString("RedisConnectionString")
    })
    ).WithDefaultEntryOptions(opts =>
    {
        opts.SetDurationInfinite();
    })
    .AsHybridCache();

var app = builder.Build();
app.UseHttpsRedirection();

// FIXME: do better with HTTP methods
app.MapGet("/", async (
    IRepository<Entity> repository, 
    int id,
    CancellationToken cancellationToken) =>
{
    var result = await repository.GetAsync(id, cancellationToken);
    return Results.Ok(result);
});
app.MapGet("/create", async (
    IRepository<Entity> repository,
    CancellationToken cancellationToken) =>
{
    Entity entity = new(
        Random.Shared.Next(),
        Guid.NewGuid().ToString("N"));
    await repository.CreateAsync(
        entity,
        cancellationToken);
    return Results.Ok(entity);
});
app.MapGet("/delete", async (
    IRepository<Entity> repository, 
    int id,
    CancellationToken cancellationToken) =>
{
    var deleted = await repository.DeleteAsync(id, cancellationToken);
    return Results.Ok(deleted);
});
app.MapGet("/update", async (
    IRepository<Entity> repository,
    int id, 
    string value,
    CancellationToken cancellationToken) =>
{
    Entity entity = new(id, value);
    var updated = await repository.UpdateAsync(
        entity, 
        cancellationToken);
    return Results.Ok(updated);
});

app.Run();
