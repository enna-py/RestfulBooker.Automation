namespace RestfulBooker.Core.Configuration;

public sealed class ReportPortalSettings
{
    public string Endpoint { get; init; } = string.Empty;

    public string Project { get; init; } = string.Empty;

    public string Launch { get; init; } = string.Empty;

    public string ApiKey { get; init; } = string.Empty;
}