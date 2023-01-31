using System.Drawing;
using System.Numerics;

using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;

using SocialMediaAssistant.Selenium;

public sealed class TweetScreenshotter
{
    private readonly ChromeWebdriverFactory _webDriverFactory;

    public TweetScreenshotter(ChromeWebdriverFactory webdriverFactory)
    {
        _webDriverFactory = webdriverFactory;
    }

    public IEnumerable<TweetScreenshot> CreateFromTweetIds(
        IEnumerable<string> tweetIds,
        Vector3 backgroundRgb)
    {
        using var webDriver = _webDriverFactory.Create();
        webDriver.Manage().Window.Size = new Size(800, 1200);

        var tweetScreenshots = tweetIds
            .Select(x => CreateTweetScreenshot(
                webDriver,
                x))
            .ToArray();
        try
        {
            var maxHeight = tweetScreenshots.Max(x => x.Image.Height);
            // they should all be the same width so this is not ideal to do but...
            // :shrug:
            var maxWidth = tweetScreenshots.Max(x => x.Image.Width);
            var actualAspect = maxWidth / (float)maxHeight;
            var targetAspect = Math.Abs(actualAspect - 1) <= Math.Abs(actualAspect - (4f / 5f))
                ? 1
                : 4f / 5f;

            float targetWidth;
            float targetHeight;
            if (maxWidth > maxHeight)
            {
                targetHeight = maxWidth / targetAspect;
                targetWidth = targetHeight * targetAspect;
            }
            else
            {
                targetWidth = maxHeight * targetAspect;
                targetHeight = targetWidth / targetAspect;
            }

            using var backgroundBrush = new SolidBrush(Color.FromArgb(
                255, 
                (byte)backgroundRgb.X, 
                (byte)backgroundRgb.Y, 
                (byte)backgroundRgb.Z));
            foreach (var tweetScreenshot in tweetScreenshots)
            {
                var verticalOffset = (targetHeight - tweetScreenshot.Image.Height) / 2f;
                var horizontalOffset = (targetWidth - tweetScreenshot.Image.Width) / 2f;

                var scaledTweetImage = new Bitmap((int)targetWidth, (int)targetHeight);
                using (var graphics = Graphics.FromImage(scaledTweetImage))
                {
                    graphics.FillRectangle(backgroundBrush, new Rectangle(0, 0, scaledTweetImage.Width, scaledTweetImage.Height));
                    graphics.DrawImage(
                        tweetScreenshot.Image,
                        new Rectangle((int)horizontalOffset, (int)verticalOffset, tweetScreenshot.Image.Width, tweetScreenshot.Image.Height),
                        new Rectangle(0, 0, tweetScreenshot.Image.Width, tweetScreenshot.Image.Height),
                        GraphicsUnit.Pixel);
                }

                yield return new TweetScreenshot(
                    tweetScreenshot.TweetId,
                    scaledTweetImage);
            }
        }
        finally
        {
            foreach (var tweetImage in tweetScreenshots)
            {
                tweetImage.Dispose();
            }
        }
    }

    private TweetScreenshot CreateTweetScreenshot(
        IWebDriver webDriver,
        string tweetId)
    {
        webDriver.Url = $"https://publish.twitter.com/?hideConversation=on&query=https%3A%2F%2Ftwitter.com%2FDevLeaderCa%2Fstatus%2F{tweetId}&theme=dark&widget=Tweet";

        // no idea why just waiting forthis embedded text isn't good enough....
        //_webDriver.WaitForPageSourceContains(
        //    $"data-tweet-id=\"{tweetId}\"",
        //    TimeSpan.FromSeconds(2));
        //var ele = _webDriver.WaitForElementId(
        //    "WidgetConfigurator-preview",
        //    TimeSpan.FromSeconds(2));
        var ele = webDriver.WaitForElementByXPath(
            "//*[contains(@id, 'twitter-widget-')]",
            TimeSpan.FromSeconds(2));

        // some different ways to scroll...
        //((IJavaScriptExecutor)_webDriver).ExecuteScript("arguments[0].scrollIntoView(true);", ele);
        //((IJavaScriptExecutor)_webDriver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight)");

        Actions actions = new Actions(webDriver);
        actions.MoveToElement(ele);
        actions.Perform();

        // 1 second *seems* to work? please find a way to get rid of this.
        Thread.Sleep(1000);

        // Get entire page screenshot
        var screenshot = ((ITakesScreenshot)webDriver).GetScreenshot();

        // Get the location of element on the page
        Point point = ele.Location;

        // Get width and height of the element
        int eleWidth = ele.Size.Width;
        int eleHeight = ele.Size.Height;

        // Crop the entire page screenshot to get only element screenshot
        var image = System.Drawing.Image.FromStream(new MemoryStream(screenshot.AsByteArray));
        //image.Save("orig.png");

        var verticalScroll = (int)(long)((IJavaScriptExecutor)webDriver).ExecuteScript("return window.pageYOffset;");

        var destImage = new Bitmap(eleWidth, eleHeight);
        using (var graphics = System.Drawing.Graphics.FromImage(destImage))
        {
            var sourceRect = new Rectangle(point.X, point.Y - verticalScroll, eleWidth, eleHeight);
            var destRect = new Rectangle(0, 0, eleWidth, eleHeight);
            graphics.DrawImage(image, destRect, sourceRect, GraphicsUnit.Pixel);
        }

        //destImage.Save($"before-aspect-{tweetId}.png");

        return new (
            tweetId,
            destImage);
    }
}