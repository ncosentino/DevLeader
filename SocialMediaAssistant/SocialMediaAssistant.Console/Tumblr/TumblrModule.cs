using Autofac;

using DontPanic.TumblrSharp;
using DontPanic.TumblrSharp.Client;
using DontPanic.TumblrSharp.OAuth;

namespace SocialMediaAssistant.Console.Tumblr
{
    internal class TumblrModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .Register(c => c
                    .Resolve<IOptionsProvider>()
                    .GetOptions<TumblrOptions>())
                .SingleInstance();
            builder
                .Register(c =>
                {
                    var tumblr = c.Resolve<TumblrOptions>();
                    var accessToken = new Token(
                        tumblr.OauthToken,
                        tumblr.OauthSecret);
                    var tumblrClient = new TumblrClientFactory().Create<TumblrClient>(
                        tumblr.ConsumerKey,
                        tumblr.SecretKey,
                        accessToken);
                    return tumblrClient;
                })
                .SingleInstance();
            builder
                .RegisterType<TumblrProfileFetcher>()
                .AsImplementedInterfaces()
                .SingleInstance();
        }
    }
}
