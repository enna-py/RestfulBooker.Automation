using Microsoft.Playwright;
using RBP.Business.Ui.Pages.Admin;
using RBP.Data.DTO.Room;
using RestfulBooker.Core.Logging;

namespace RBP.Business.Ui.Components;

public sealed class EditRoomComponent
{
    private readonly IPage _page;

    public EditRoomComponent(IPage page)
    {
        _page = page;
    }

    private ILocator Root =>
        _page.Locator(".room-details");

    private ILocator Description =>
        Root.Locator("#description");

    private ILocator Price =>
        Root.Locator("#roomPrice");

    private ILocator Image =>
        Root.Locator("#image");

    private ILocator Update =>
        Root.Locator("#update");

    private ILocator Feature(string value) =>
    Root.Locator($"input[value='{value}']");

    public async Task<EditRoomComponent> SetDescriptionAsync(string value)
    {
        LoggerManager.Logger.Information(
            "Setting room description to '{Description}'",
            value);

        await Description.FillAsync(value);
        return this;
    }

    public async Task<EditRoomComponent> SetPriceAsync(decimal value)
    {
        LoggerManager.Logger.Information(
            "Setting room price to '{Price}'",
            value);

        await Price.FillAsync(value.ToString());
        return this;
    }

    public async Task<EditRoomComponent> SetImageAsync(string value)
    {
        LoggerManager.Logger.Information(
            "Setting room image to '{Image}'",
            value);

        await Image.FillAsync(value);
        return this;
    }

    public async Task<EditRoomComponent> FillAsync(RoomCardDto room)
    {
        LoggerManager.Logger.Information(
            "Filling room edit form with updated room data");

        await SetDescriptionAsync(room.Description);

        await SetPriceAsync(room.Price);

        await SetImageAsync(room.Image);

        await SelectFeaturesAsync(room.Features.ToArray());

        return this;
    }

    public async Task<EditRoomComponent> SelectFeaturesAsync(params string[] features)
    {
        LoggerManager.Logger.Information(
            "Selecting room features: {Features}",
            string.Join(", ", features));

        IReadOnlyList<string> all =
        [
            "TV",
        "WiFi",
        "Radio",
        "Refreshments",
        "Safe",
        "Views"
        ];

        foreach (string feature in all)
        {
            bool shouldBeChecked =
                features.Contains(feature);

            ILocator checkbox = Feature(feature);

            if (await checkbox.IsCheckedAsync() != shouldBeChecked)
            {
                await checkbox.ClickAsync();
            }
        }

        return this;
    }
    public async Task<AdminRoomDetailsPage> SaveAsync()
    {
        LoggerManager.Logger.Information(
            "Saving room changes");

        // Saving resets the room state (clearing price/image) before an async
        // re-fetch repopulates the read-only summary - wait for that re-fetch
        // to complete so callers don't read the transient empty state.
        var refetchResponse = _page.WaitForResponseAsync(r =>
            r.Url.Contains("/api/room/") &&
            r.Request.Method == "GET");

        await Update.ClickAsync();

        await refetchResponse;

        return new AdminRoomDetailsPage(_page);
    }
}
