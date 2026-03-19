using System.Threading.Tasks;
using Reqnroll;
using ParabankTestAutomation.Tests.Pages;
using ParabankTestAutomation.Tests.Utilities;
using FluentAssertions;

namespace ParabankTestAutomation.Tests.StepDefinitions
{
    [Binding]
    public class ProductsSteps : BaseTest
    {
        public ProductsSteps(ScenarioContext scenarioContext)
            : base(scenarioContext) { }


        [When("the user navigates to the Products page")]
        public async Task NavigateToTheProductsPage()
        {
            await PageObjects.ProductsPage.NavigateToProductsAsync();
        }

        [When("the user clicks on Functional Testing button")]
        public async Task ClickOnFunctionalTestingButton()
        {
            await PageObjects.ProductsPage.ClickFunctionalTestButtonAsync();
        }

        [When("the user clicks on the SOAtest product")]
        public async Task ClickSOAtestProduct()
        {
            await PageObjects.ProductsPage.ClicksOnSOAtestProductAsync();
        }

        [Then("the user should be able to view the title {string}")]
        public async Task ViewProductTitle(string title)
        {
            var actualTitle = await PageObjects.ProductsPage.GetProductTitleAsync();
            actualTitle.Should().Be(title, $"Expected product title to be '{title}' but found '{actualTitle}'");
        }

        [Then("the user should be able to view the list of available products")]
        public async Task ViewProductsList()
        {
            var isVisible = await PageObjects.ProductsPage.IsProductsListVisibleAsync();
            isVisible.Should().BeTrue("Products list should be visible.");
        }
    }
}