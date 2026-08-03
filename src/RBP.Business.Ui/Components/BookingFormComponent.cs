using Microsoft.Playwright;
using RBP.Data.DTO.Booking;
using RestfulBooker.Core.Logging;

namespace RBP.Business.Ui.Components;

public sealed class BookingFormComponent
{
    private readonly IPage _page;
    private ILocator FirstName =>
    _page.Locator(".room-firstname");

    private ILocator LastName =>
        _page.Locator(".room-lastname");

    private ILocator Email =>
        _page.Locator(".room-email");

    private ILocator Phone =>
        _page.Locator(".room-phone");
    private ILocator SubmitButton =>
    _page.GetByRole(
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
            "Entering first name '{FirstName}'",
            firstName);

        await FirstName.FillAsync(firstName);
    }

    public async Task FillLastNameAsync(string lastName)
    {
        LoggerManager.Logger.Information(
            "Entering last name '{LastName}'",
            lastName);

        await LastName.FillAsync(lastName);
    }

    public async Task FillEmailAsync(string email)
    {
        LoggerManager.Logger.Information(
            "Entering email '{Email}'",
            email);

        await Email.FillAsync(email);
    }

    public async Task FillPhoneAsync(string phone)
    {
        LoggerManager.Logger.Information(
            "Entering phone '{Phone}'",
            phone);

        await Phone.FillAsync(phone);
    }

    public async Task FillAsync(BookingRequest request)
    {
        await FillFirstNameAsync(request.Guest.FirstName);

        await FillLastNameAsync(request.Guest.LastName);

        await FillEmailAsync(request.Guest.Email);

        await FillPhoneAsync(request.Guest.Phone);
    }

    public async Task SubmitAsync()
    {
        LoggerManager.Logger.Information(
            "Submitting booking");

        await SubmitButton.ClickAsync();
    }
}
