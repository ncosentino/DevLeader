using System.Numerics;
using System.Reflection;

using Autofac;

using SocialMediaAssistant.Console;
using SocialMediaAssistant.Selenium;

var containerBuilder = new ContainerBuilder();
containerBuilder.RegisterAssemblyModules(
    Directory.GetFiles(
        AppDomain.CurrentDomain.BaseDirectory,
        "*SocialMediaAssistant.*dll")
    //.Concat(Directory.GetFiles(
    //    AppDomain.CurrentDomain.BaseDirectory,
    //    "SocialMediaAssistant.*exe"))
    .Select(x => Assembly.LoadFrom(x))
    .ToArray());
using var container = containerBuilder.Build();
using var scope = container.BeginLifetimeScope();

//SaveTweetScreenshots(
//    scope.Resolve<TweetScreenshotter>(),
//    "DevLeaderCA",
//    new[]
//    {
//        "1625516458699821056",
//        "1625516459719028738",
//        "1625516461535145985",
//        "1625516463569375233",
//        "1625516465612005377",
//        "1625516467407179776",
//        "1625516469491752960",
//        "1625516471593103361",
//    });

//var profileFetchers = scope.Resolve<IEnumerable<IProfileFetcher>>();
//await Parallel.ForEachAsync(
//    profileFetchers,
//    async (pf, ct) =>
//    {
//        // FIXME: we will want to choose a pattern of pass in the parameters
//        // on the method, or pass in the config via constructor...
//        // if we do it at constructor time, we would need to rebuild to switch
//        // users. for our planned use cases, this is totally fine. if we ever
//        // wanted to branch out and support multi users, we'd need to either
//        // change that pattern or just reconstruct the fetchers per user.
//        var profile = await pf.FetchAsync("devleader");
//        Console.WriteLine(profile);
//    });

Console.WriteLine("Press enter to exit.");
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


static void SaveTweetScreenshots(
    TweetScreenshotter screenshotter,
    string twitterUserId,
    IReadOnlyList<string> tweetIds)
{
    var outputDirectory = Path.Combine(
        AppDomain.CurrentDomain.BaseDirectory,
        $"twitter-thread-{tweetIds[0]}");
    Directory.CreateDirectory(outputDirectory);

    foreach (var screenshot in screenshotter.CreateFromTweetIds(
        twitterUserId,
        tweetIds,
        new Vector3(255, 255, 255)))
    {
        try
        {
            var fileName = Path.Combine(outputDirectory, $"{screenshot.TweetId}.png");
            screenshot.Image.Save(fileName);
        }
        finally
        {
            screenshot.Dispose();
        }
    }
}