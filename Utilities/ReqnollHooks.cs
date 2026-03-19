using System;
using System.IO;
using System.Threading.Tasks;
using Reqnroll;
using Microsoft.Playwright;
using ParabankTestAutomation.Tests.Config;
using ParabankTestAutomation.Tests.Logging;
using ParabankTestAutomation.Tests.Utilities;

namespace ParabankTestAutomation.Tests
{
    [Binding]
    public class ReqnollHooks : BaseTest
    {
        private readonly ScenarioContext _scenarioContext;
        private bool _screenshotTaken = false;

        public ReqnollHooks(ScenarioContext scenarioContext)
            : base(scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public async Task BeforeScenario()
        {
            var scenarioName = _scenarioContext.ScenarioInfo.Title;
            await SetupAsync(scenarioName);
            _scenarioContext["Page"] = Page;
            _scenarioContext["Logger"] = Logger;
        }

        [AfterStep]
        public async Task AfterStep()
        {
            var stepText = _scenarioContext.StepContext?.StepInfo?.Text ?? "Step";
            bool isStepFailed = _scenarioContext.TestError != null;
            var logLevel = Logger != null ? Logger.GetType().GetProperty("LogLevel")?.GetValue(Logger) : null;

            if (isStepFailed && Page != null && !Page.IsClosed)
            {
                // Take screenshot on failure
                await Logger.ErrorWithScreenshotAsync($"Step failed: {stepText}");
                _screenshotTaken = true;
            }
            else if (Settings.LogLevel == LogLevel.Trace && Page != null && !Page.IsClosed)
            {
                // Take screenshot at every step if Trace
                await Logger.TraceWithScreenshotAsync($"Step: {stepText}");
                _screenshotTaken = true;
            }
        }

        [AfterScenario]
        public async Task AfterScenario()
        {
            // Take screenshot if scenario failed and not already taken
            if (_scenarioContext.TestError != null && Page != null && !Page.IsClosed && !_screenshotTaken)
            {
                await Logger.ErrorWithScreenshotAsync($"Scenario failed: {_scenarioContext.ScenarioInfo.Title}");
            }
            _screenshotTaken = false; // reset for next scenario
            await TearDownAsync();
        }
    }
}