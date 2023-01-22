using Autofac;

namespace ExampleProgram
{
    public sealed class TestModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterFacadeWithDiscoverableSources<MyObjectRepositoryFacade, IRepository>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
