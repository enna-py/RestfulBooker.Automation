using Microsoft.Playwright;
using RBP.Business.Ui.Pages.Admin;
using RBP.Data.DTO.Booking;
using RestfulBooker.Core.Logging;
using static Microsoft.Playwright.Assertions;

namespace RBP.Tests.E2E.Assertions;

public static class AdminMessagesPageAssertions
{
    public static async Task ShouldContainBookingNotification(
    this AdminMessagesPage page,
    GuestDto guest)
    {
        LoggerManager.Logger.Information(
            "Verify booking notification exists in Admin messages");

        ILocator message = page.MessageRow($"{guest.FirstName} {guest.LastName}");

        await Expect(message).ToBeVisibleAsync();

        await Expect(message).ToContainTextAsync("You have a new booking!");
    }
}
