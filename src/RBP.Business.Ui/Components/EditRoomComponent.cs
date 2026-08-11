using Microsoft.Playwright;
using RBP.Business.Ui.Pages.Admin;
using RBP.Data.DTO.Room;

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
        await Description.FillAsync(value);
        return this;
    }

    public async Task<EditRoomComponent> SetPriceAsync(decimal value)
    {
        await Price.FillAsync(value.ToString());
        return this;
    }

    public async Task<AdminRoomsPage> FillAsync(RoomCardDto room)
    {
        await Description.FillAsync(room.Description);

        await Price.FillAsync(room.Price.ToString());

        await SelectFeaturesAsync(room.Features.ToArray());

        return new AdminRoomsPage(_page);
    }

    public async Task<EditRoomComponent> SelectFeaturesAsync(params string[] features)
    {
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
    public async Task<AdminRoomsPage> SaveAsync()
    {
        await Update.ClickAsync();

        return new AdminRoomsPage(_page);
    }
}
