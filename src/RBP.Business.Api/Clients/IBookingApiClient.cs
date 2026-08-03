using RBP.Data.DTO.Booking;

namespace RBP.Business.Api.Clients;

public interface IBookingApiClient
{
    Task<IReadOnlyCollection<BookingDto>> GetBookingsByRoomAsync(int roomId);
}
