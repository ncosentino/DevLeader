using Autofac;

namespace AutofacPlugin1;

internal sealed class MyModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder
            .RegisterType<MyService>()
            .AsImplementedInterfaces()
            .SingleInstance();
    }
}