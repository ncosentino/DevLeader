using CacheExamples.Repositories.Common;
using CacheExamples.Repositories.Examples.EfCrud;

using Microsoft.EntityFrameworkCore;

public static class BuilderExtensionsEfCrud
{
    public static void SetupEfCrud(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContextFactory<AppDbContext>(options =>
        {
            options.UseSqlite("Data Source=test.db");
        });
        builder.Services.AddSingleton<IEntityRepository, EfCoreRepository>();
    }
}