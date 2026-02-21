using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ProductsDbContext>(options =>
    options.UseSqlite("Data Source=products.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ProductsDbContext>();
    db.Database.EnsureCreated();
}

// This is the EXTRACTED Products microservice.
// It has been pulled out from the monolith and now runs independently.
// It owns the Products domain with its own database.

app.MapGet("/products", async (ProductsDbContext db) =>
{
    return await db.Products.ToListAsync();
});

app.MapPost("/products", async (Product product, ProductsDbContext db) =>
{
    db.Products.Add(product);
    await db.SaveChangesAsync();
    return Results.Created($"/products/{product.Id}", product);
});

app.Run();

public class ProductsDbContext : DbContext
{
    public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();
}

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
}
