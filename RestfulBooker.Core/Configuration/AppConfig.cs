namespace RestfulBooker.Core.Configuration;

public sealed class AppConfig
{
    public ApiSettings Api { get; init; } = new();

    public UiSettings Ui { get; init; } = new();

    public CredentialsSettings Credentials { get; init; } = new();

    public LoggingSettings Logging { get; init; } = new();

    public ReportPortalSettings ReportPortal { get; init; } = new();
}