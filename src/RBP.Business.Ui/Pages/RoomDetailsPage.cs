using Microsoft.Playwright;
using RBP.Business.Ui.Components;
using RBP.Business.Ui.Pages;
using RBP.Core.Helpers;
using RBP.Data.DTO.Room;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Core.Logging;

namespace RBP.Business.Ui.Pagesl;

public sealed class RoomDetailsPage : BasePage
{
    private ILocator SubmitButton => Page.Locator("#doReservation");

    private ILocator BookingCard => Page.Locator("//div[contains(@class,'booking-card')]");

    public ILocator ConfirmationTitle => BookingCard.Locator("//h2");

    public ILocator ConfirmationMessage => BookingCard.Locator("//p[1]");

    public ILocator BookingDates => BookingCard.Locator(".//p/strong");

    public ILocator ReturnHomeButton => BookingCard.Locator(".//a");

    private ILocator DescriptionSection =>
        Page.Locator("div.mb-4").Filter(new() { HasText = "Room Description" });

    private ILocator Description =>
        DescriptionSection.Locator("p");

    private ILocator FeaturesSection =>
        Page.Locator("div.mb-4").Filter(new() { HasText = "Room Features" });

    private ILocator Features =>
        FeaturesSection.Locator(".col-md-4 span");

    private ILocator HeroImage =>
        Page.Locator("img.hero-image");

    private ILocator PriceContainer =>
        Page.Locator("div.d-flex.align-items-baseline");

    public BookingFormComponent BookingForm { get; }

    public RoomDetailsPage(IPage page)
        : base(page)
    {
        BookingForm = new BookingFormComponent(page);
    }

    public async Task<RoomDetailsPage> OpenAsync(
        int roomId,
        DateOnly checkIn,
        DateOnly checkOut)
    {
        LoggerManager.Logger.Information(
            "Opening reservation page for room {RoomId} ({CheckIn} - {CheckOut})",
            roomId,
            checkIn,
            checkOut);

        string checkInValue = checkIn.ToString("yyyy-MM-dd");
        string checkOutValue = checkOut.ToString("yyyy-MM-dd");

        await Page.GotoAsync(
            $"{ConfigurationService.Current.Ui.BaseUrl}/reservation/{roomId}?checkin={checkInValue}&checkout={checkOutValue}");

        return this;
    }

    public async Task ReserveNowAsync()
    {
        LoggerManager.Logger.Information(
            "Clicking 'Reserve now'");

        await SubmitButton.ScrollIntoViewIfNeededAsync();

        await SubmitButton.WaitForAsync();

        await SubmitButton.ClickAsync();
    }

    public async Task<RoomCardDto> GetRoomAsync()
    {
        string description = await Description.InnerTextAsync();

        IReadOnlyList<string> features = await Features.AllInnerTextsAsync();

        string image = await HeroImage.GetAttributeAsync("src") ?? string.Empty;

        string priceText = await PriceContainer.InnerTextAsync();

        return new RoomCardDto
        {
            Description = StringNormalizer.Normalize(description),

            Price = decimal.Parse(
                StringNormalizer.Normalize(priceText)
                    .Replace("£", "")
                    .Replace("per night", "")),

            Image = StringNormalizer.Normalize(image),

            Features = StringNormalizer.Normalize(features)
        };
    }
}