using Microsoft.Playwright;
using RBP.Data.DTO.Booking;
using RestfulBooker.Core.Logging;
using System.Text.Json;

namespace RBP.Business.Ui.Components;

public sealed class BookingFormComponent
{
    private readonly IPage _page;

    private ILocator Root =>
        _page.Locator(".booking-card form");

    private ILocator FirstName =>
    Root.Locator(".room-firstname");

    private ILocator LastName =>
        Root.Locator(".room-lastname");

    private ILocator Email =>
        Root.Locator(".room-email");

    private ILocator Phone =>
        Root.Locator(".room-phone");
    private ILocator SubmitButton =>
    Root.GetByRole(
        AriaRole.Button,
        new()
        {
            Name = "Reserve Now"
        });

    public BookingFormComponent(IPage page)
    {
        _page = page;
    }
    public async Task WaitUntilVisibleAsync()
    {
        LoggerManager.Logger.Information(
            "Waiting for booking form");

        await FirstName.WaitForAsync();
    }

    public async Task FillFirstNameAsync(string firstName)
    {
        LoggerManager.Logger.Information(
            "Entering first name");

        await FirstName.FillAsync(firstName);
    }

    public async Task FillLastNameAsync(string lastName)
    {
        LoggerManager.Logger.Information(
            "Entering last name");

        await LastName.FillAsync(lastName);
    }

    public async Task FillEmailAsync(string email)
    {
        LoggerManager.Logger.Information(
            "Entering email");

        await Email.FillAsync(email);
    }

    public async Task FillPhoneAsync(string phone)
    {
        LoggerManager.Logger.Information(
            "Entering phone");

        await Phone.FillAsync(phone);
    }

    public async Task FillAsync(BookingRequest request)
    {
        await FillFirstNameAsync(request.Guest.FirstName);

        await FillLastNameAsync(request.Guest.LastName);

        await FillEmailAsync(request.Guest.Email);

        await FillPhoneAsync(request.Guest.Phone);
    }

    public async Task<BookingDto> SubmitAsync()
    {
        LoggerManager.Logger.Information(
            "Submitting booking");

        var responseTask = _page.WaitForResponseAsync(r =>
            r.Url.EndsWith("/booking") &&
            r.Request.Method == "POST");

        await SubmitButton.ClickAsync();

        var response = await responseTask;

        return JsonSerializer.Deserialize<BookingDto>(
            await response.TextAsync())!;
    }
}
