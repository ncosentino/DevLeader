using Autofac;
using Autofac.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder
    .Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory(cb =>
    {
        cb.RegisterAssemblyModules(typeof(Program).Assembly);
    }))
    .ConfigureServices(services => services.AddAutofac());


var app = builder.Build();
app.UseHttpsRedirection();

app.MapGet("/", ([FromServices]IMessageFormatter formatter) =>
{
    var result = formatter.FormatLogMessage("Hello World");
    return result;
});

app.Run();



public interface IMessageFormatter
{
    string FormatLogMessage(string message);
}

internal sealed class StandardLogMessageFormatter 
    : IMessageFormatter
{
    public string FormatLogMessage(string message)
        => message;
}

internal sealed class LoggingModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<StandardLogMessageFormatter>()
            .AsImplementedInterfaces()
            .SingleInstance();
    }
}

internal sealed class DecoratorModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterDecorator<
            DecoratedMessageFormatter, 
            IMessageFormatter>();
    }
}

internal sealed class DecoratedMessageFormatter
    : IMessageFormatter
{
    private readonly IMessageFormatter _inner;

    public DecoratedMessageFormatter(IMessageFormatter inner)
    {
        _inner = inner;
    }

    public string FormatLogMessage(string message)
    {
        var result = $"DECORATED: {_inner.FormatLogMessage(message)}";
        return result;
    }
}