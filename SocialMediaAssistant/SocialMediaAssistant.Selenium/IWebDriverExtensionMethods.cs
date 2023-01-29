using OpenQA.Selenium.Support.UI;

// NOTE: dropping this into this namespace for ease of access
namespace OpenQA.Selenium
{
    public static class IWebDriverExtensionMethods
    {
        public static PageSourceContainsResult WaitForPageSourceContains(
            this IWebDriver driver,
            string textToFind,
            TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout);

            string? pageSource = null;
            var foundIndex = -1;
            var success = wait.Until(x => // anonymous delegate (google this later)
            {
                pageSource = x.PageSource;
                foundIndex = pageSource.IndexOf(textToFind, StringComparison.Ordinal);
                return foundIndex != -1; // -1 means it was not found, so keep waiting
            });

            return new PageSourceContainsResult(
                textToFind,
                foundIndex == -1 ? null : pageSource,
                foundIndex);
        }
    }
}
