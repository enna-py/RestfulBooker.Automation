using Serilog;

namespace RestfulBooker.Core.Logging;

public static class LoggerManager
{
    private static ILogger _logger = Log.Logger;

    public static bool IsInitialized { get; private set; }

    public static ILogger Logger => _logger;

    public static void Initialize()
    {
        if (IsInitialized)
        {
            return;
        }

        _logger = LoggerFactory.CreateLogger();
        Log.Logger = _logger;

        IsInitialized = true;
    }

    public static void Dispose()
    {
        Log.CloseAndFlush();

        _logger = Log.Logger;
    }
}