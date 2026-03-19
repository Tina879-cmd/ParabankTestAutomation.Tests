using ParabankTestAutomation.Tests.Logging;

namespace ParabankTestAutomation.Tests.Config
{
    public class AppSettings
    {
        public string BaseUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public LogLevel LogLevel { get; set; }
        public bool Headless { get; set; } 
    }
}