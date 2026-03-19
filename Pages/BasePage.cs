using Microsoft.Playwright;
using ParabankTestAutomation.Tests.Logging;

namespace ParabankTestAutomation.Tests.Pages
{
    public abstract class BasePage
    {
        protected const int DefaultTimeoutMs = 10000;
        protected readonly IPage _page;
        public IPage Page => _page;
        protected readonly Logger _logger; 

        protected BasePage(IPage page, Logger logger = null)
        {
            _page = page;
            _logger = logger;
        }
        
        public async Task ClickAsync(string selector)
        {
            await Page.ClickAsync(selector);
        }

        public async Task FillAsync(string selector, string value)
        {
            await Page.FillAsync(selector, value);
        }

        public async Task FillAsync(string selector, int value)
        {
            await Page.FillAsync(selector, value.ToString());
        }

        public async Task<bool> IsVisibleAsync(string selector)
        {
            return await Page.IsVisibleAsync(selector);
        }

        public async Task<string> GetTextAsync(string selector)
        {
            return await Page.TextContentAsync(selector);
        }

        // Common timeout for locator waits
        public async Task WaitForVisibleAsync(string selector, int? timeoutMs = null)
        {
            await Page.Locator(selector).WaitForAsync(new LocatorWaitForOptions
            {
                State = WaitForSelectorState.Visible,
                Timeout = timeoutMs ?? DefaultTimeoutMs
            });
        }

        public async Task<bool> RetryAsync(Func<Task> action, int maxRetries = 3, int delayMs = 500)
        {
            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    await action();
                    return true;
                }
                catch (Exception ex)
                {
                    _logger?.Error(ex, $"Retry {attempt} failed.");
                    if (attempt == maxRetries)
                        throw;
                    await Task.Delay(delayMs);
                }
            }
            return false;
        }
    }
}
