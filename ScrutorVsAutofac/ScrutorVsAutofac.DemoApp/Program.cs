using Autofac;

using Microsoft.Extensions.DependencyInjection;

using ScrutorVsAutofac.SDK;

using System.Reflection;

// FIXME: do more robust checking than this
// naive approach to blindly try and load
// any and all assemblies with no checks!
var assemblies = Directory
    .GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
    .Select(Assembly.LoadFrom)
    .ToArray();

//await AutofacExample(assemblies);
await ServiceCollectionExample(assemblies);

static async Task AutofacExample(Assembly[] assemblies)
{  
    ContainerBuilder builder = new();
    builder.RegisterAssemblyModules(assemblies);
    
    using var container = builder.Build();
    using var scope = container.BeginLifetimeScope();
    
    var myService = scope.Resolve<IMyService>();
    await myService.RunAsync(
        "From AutofacExample...",
        default);
}

static async Task ServiceCollectionExample(Assembly[] assemblies)
{
    ServiceCollection services = new();
    services.Scan(scan => scan
        .FromAssemblies(assemblies)
        .AddClasses()
        .AsSelfWithInterfaces()
        .WithSingletonLifetime());

    var serviceProvider = services.BuildServiceProvider();
    
    var myService = serviceProvider.GetRequiredService<IMyService>();    
    await myService.RunAsync(
        "From ServiceCollectionExample...",
        default);
}

