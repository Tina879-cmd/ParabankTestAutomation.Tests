using FluentAssertions;
using Microsoft.Playwright;
using ParabankTestAutomation.Tests.Config;

namespace ParabankTestAutomation.Tests.Pages
{
    public class LoginPage : BasePage
    {
        private readonly AppSettings _settings;

        // Locators
        public string Username => "input[name='username']";
        public string Password => "input[name='password']";
        public string LoginButton => "input[type='submit']";
        public string ErrorMessage => ".error";
        public string AccountOverviewTitle => ".title";

        public LoginPage(IPage page)
            : base(page)
        {
            _settings = ConfigReader.GetAppSettings();
        }

        public (string url, string username, string password) GetLoginConfig()
        {
            return (_settings.BaseUrl, _settings.Username, _settings.Password);
        }

        public async Task NavigateUrl()
        {
            var (url, _, _) = GetLoginConfig();
            await Page.GotoAsync(url);
        }

        public async Task LoginWithValidCredentials()
        {
            var (_, username, password) = GetLoginConfig();
            await WaitForVisibleAsync(Username);
            await ClickAsync(Username);
            await FillAsync(Username, username);
            await Page.Locator(Username).PressAsync("Tab");
            await FillAsync(Password, password);
            await Page.Locator(Password).PressAsync("Tab");
            await WaitForVisibleAsync(LoginButton);
            await ClickAsync(LoginButton);
        }

        public async Task LoginWithInvalidCredentials()
        {
            var (_, username, _) = GetLoginConfig();
            string invalidPassword = "invalidPassword123!";
            await WaitForVisibleAsync(Username);
            await ClickAsync(Username);
            await FillAsync(Username, username);
            await Page.Locator(Username).PressAsync("Tab");
            await FillAsync(Password, invalidPassword);
            await Page.Locator(Password).PressAsync("Tab");
            await WaitForVisibleAsync(LoginButton);
            await ClickAsync(LoginButton);
        }

        public async Task<bool> IsLoggedInAsync(string expectedTitle)
        {
            var actualTitle = (await Page.Locator(AccountOverviewTitle).Nth(0).TextContentAsync())?.Trim();
            return actualTitle.Equals(expectedTitle, StringComparison.OrdinalIgnoreCase);
        }

        public async Task LoginToParabankApplication()
        {
            await NavigateUrl();
            await LoginWithValidCredentials();
            var isLoggedIn = await IsLoggedInAsync("Accounts Overview");
            isLoggedIn.Should().BeTrue("User should be logged in and see account overview.");
        }

        public async Task<string> GetErrorMessageAsync(string expectedMessage)
        {
            var errorMessage = await GetTextAsync(ErrorMessage);
            return errorMessage;
        }
    }
}
