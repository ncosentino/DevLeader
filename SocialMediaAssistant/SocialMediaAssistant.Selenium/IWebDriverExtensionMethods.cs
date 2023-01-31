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

        public static IWebElement? WaitForElementId(
            this IWebDriver driver,
            string elementId,
            TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout);

            IWebElement? element = null;
            var success = wait.Until(x =>
            {
                element = driver.FindElements(By.Id(elementId)).FirstOrDefault();
                return element != null;
            });

            return element;
        }

        public static IWebElement? WaitForElementByXPath(
            this IWebDriver driver,
            string xpath,
            TimeSpan timeout)
        {
            var wait = new WebDriverWait(driver, timeout);

            IWebElement? element = null;
            var success = wait.Until(x =>
            {
                element = driver.FindElements(By.XPath(xpath)).FirstOrDefault();
                return element != null;
            });

            return element;
        }
    }
}
