using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace ParabankTestAutomation.Tests.Logging
{
    public class Logger : IDisposable
    {
        private readonly LogLevel _logLevel;
        private readonly IPage _page;
        private readonly string _scenarioName;
        private readonly StreamWriter _fileWriter;

        public Logger(LogLevel logLevel, IPage page = null, string scenarioName = null, string logFilePath = null)
        {
            _logLevel = logLevel;
            _page = page;
            _scenarioName = scenarioName ?? "DefaultScenario";
            if (!string.IsNullOrEmpty(logFilePath))
            {
                var logDir = Path.GetDirectoryName(logFilePath);
                if (!string.IsNullOrEmpty(logDir) && !Directory.Exists(logDir))
                {
                    Directory.CreateDirectory(logDir);
                }
                _fileWriter = new StreamWriter(logFilePath, append: true) { AutoFlush = true };
            }
        }

        private string FormatMessage(string level, string message)
        {
            return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] [{_scenarioName}] {message}";
        }

        public void Trace(string message)
        {
            if (_logLevel <= LogLevel.Trace)
                WriteLog("TRACE", message);
        }

        public void Info(string message)
        {
            if (_logLevel <= LogLevel.Info)
                WriteLog("INFO", message);
        }

        public void Error(string message)
        {
            if (_logLevel <= LogLevel.Error)
                WriteLog("ERROR", message);
        }

        public void Error(Exception ex, string message)
        {
            if (_logLevel <= LogLevel.Error)
                WriteLog("ERROR", $"{message}\n{ex}");
        }

        private void WriteLog(string level, string message)
        {
            var formatted = FormatMessage(level, message);
            Console.WriteLine(formatted);
            _fileWriter?.WriteLine(formatted);
        }

        public async Task TraceWithScreenshotAsync(string message, string screenshotName = null)
        {
            Trace(message);
            if (_page != null && _logLevel == LogLevel.Trace)
            {
                var safeFileName = MakeSafeFileName(screenshotName ?? $"trace_{DateTime.Now:yyyyMMdd_HHmmssfff}.png");
                await TakeScreenshotAsync(safeFileName);
            }
        }

        public async Task InfoWithScreenshotAsync(string message, string screenshotName = null)
        {
            Info(message);
            if (_page != null && (_logLevel == LogLevel.Trace || _logLevel == LogLevel.Info))
            {
                var safeFileName = MakeSafeFileName(screenshotName ?? $"info_{DateTime.Now:yyyyMMdd_HHmmssfff}.png");
                await TakeScreenshotAsync(safeFileName);
            }
        }

        public async Task ErrorWithScreenshotAsync(string message, string screenshotName = null)
        {
            Error(message);
            if (_page != null)
            {
                var safeFileName = MakeSafeFileName(screenshotName ?? $"error_{DateTime.Now:yyyyMMdd_HHmmssfff}.png");
                await TakeScreenshotAsync(safeFileName);
            }
        }

        private async Task TakeScreenshotAsync(string fileName)
        {
            var screenshotsDir = Path.Combine(Directory.GetCurrentDirectory(), "Screenshots");
            Directory.CreateDirectory(screenshotsDir);
            var filePath = Path.Combine(screenshotsDir, fileName);
            await _page.ScreenshotAsync(new PageScreenshotOptions { Path = filePath });
            var msg = $"[Screenshot] Saved to {filePath}";
            Console.WriteLine(msg);
            _fileWriter?.WriteLine(msg);
        }

        private static string MakeSafeFileName(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                name = name.Replace(c, '_');
            }
            // Also replace double quotes specifically
            name = name.Replace("\"", "_");
            return name;
        }

        public void Dispose()
        {
            _fileWriter?.Dispose();
        }
    }
}