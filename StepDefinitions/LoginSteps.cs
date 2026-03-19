using System.Threading.Tasks;
using Reqnroll;
using ParabankTestAutomation.Tests.Pages;
using ParabankTestAutomation.Tests.Utilities;
using FluentAssertions;

namespace ParabankTestAutomation.Tests.Steps
{
    [Binding]
    public class LoginSteps : BaseTest
    {
        public LoginSteps(ScenarioContext scenarioContext)
            : base(scenarioContext) {}

        [Given("the user navigates to the login page")]
        public async Task NavigateToLoginPage()
        {
            await PageObjects.LoginPage.NavigateUrl();
        }

        [Given("the user navigates to the Parabank application")]
        public async Task NavigateToParabankApplication()
        {
            await PageObjects.LoginPage.LoginToParabankApplication();
        }

        [When("the user logs in with valid credentials")]
        public async Task ValidCredentialsLogin()
        {
            await PageObjects.LoginPage.LoginWithValidCredentials();
        }

        [When("the user logs in with invalid credentials")]
        public async Task InvalidCredentialsLogin()
        {
            await PageObjects.LoginPage.LoginWithInvalidCredentials();
        }

        [Then("the user should view the title {string}")]
        public async Task AccountOverviewVisible(string expectedTitle)
        {
            var isLoggedIn = await PageObjects.LoginPage.IsLoggedInAsync(expectedTitle);
            isLoggedIn.Should().BeTrue("User should be logged in and see account overview.");
        }

        [Then("the user should view an error message {string}")]
        public async Task ErrorMessageVisible(string expectedMessage)
        {
            var errorMsg = await PageObjects.LoginPage.GetErrorMessageAsync(expectedMessage);
            errorMsg.Should().Be(expectedMessage, "Error message should be shown for invalid login.");
        }
    }
}