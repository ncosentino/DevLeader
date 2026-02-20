# Dependency Injection in .NET

Dependency Injection (DI) is a design pattern where objects receive their dependencies from an external source rather than creating them internally. In .NET, the built-in DI container is available via Microsoft.Extensions.DependencyInjection.

The three main service lifetimes in .NET DI are Singleton, Scoped, and Transient. Singleton services are created once per application lifetime. Scoped services are created once per request (or scope). Transient services are created every time they are requested.

## Registering Services

Services are registered in Program.cs using the IServiceCollection API. Common registration methods include AddSingleton, AddScoped, and AddTransient.

```csharp
builder.Services.AddSingleton<IMyService, MyService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
```

## Constructor Injection

The preferred approach in .NET is constructor injection. The DI container resolves all constructor parameters automatically when building the service.

```csharp
public class OrderService : IOrderService
{
    private readonly IEmailSender _emailSender;

    public OrderService(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }
}
```

## When to Use Each Lifetime

Use Singleton for stateless services that are expensive to create, such as HttpClient or caching services. Use Scoped for services that need per-request state, such as database contexts. Use Transient for lightweight, stateless services.

Captive dependency is a common mistake: a Singleton that depends on a Scoped service will hold a stale reference. .NET's built-in container validates this in development mode.
