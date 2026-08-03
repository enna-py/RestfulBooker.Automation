using System.Text.Json.Serialization;

namespace RBP.Data.DTO.Booking;

public sealed class BookingDatesDto
{
    [JsonPropertyName("checkin")]
    public DateOnly CheckIn { get; init; }

    [JsonPropertyName("checkout")]
    public DateOnly CheckOut { get; init; }
}