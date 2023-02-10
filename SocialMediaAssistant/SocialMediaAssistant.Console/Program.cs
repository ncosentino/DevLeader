using System.Reflection;

using Autofac;

using SocialMediaAssistant.Console;

var containerBuilder = new ContainerBuilder();
containerBuilder.RegisterAssemblyModules(Assembly.GetExecutingAssembly());
using var container = containerBuilder.Build();
using var scope = container.BeginLifetimeScope();

var profileFetchers = scope.Resolve<IEnumerable<IProfileFetcher>>();
await Parallel.ForEachAsync(
    profileFetchers,
    async (pf, ct) =>
    {
        // FIXME: we will want to choose a pattern of pass in the parameters
        // on the method, or pass in the config via constructor...
        // if we do it at constructor time, we would need to rebuild to switch
        // users. for our planned use cases, this is totally fine. if we ever
        // wanted to branch out and support multi users, we'd need to either
        // change that pattern or just reconstruct the fetchers per user.
        var profile = await pf.FetchAsync("devleader");
        Console.WriteLine(profile);
    });

Console.ReadLine();

//static async Task GetTikTokFollowersAsync()
//{
//    var tikTokProfileFetcher = new TikTokSeleniumProfileFetcher();
//    var profileInfo = await tikTokProfileFetcher.FetchAsync("devleader");
//    Console.WriteLine(profileInfo);
//}

//static async Task GetInstagramFollowersAsync()
//{
//    var InstagramProfileFetcher = new InstagramSeleniumProfileFetcher();
//    var profileInfo = InstagramProfileFetcher.FetchAsync("dev.leader");
//    Console.WriteLine(profileInfo);
//}


//static void SaveTweetScreenshots()
//{
//    var webDriverFactory = new ChromeWebdriverFactory();
//    var screenshotter = new TweetScreenshotter(webDriverFactory);

//    foreach (var screenshot in screenshotter.CreateFromTweetIds(
//        "DevLeaderCA",
//        new string[]
//        {
//        "1619544614079467520",
//        "1619544747529609216",
//        "1619544831688314882",
//        "1619544897031372800",
//        },
//        new Vector3(255, 255, 255)))
//    {
//        try
//        {
//            screenshot.Image.Save($"{screenshot.TweetId}.png");
//        }
//        finally
//        {
//            screenshot.Dispose();
//        }
//    }
//}