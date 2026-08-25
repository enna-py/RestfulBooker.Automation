namespace RestfulBooker.Core.Configuration;

public sealed class ApiSettings
{
    public string RoomUrl { get; init; } = string.Empty;

    public string AuthUrl { get; init; } = string.Empty;

    public string BookingUrl { get; init; } = string.Empty;

    public string MessageUrl { get; init; } = string.Empty;

    public string BrandingUrl { get; init; } = string.Empty;
}