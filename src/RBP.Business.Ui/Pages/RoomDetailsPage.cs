using Microsoft.Playwright;
using RBP.Business.Ui.Components;
using RBP.Business.Ui.Pages;
using RestfulBooker.Core.Logging;

namespace RBP.Business.Ui.Pagesl;

public sealed class RoomDetailsPage : BasePage
{
    private ILocator SubmitButton => Page.Locator("#doReservation");
    private ILocator ConfirmationTitle =>
    Page.GetByRole(
        AriaRole.Heading,
        new() { Name = "Booking Confirmed" });

    public BookingFormComponent BookingForm { get; }

    public RoomDetailsPage(IPage page)
        : base(page)
    {
        BookingForm = new BookingFormComponent(page);
    }

    public async Task ReserveNowAsync()
    {
        LoggerManager.Logger.Information(
            "Clicking 'Reserve now'");

        await SubmitButton.ScrollIntoViewIfNeededAsync();
        await SubmitButton.ClickAsync();
    }

    public async Task<string> GetConfirmationTextAsync()
    {
        return await ConfirmationTitle.InnerTextAsync();
    }

    public async Task<bool> IsSuccessAsync()
    {
        throw new NotImplementedException();
    }
}