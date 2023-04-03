using Xunit;

namespace RefactoringCompositionDITesting.UrlParsingUnitTests
{
    public sealed class UrlParsingUnitTests
    {
        private readonly HtmlUrlExtractor _htmlUrlExtractor;


        public UrlParsingUnitTests()
        {
            _htmlUrlExtractor = new HtmlUrlExtractor();
        }

        [Fact]
        private void MethodName_Scenario_Expectation()
        {
            var urls = _htmlUrlExtractor
                .BuildUrlListFromHtml(
                """
                <body>
                    <a href='https://www.someurl.com'/>
                </body>
                """);
            var url = Assert.Single(urls);
            Assert.Equal(
                "https://www.someurl.com",
                url);
        }

        [Fact]
        private void BuildUrlListFromHtml_NoHrefQuotes_SingleUrl()
        {
            var urls = _htmlUrlExtractor
                .BuildUrlListFromHtml(
                """
                class="menupop with-avatar"><a
                class=ab-item aria-haspopup=true 
                href=https://www.devleader.ca/wp-admin/profile.php>
                Howdy, <span
                """);
            var url = Assert.Single(urls);
            Assert.Equal(
                "https://www.devleader.ca/wp-admin/profile.php",
                url);
        }
    }
}