using Microsoft.Extensions.Configuration;

namespace Task8_WPF.DAL.ConnectionSettings;

public static class ConfigurationHelper
{
    public static IConfigurationRoot GetConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("connectionsettings.json", optional: false)
            .Build();
    }

    public static string GetConnectionString(string name = "DefaultConnection")
    {
        return GetConfiguration().GetConnectionString(name);
    }
}
