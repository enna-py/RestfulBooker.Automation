using System.Text.Json.Serialization;

namespace RBP.Data.DTO.Booking;

public sealed class BookingListDto
{
    [JsonPropertyName("bookings")]
    public List<BookingDto> Bookings { get; init; } = [];
}
