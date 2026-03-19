using System.Threading.Tasks;
using Microsoft.Playwright;

namespace ParabankTestAutomation.Tests.Pages
{
    public class ProductsPage : BasePage
    {
        public string ProductsLink => "a[href='http://www.parasoft.com/jsp/products.jsp']";
        public string ProductTitle => ".border-blue300"; 
        public string ProductsList => ".grid-cols-1"; 
        public string FunctionalTestButton => "button[data-tab='2']";
        public string SOAtestProduct => "a[href='https://www.parasoft.com/products/parasoft-soatest/']";
        public ProductsPage(IPage page) : base(page) { }

        public async Task NavigateToProductsAsync()
        {
            var productsLink = Page.Locator(ProductsLink).Nth(0);
            await productsLink.ClickAsync();
        }

        public async Task<string> GetProductTitleAsync()
        {
            var productTitle = Page.Locator(ProductTitle).Nth(0);
            return (await productTitle.TextContentAsync())?.Trim();
        }

        public async Task<bool> IsProductsListVisibleAsync()
        {
            return await IsVisibleAsync(ProductsList);
        }

        public async Task ClickFunctionalTestButtonAsync()
        {
            await WaitForVisibleAsync(FunctionalTestButton);
            await ClickAsync(FunctionalTestButton);
        }

        public async Task ClicksOnSOAtestProductAsync()
        {
           var soatestLink = Page.Locator(SOAtestProduct).Nth(1);
           await soatestLink.ClickAsync();
        }
    }
}