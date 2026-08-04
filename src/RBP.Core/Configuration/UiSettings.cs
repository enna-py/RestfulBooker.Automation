namespace RestfulBooker.Core.Configuration;

public sealed class UiSettings
{
    public string BaseUrl { get; init; } = string.Empty;

    public TimeSpan Timeout { get; init; }

    public string Browser { get; init; } = "chromium";

    public bool Headless { get; init; }

    public bool Incognito { get; init; }
}