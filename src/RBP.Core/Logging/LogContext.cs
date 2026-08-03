namespace RestfulBooker.Core.Logging;

public static class LogContext
{
    public static string TestName { get; internal set; } = string.Empty;

    public static string CorrelationId { get; internal set; } = string.Empty;
}