namespace RestfulBooker.Core.Configuration;

public static class ConfigurationProvider
{
    public static AppConfig Current { get; internal set; } = new();
}