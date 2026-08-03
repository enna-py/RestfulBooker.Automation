namespace RBP.Data.DTO.Booking;
public sealed class BookingRequest
{
    public int RoomId { get; init; }

    public GuestDto Guest { get; init; } = new();

    public DateOnly CheckIn { get; init; }

    public DateOnly CheckOut { get; init; }
}
