using Microsoft.Extensions.Configuration;

namespace ParabankTestAutomation.Tests.Config
{
    public static class ConfigReader
    {
        public static AppSettings GetAppSettings()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return config.Get<AppSettings>();
        }
    }
}