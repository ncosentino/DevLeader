using Autofac;

using CommandLine;

namespace SocialMediaAssistant.Console.Autofac
{
    internal class OptionsModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(c => new CommandLineOptionsProvider(
                    Environment.GetCommandLineArgs(),
                    c.Resolve<Parser>()))
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
