using Serilog;

namespace RestfulBooker.Core.Logging;

public static class LoggerFactory
{
    public static ILogger CreateLogger()
    {
        return LoggingConfiguration
            .Create()
            .CreateLogger();
    }
}