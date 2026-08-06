using Microsoft.Playwright;
using RBP.Business.Ui.Components;
using RBP.Business.Ui.Pages;
using RestfulBooker.Core.Logging;
using static Microsoft.Playwright.Assertions;

namespace RBP.Business.Ui.Pagesl;

public sealed class RoomDetailsPage : BasePage
{
    private ILocator SubmitButton => Page.Locator("#doReservation");
    
    private ILocator BookingCard => Page.Locator("//div[contains(@class,'booking-card')]");
    
    public ILocator ConfirmationTitle => BookingCard.Locator("//h2");
    
    public ILocator ConfirmationMessage => BookingCard.Locator("//p[1]");
    
    public ILocator BookingDates => BookingCard.Locator(".//p/strong");

    public ILocator ReturnHomeButton => BookingCard.Locator(".//a");

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

        await Expect(SubmitButton).ToBeVisibleAsync();
        
        await SubmitButton.ClickAsync();
    }
}