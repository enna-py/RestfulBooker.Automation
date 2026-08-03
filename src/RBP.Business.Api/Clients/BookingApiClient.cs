using RBP.Business.Api.Endpoints;
using RBP.Data.DTO.Booking;
using RestfulBooker.Api.Base;
using RestfulBooker.Api.Factories;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Core.Exceptions;
using RestfulBooker.Data.DTO.Common;
using RestSharp;

namespace RBP.Business.Api.Clients;

public sealed class BookingApiClient : BaseApiClient
{
    public BookingApiClient()
        : base(ConfigurationService.Current.Api.BookingUrl)
    {
    }

    public async Task<IReadOnlyCollection<BookingDto>> GetBookingsByRoomAsync(int roomId)
    {
        ApiResponse<BookingListDto> response =
            await GetAsync<BookingListDto>("/", validateResponse: true);

        if (response.Data is null)
        {
            throw new ApiException(
                (int)response.StatusCode,
                "Booking list was not returned.");
        }

        return response.Data.Bookings
            .Where(b => b.RoomId == roomId)
            .ToList();
    }
}