using AwesomeAssertions;
using RBP.Business.Ui.Pagesl;
using RBP.Data.DTO.Booking;
using RestfulBooker.Core.Logging;
using static Microsoft.Playwright.Assertions;

namespace RBP.Tests.E2E.Assertions;

public static class RoomDetailsPageAssertions
{
    public static async Task ShouldHaveSuccessfulBooking(
    this RoomDetailsPage page)
    {
        LoggerManager.Logger.Information("Verify booking confirmation");

        await Expect(page.ConfirmationTitle).ToHaveTextAsync("Booking Confirmed");
        await Expect(page.ConfirmationMessage).ToHaveTextAsync("Your booking has been confirmed for the following dates:");
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
