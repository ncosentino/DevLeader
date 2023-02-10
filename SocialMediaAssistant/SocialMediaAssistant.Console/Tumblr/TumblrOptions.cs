using CommandLine;

namespace SocialMediaAssistant.Console.Tumblr
{
    public class TumblrOptions
    {
        [Option("TUMBLR_CONSUMER_KEY", Required = true)]
        public string ConsumerKey { get; set; }

        [Option("TUMBLR_SECRET_KEY", Required = true)]
        public string SecretKey { get; set; }

        [Option("TUMBLR_OAUTH_TOKEN", Required = true)]
        public string OauthToken { get; set; }

        [Option("TUMBLR_OAUTH_SECRET", Required = true)]
        public string OauthSecret { get; set; }
    };
}