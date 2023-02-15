using Autofac;

namespace SocialMediaAssistant.Console.Twitter
{
    internal class TwitterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<TweetScreenshotter>()
                //.AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
