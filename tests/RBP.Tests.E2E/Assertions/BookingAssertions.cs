using AwesomeAssertions;
using RBP.Data.DTO.Booking;

namespace RBP.Tests.E2E.Assertions;

public static class BookingAssertions
{
    public static void ShouldContainBooking(
        this IEnumerable<BookingDto> bookings,
        BookingRequest request)
    {
        bookings.Should().Contain(b =>
            b.FirstName == request.Guest.FirstName &&
            b.LastName == request.Guest.LastName &&
            b.BookingDates.CheckIn == request.CheckIn &&
            b.BookingDates.CheckOut == request.CheckOut);
    }

    public static void ShouldMatch(this BookingDto actual, BookingDto expected)
    {
        actual.Should().BeEquivalentTo(expected, options => options.ComparingByMembers<BookingDto>());
    }

    public static void ShouldMatch(this BookingDto actual, BookingRequest expected, int bookingId)
    {
        BookingDto expectedBooking = new()
        {
            BookingId = bookingId,
            RoomId = expected.RoomId,
            FirstName = expected.Guest.FirstName,
            LastName = expected.Guest.LastName,
            DepositPaid = true,
            BookingDates = new BookingDatesDto
            {
                CheckIn = expected.CheckIn,
                CheckOut = expected.CheckOut
            }
        };

        actual.ShouldMatch(expectedBooking);
    }
}
