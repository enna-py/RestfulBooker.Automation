using RBP.Business.Ui.Pagesl;
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
}
