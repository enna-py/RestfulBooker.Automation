using System.Text.Json.Serialization;

namespace RBP.Data.DTO.Booking;

public sealed class BookingApiRequest
{
    [JsonPropertyName("roomid")]
    public int RoomId { get; init; }

    [JsonPropertyName("firstname")]
    public string FirstName { get; init; } = string.Empty;

    [JsonPropertyName("lastname")]
    public string LastName { get; init; } = string.Empty;

    [JsonPropertyName("depositpaid")]
    public bool DepositPaid { get; init; }

    [JsonPropertyName("email")]
    public string Email { get; init; } = string.Empty;

    [JsonPropertyName("phone")]
    public string Phone { get; init; } = string.Empty;

    [JsonPropertyName("bookingdates")]
    public BookingDatesDto BookingDates { get; init; } = new();
}
