using AwesomeAssertions;
using RBP.Data.DTO.Booking;
using RestfulBooker.Data.DTO.Common;
using System.Net;

namespace RestfulBooker.Tests.Assertions;

public static class BookingApiAssertions
{
    public static void ShouldBeCreatedSuccessfully(
        this ApiResponse<BookingCreationResponse> response)
    {
        response.IsSuccessful.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data!.BookingId.Should().BeGreaterThan(0);
    }

    public static void ShouldBeRejectedAsConflict(
        this ApiResponse<BookingCreationResponse> response)
    {
        response.IsSuccessful.Should().BeFalse();
        response.StatusCode.Should().Be(HttpStatusCode.Conflict);
    }

    public static void ShouldNotContainBooking(
        this IEnumerable<BookingDto> bookings,
        BookingRequest request)
    {
        bookings.Should().NotContain(b =>
            b.RoomId == request.RoomId &&
            b.BookingDates.CheckIn == request.CheckIn &&
            b.BookingDates.CheckOut == request.CheckOut);
    }
}
