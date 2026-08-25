using Microsoft.Playwright;
using RBP.Business.Ui.Components;
using RBP.Data.DTO.Room;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Core.Logging;

namespace RBP.Business.Ui.Pages.Admin;

public sealed class AdminRoomDetailsPage : BasePage
{
    // First and last name render as separate sibling <p> elements with no
    // whitespace between them in the DOM, so Playwright's HasText concatenates
    // them without a space and a "first last" filter never matches. Filtering
    // by last name alone works, so callers must pass a unique last name.
    private ILocator BookingRow(string lastName) =>
        Page.Locator(".detail")
            .Filter(new() { HasText = lastName });

    // Save doesn't navigate away - the same .room-details container swaps from
    // the edit form into this read-only summary, re-fetched from the API.
    private ILocator RoomSummary =>
        Page.Locator(".room-details");

    private ILocator DescriptionSummary =>
        RoomSummary.Locator("p").Filter(new() { HasText = "Description:" }).Locator("span");

    private ILocator FeaturesSummary =>
        RoomSummary.Locator("p").Filter(new() { HasText = "Features:" }).Locator("span").First;

    private ILocator RoomPriceSummary =>
        RoomSummary.Locator("p").Filter(new() { HasText = "Room price:" }).Locator("span");

    private ILocator RoomImageSummary =>
        RoomSummary.Locator("img");

    public AdminRoomDetailsPage(IPage page)
        : base(page)
    {
    }

    public async Task<AdminRoomDetailsPage> OpenAsync(int roomId)
    {
        LoggerManager.Logger.Information(
            "Opening Admin room details page for room {RoomId}",
            roomId);

        await Page.GotoAsync(
            $"{ConfigurationService.Current.Ui.BaseUrl}/admin/room/{roomId}");

        return this;
    }

    public async Task<BookingEditComponent> OpenBookingEditAsync(string lastName)
    {
        LoggerManager.Logger.Information(
            "Opening edit form for booking with last name '{LastName}'",
            lastName);

        await BookingRow(lastName)
            .Locator(".fa-pencil")
            .ClickAsync();

        return new BookingEditComponent(Page);
    }

    public async Task<EditRoomComponent> OpenRoomEditAsync()
    {
        LoggerManager.Logger.Information(
            "Opening room edit form");

        await Page.GetByRole(AriaRole.Button, new() { Name = "Edit" }).ClickAsync();

        return new EditRoomComponent(Page);
    }

    public async Task<RoomCardDto> GetRoomSummaryAsync()
    {
        string description = await DescriptionSummary.InnerTextAsync();

        string priceText = await RoomPriceSummary.InnerTextAsync();

        string featuresText = await FeaturesSummary.InnerTextAsync();

        string image = await RoomImageSummary.GetAttributeAsync("src") ?? string.Empty;

        return new RoomCardDto
        {
            Description = description,
            Price = decimal.Parse(priceText),
            Image = image,
            Features = featuresText.Split(
                ',',
                StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
        };
    }
}
