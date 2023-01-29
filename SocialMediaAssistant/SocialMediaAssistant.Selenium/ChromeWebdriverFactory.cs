using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SocialMediaAssistant.Selenium
{
    public sealed class ChromeWebdriverFactory
    {
        public IWebDriver Create()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddArguments("headless");
            chromeOptions.AddArgument("--silent");
            chromeOptions.AddArgument("log-level=3");

            IWebDriver webDriver = new ChromeDriver(chromeOptions);
            return webDriver;
        }
    }
}