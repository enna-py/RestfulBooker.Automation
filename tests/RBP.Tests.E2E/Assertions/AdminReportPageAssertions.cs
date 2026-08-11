using RBP.Business.Ui.Pages.Admin;
using RBP.Data.DTO.Booking;
using RestfulBooker.Core.Logging;
using static Microsoft.Playwright.Assertions;

namespace RBP.Tests.E2E.Assertions;

public static class AdminReportPageAssertions
{
    public static async Task ShouldContainBookingEvent(
    this AdminReportPage page,
    GuestDto guest,
    int roomNumber)
    {
        LoggerManager.Logger.Information(
            "Verify booking event is visible in Admin report");

        await Expect(page.BookingEvent($"{guest.FirstName} {guest.LastName}", roomNumber))
            .ToBeVisibleAsync();
    }
}
