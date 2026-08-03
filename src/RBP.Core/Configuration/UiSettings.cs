namespace RestfulBooker.Core.Configuration;

public sealed class UiSettings
{
    public string BaseUrl { get; init; } = string.Empty;

    public BrowserType Browser { get; init; }

    public bool Headless { get; init; }

    public TimeSpan Timeout { get; init; }
}