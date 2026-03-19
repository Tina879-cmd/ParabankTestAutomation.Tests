using Microsoft.Playwright;
using ParabankTestAutomation.Tests.Pages;

namespace ParabankTestAutomation.Tests.Utilities
{
    public class PageObjectFactory
    {
        private readonly IPage _page;

        public PageObjectFactory(IPage page)
        {
            _page = page;
        }
        public LoginPage LoginPage => new LoginPage(_page);
        public RegisterPage RegisterPage => new RegisterPage(_page);
        public ProductsPage ProductsPage => new ProductsPage(_page);
    }
}
