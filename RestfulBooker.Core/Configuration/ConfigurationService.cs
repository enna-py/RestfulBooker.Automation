using Microsoft.Extensions.Configuration;

namespace RestfulBooker.Core.Configuration;

public static class ConfigurationService
{
    public static AppConfig Current { get; private set; } = null!;

    public static void Initialize()
    {
        IConfiguration configuration = BuildConfiguration();

        AppConfig appConfig = BindConfiguration(configuration);

        ValidateConfiguration(appConfig);

        Current = appConfig;
    }

    private static IConfiguration BuildConfiguration()
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false)
            .AddJsonFile("appsettings.local.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
    }

    private static AppConfig BindConfiguration(IConfiguration configuration)
    {
        return configuration.Get<AppConfig>()
            ?? throw new ConfigurationException(
                "Unable to bind application configuration.");
    }

    private static void ValidateConfiguration(AppConfig config)
    {
        ValidateApi(config.Api);
        ValidateUi(config.Ui);
        ValidateCredentials(config.Credentials);
        ValidateLogging(config.Logging);
        //ValidateReportPortal(config.ReportPortal);
    }

    private static void ValidateApi(ApiSettings api)
    {
        RequireValidUri(api.RoomUrl,
            $"{nameof(ApiSettings)}.{nameof(ApiSettings.RoomUrl)}");

        RequireValidUri(api.AuthUrl,
            $"{nameof(ApiSettings)}.{nameof(ApiSettings.AuthUrl)}");

        RequireValidUri(api.BookingUrl,
            $"{nameof(ApiSettings)}.{nameof(ApiSettings.BookingUrl)}");
    }

    private static void ValidateUi(UiSettings ui)
    {
        RequireValidUri(ui.BaseUrl,
            $"{nameof(UiSettings)}.{nameof(UiSettings.BaseUrl)}");

        RequirePositive(ui.Timeout,
            $"{nameof(UiSettings)}.{nameof(UiSettings.Timeout)}");
    }

    private static void ValidateCredentials(CredentialsSettings credentials)
    {
        RequireNotEmpty(credentials.Username,
            $"{nameof(CredentialsSettings)}.{nameof(CredentialsSettings.Username)}");

        RequireNotEmpty(credentials.Password,
            $"{nameof(CredentialsSettings)}.{nameof(CredentialsSettings.Password)}");
    }

    private static void ValidateLogging(LoggingSettings logging)
    {
        RequireDirectory(logging.LogDirectory,
            $"{nameof(LoggingSettings)}.{nameof(LoggingSettings.LogDirectory)}");
    }

    private static void ValidateReportPortal(ReportPortalSettings reportPortal)
    {
        RequireValidUri(reportPortal.Endpoint,
            $"{nameof(ReportPortalSettings)}.{nameof(ReportPortalSettings.Endpoint)}");

        RequireNotEmpty(reportPortal.Project,
            $"{nameof(ReportPortalSettings)}.{nameof(ReportPortalSettings.Project)}");

        RequireNotEmpty(reportPortal.Launch,
            $"{nameof(ReportPortalSettings)}.{nameof(ReportPortalSettings.Launch)}");

        RequireNotEmpty(reportPortal.ApiKey,
            $"{nameof(ReportPortalSettings)}.{nameof(ReportPortalSettings.ApiKey)}");
    }

    private static void RequireNotEmpty(string? value, string settingName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            ThrowConfigurationException(settingName, "must not be empty.");
        }
    }

    private static void RequirePositive(TimeSpan value, string settingName)
    {
        if (value <= TimeSpan.Zero)
        {
            ThrowConfigurationException(settingName, "must be greater than zero.");
        }
    }

    private static void RequireValidUri(string? value, string settingName)
    {
        RequireNotEmpty(value, settingName);

        if (!Uri.TryCreate(value, UriKind.Absolute, out _))
        {
            ThrowConfigurationException(settingName, "must be a valid absolute URI.");
        }
    }

    private static void RequireDirectory(string? value, string settingName)
    {
        RequireNotEmpty(value, settingName);
    }

    private static void ThrowConfigurationException(string settingName, string message)
    {
        throw new ConfigurationException(
            $"Configuration setting '{settingName}' {message}");
    }
}