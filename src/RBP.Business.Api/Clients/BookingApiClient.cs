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

public sealed class BookingApiClient : BaseApiClient, IBookingApiClient
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

    public async Task<ApiResponse<BookingCreationResponse>> CreateBookingAsync(
        BookingRequest request,
        bool validateResponse = true)
    {
        BookingApiRequest body = new()
        {
            RoomId = request.RoomId,
            FirstName = request.Guest.FirstName,
            LastName = request.Guest.LastName,
            DepositPaid = true,
            Email = request.Guest.Email,
            Phone = request.Guest.Phone,
            BookingDates = new BookingDatesDto
            {
                CheckIn = request.CheckIn,
                CheckOut = request.CheckOut
            }
        };

        return await PostAsync<BookingApiRequest, BookingCreationResponse>(
            BookingEndpoints.Bookings,
            body,
            validateResponse);
    }

    public async Task<IReadOnlyCollection<BookingDto>> GetBookingsByRoomAsync(int roomId)
    {
        ApiResponse<BookingListDto> response =
            await GetAsync<BookingListDto>(
                BookingEndpoints.ByRoom(roomId));

        return response.Data?.Bookings ?? [];
    }

    public async Task<ApiResponse<object>> DeleteBookingAsync(
        int bookingId,
        bool validateResponse = true)
    {
        return await DeleteAsync<object>(
            BookingEndpoints.ById(bookingId),
            validateResponse);
    }
}