using Autofac;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

AutofacExample();
ServiceCollectionExample();

static void AutofacExample()
{
    ContainerBuilder builder = new();
    builder
        .Register(ctx =>
        {
            var someValue = "Hello, from Autofac!";
            var myType = new MyType(someValue);
            return myType;
        })
        .SingleInstance();

    using var container = builder.Build();
    using var lifetimeScope = container.BeginLifetimeScope();
    var myInstance = lifetimeScope.Resolve<MyType>();

    Console.WriteLine(myInstance.SomeValue);
}

static void ServiceCollectionExample()
{
    ServiceCollection services = new();
    services.Add(new ServiceDescriptor(
        typeof(MyType),
        provider =>
        {
            var someValue = "Hello, from IServiceCollection!";
            var myType = new MyType(someValue);
            return myType;
        },
        ServiceLifetime.Singleton));

    var serviceProvider = services.BuildServiceProvider();
    var myInstance = serviceProvider.GetRequiredService<MyType>();

    Console.WriteLine(myInstance.SomeValue);
}

public sealed class MyType(
    string _someValue)
{
    public string SomeValue { get; } = _someValue;
}