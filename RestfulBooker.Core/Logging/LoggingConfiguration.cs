using RestfulBooker.Core.Configuration;
using Serilog;
using Serilog.Events;

namespace RestfulBooker.Core.Logging;

public static class LoggingConfiguration
{
    public static LoggerConfiguration Create()
    {
        LoggingSettings settings = ConfigurationService.Current.Logging;

        CreateLogDirectory(settings.LogDirectory);

        LogEventLevel minimumLevel =
            Enum.Parse<LogEventLevel>(settings.MinimumLevel, ignoreCase: true);

        return new LoggerConfiguration()
            .MinimumLevel.Is(minimumLevel)
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(
                path: Path.Combine(settings.LogDirectory, "automation-.log"),
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 30,
                shared: true);
    }

    private static void CreateLogDirectory(string directory)
    {
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }
}