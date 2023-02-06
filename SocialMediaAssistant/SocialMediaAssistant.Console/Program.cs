using System.Numerics;
using SocialMediaAssistant.Selenium;

// EXAMPLES
//await GetTikTokFollowersAsync();
//SaveTweetScreenshots();

static async Task GetTikTokFollowersAsync()
{
    var tikTokProfileFetcher = new TikTokSeleniumProfileFetcher();
    var profileInfo = await tikTokProfileFetcher.FetchAsync("devleader");
    Console.WriteLine(profileInfo);
}

static async Task GetInstagramFollowersAsync()
{
    var InstagramProfileFetcher = new InstagramSeleniumProfileFetcher();
    var profileInfo = InstagramProfileFetcher.FetchAsync("dev.leader");
    Console.WriteLine(profileInfo);
}


static void SaveTweetScreenshots()
{
    var webDriverFactory = new ChromeWebdriverFactory();
    var screenshotter = new TweetScreenshotter(webDriverFactory);

    foreach (var screenshot in screenshotter.CreateFromTweetIds(
        new string[]
        {
        "1619544614079467520",
        "1619544747529609216",
        "1619544831688314882",
        "1619544897031372800",
        },
        new Vector3(255, 255, 255)))
    {
        try
        {
            screenshot.Image.Save($"{screenshot.TweetId}.png");
        }
        finally
        {
            screenshot.Dispose();
        }
    }
}