namespace RestfulBooker.Core.Configuration;

public sealed class LoggingSettings
{
    public string LogDirectory { get; init; } = "Logs";

    public string MinimumLevel { get; init; } = "Information";
}