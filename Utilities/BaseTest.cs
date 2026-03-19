using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Playwright;
using ParabankTestAutomation.Tests.Config;
using ParabankTestAutomation.Tests.Logging;
using Reqnroll;

namespace ParabankTestAutomation.Tests.Utilities
{
    public abstract class BaseTest
    {
        protected IPlaywright Playwright { get; private set; }
        protected ScenarioContext ScenarioContext { get; }
        protected IBrowser Browser { get; private set; }
        protected IPage Page { get; private set; }
        protected Logger Logger { get; private set; }
        protected AppSettings Settings { get; private set; }
        public PageObjectFactory PageObjects { get; }
        private string _traceFilePath;

        public async Task SetupAsync(string scenarioName = null)
        {
            Settings = ConfigReader.GetAppSettings();
            Playwright = await Microsoft.Playwright.Playwright.CreateAsync();
            Browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = Settings.Headless // Uses value from appsettings.json
            });
            Page = await Browser.NewPageAsync();

            // Start Playwright tracing for HTML report
            await Page.Context.Tracing.StartAsync(new TracingStartOptions
            {
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });

            var safeScenarioName = string.Join("_", (scenarioName ?? "DefaultScenario").Split(Path.GetInvalidFileNameChars()));
            var logFileName = $"Logs/{safeScenarioName}_{DateTime.Now:yyyyMMdd_HHmmssfff}.txt";
            Logger = new Logger(Settings.LogLevel, Page, scenarioName, logFileName);

            // Prepare trace file path
            var tracesDir = Path.Combine(Directory.GetCurrentDirectory(), "Traces");
            Directory.CreateDirectory(tracesDir);
            _traceFilePath = Path.Combine(tracesDir, $"{safeScenarioName}_{DateTime.Now:yyyyMMdd_HHmmssfff}_trace.zip");
        }

        public BaseTest(ScenarioContext scenarioContext)
        {
            ScenarioContext = scenarioContext;
            Page = scenarioContext.ContainsKey("Page") ? scenarioContext["Page"] as IPage : null;
            Logger = scenarioContext.ContainsKey("Logger") ? scenarioContext["Logger"] as Logger : null;
            Settings = ConfigReader.GetAppSettings();
            PageObjects = new PageObjectFactory(Page);
        }

        public async Task TearDownAsync()
        {
            // Stop tracing and save the trace file
            if (Page?.Context != null && !string.IsNullOrEmpty(_traceFilePath))
            {
                await Page.Context.Tracing.StopAsync(new TracingStopOptions
                {
                    Path = _traceFilePath
                });
            }
            
            Logger?.Dispose();
            if (Browser != null) await Browser.CloseAsync();
            Playwright?.Dispose();
        }
    }
}