using RBP.Business.Api.Endpoints;
using RBP.Data.DTO.Booking;
using RestfulBooker.Api.Base;
using RestfulBooker.Api.Factories;
using RestfulBooker.Core.Authentication;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Core.Exceptions;
using RestfulBooker.Data.DTO.Common;
using RestSharp;

namespace RBP.Business.Api.Clients;

public sealed class BookingApiClient : BaseApiClient
{
    public BookingApiClient(AuthenticationState authState)
        : base(ConfigurationService.Current.Api.BookingUrl, authState)
    {
    }

    public async Task<BookingDto> GetBookingAsync(int bookingId)
    {
        ApiResponse<BookingDto> response =
            await GetAsync<BookingDto>(
                $"/{bookingId}",
                validateResponse: true);

        if (response.Data is null)
        {
            throw new ApiException(
                (int)response.StatusCode,
                $"Booking '{bookingId}' was not returned.");
        }

        return response.Data;
    }
}