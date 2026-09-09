using AwesomeAssertions;
using Microsoft.Playwright;
using RBP.Business.Ui.Components;
using RBP.Business.Ui.Pages.Admin;
using RBP.Data.DTO.Booking;
using RBP.Data.DTO.Message;
using RestfulBooker.Core.Logging;
using static Microsoft.Playwright.Assertions;

namespace RBP.Business.Ui.Steps;

public sealed class AdminMessagesSteps
{
    private AdminMessagesPage _messagesPage;

    public AdminMessagesSteps(AdminMessagesPage messagesPage)
    {
        _messagesPage = messagesPage;
    }

    public async Task DeleteMessageAsync(string identifier)
    {
        _messagesPage = await _messagesPage.DeleteMessageAsync(identifier);
    }

    public async Task ShouldContainBookingNotificationAsync(GuestDto guest)
    {
        LoggerManager.Logger.Information(
            "Verify booking notification exists in Admin messages");

        ILocator message = _messagesPage.MessageRow($"{guest.FirstName} {guest.LastName}");

        await Expect(message).ToBeVisibleAsync();

        await Expect(message).ToContainTextAsync("You have a new booking!");
    }

    public async Task ShouldNotContainMessageAsync(string identifier)
    {
        LoggerManager.Logger.Information(
            "Verify message '{Identifier}' is no longer displayed",
            identifier);

        await Expect(_messagesPage.MessageRow(identifier)).Not.ToBeVisibleAsync();
    }

    public async Task ShouldMatchMessageDetailsAsync(string identifier, ContactMessageRequest expected)
    {
        MessageDetailComponent messageDetail = await _messagesPage.OpenMessageAsync(identifier);

        ContactMessageRequest actual = await messageDetail.GetDetailsAsync();

        actual.Name.Should().Be(expected.Name);

        actual.Email.Should().Be(expected.Email);

        actual.Phone.Should().Be(expected.Phone);

        actual.Subject.Should().Be(expected.Subject);

        actual.Description.Should().Be(expected.Description);

        _messagesPage = await messageDetail.CloseAsync();
    }
}
