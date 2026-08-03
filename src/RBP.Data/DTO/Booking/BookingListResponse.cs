namespace RBP.Data.DTO.Booking;

public sealed class BookingListResponse
{
    public IReadOnlyCollection<BookingListDto> Bookings { get; init; } = [];
}