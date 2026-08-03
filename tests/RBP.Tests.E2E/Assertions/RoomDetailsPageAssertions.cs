using AwesomeAssertions;
using RBP.Business.Ui.Pagesl;
using RBP.Data.DTO.Booking;
using RestfulBooker.Core.Logging;

namespace RBP.Core.Assertion;

public static class RoomDetailsPageAssertions
{
    public static async Task ShouldHaveSuccessfulBooking(
        this RoomDetailsPage page)
    {
        LoggerManager.Logger.Information(
            "Verifying successful message");

        string confirmation =
            await page.GetConfirmationTextAsync();

        confirmation.Should().Contain("Booking Confirmed");
    }

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
}
