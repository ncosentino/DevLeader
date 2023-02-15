using Autofac;

namespace SocialMediaAssistant.Selenium
{
    internal class SeleniumModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ChromeWebdriverFactory>()
                //.AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
