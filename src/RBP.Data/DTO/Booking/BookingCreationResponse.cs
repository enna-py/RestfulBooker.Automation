using System.Text.Json.Serialization;

namespace RBP.Data.DTO.Booking;

public sealed class BookingCreationResponse
{
    [JsonPropertyName("bookingid")]
    public int BookingId { get; init; }

    [JsonPropertyName("booking")]
    public BookingDto Booking { get; init; } = new();
}
