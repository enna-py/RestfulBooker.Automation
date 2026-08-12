using RBP.Data.DTO.Booking;
using RestfulBooker.Data.Builders.Booking;
using RestfulBooker.Data.DTO.Common;
using RestfulBooker.Tests.API;
using RestfulBooker.Tests.Assertions;

namespace RestfulBooker.Tests.Smoke.Api.Booking;

[TestFixture]
public class PreventDoubleBookingFixture : BaseApiFixture
{
    [Test]
    [Category("Smoke")]
    [Category("API")]
    [Property("JiraKey", "RBP-10")]
    public async Task Booking_Should_Not_Allow_Overlapping_Dates()
    {
        const int roomId = 3;

        DateOnly checkIn =
            DateOnly.FromDateTime(DateTime.Today.AddDays(Random.Shared.Next(30, 300)));

        DateOnly checkOut = checkIn.AddDays(5);

        BookingRequest firstBooking = new BookingRequestBuilder()
            .WithRoomId(roomId)
            .WithDates(checkIn, checkOut)
            .Build();

        BookingRequest overlappingBooking = new BookingRequestBuilder()
            .WithRoomId(roomId)
            .WithDates(checkIn.AddDays(2), checkOut.AddDays(2))
            .Build();

        ApiResponse<BookingCreationResponse> firstResponse =
            await BookingApiClient.CreateBookingAsync(firstBooking);

        firstResponse.ShouldBeCreatedSuccessfully();

        ApiResponse<BookingCreationResponse> secondResponse =
            await BookingApiClient.CreateBookingAsync(
                overlappingBooking,
                validateResponse: false);

        secondResponse.ShouldBeRejectedAsConflict();

        IReadOnlyCollection<BookingDto> roomBookings =
            await BookingApiClient.GetBookingsByRoomAsync(roomId);

        roomBookings.ShouldNotContainBooking(overlappingBooking);
    }
}
