using Autofac;

using CommandLine;

namespace SocialMediaAssistant.Console.Autofac
{
    internal class CommandLineModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register<Parser>(c => Parser.Default)
                .SingleInstance();
        }
    }
}
