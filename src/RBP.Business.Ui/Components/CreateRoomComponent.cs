using Microsoft.Playwright;
using RBP.Business.Ui.Pages.Admin;
using RestfulBooker.Core.Logging;
using RestfulBooker.Data.DTO.Room;

namespace RBP.Business.Ui.Components;

public sealed class CreateRoomComponent
{
    private readonly IPage _page;

    public CreateRoomComponent(IPage page)
    {
        _page = page;
    }

    private ILocator RoomName =>
        _page.Locator("#roomName");

    private ILocator Type =>
        _page.Locator("#type");

    private ILocator Accessible =>
        _page.Locator("#accessible");

    private ILocator Price =>
        _page.Locator("#roomPrice");

    private ILocator Create =>
        _page.Locator("#createRoom");

    private ILocator Feature(string value) =>
        _page.Locator($"input[name='featureCheck'][value='{value}']");

    public async Task<CreateRoomComponent> SetRoomNameAsync(string value)
    {
        LoggerManager.Logger.Information(
            "Setting new room name to '{RoomName}'",
            value);

        await RoomName.FillAsync(value);
        return this;
    }

    public async Task<CreateRoomComponent> SetTypeAsync(string value)
    {
        LoggerManager.Logger.Information(
            "Setting new room type to '{Type}'",
            value);

        await Type.SelectOptionAsync(value);
        return this;
    }

    public async Task<CreateRoomComponent> SetAccessibleAsync(bool value)
    {
        LoggerManager.Logger.Information(
            "Setting new room accessible to '{Accessible}'",
            value);

        await Accessible.SelectOptionAsync(value.ToString().ToLowerInvariant());
        return this;
    }

    public async Task<CreateRoomComponent> SetPriceAsync(int value)
    {
        LoggerManager.Logger.Information(
            "Setting new room price to '{Price}'",
            value);

        await Price.FillAsync(value.ToString());
        return this;
    }

    public async Task<CreateRoomComponent> SelectFeaturesAsync(params string[] features)
    {
        LoggerManager.Logger.Information(
            "Selecting new room features: {Features}",
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

    public async Task<CreateRoomComponent> FillAsync(RoomApiRequest room)
    {
        LoggerManager.Logger.Information(
            "Filling create-room form with new room data");

        await SetRoomNameAsync(room.RoomName);

        await SetTypeAsync(room.Type);

        await SetAccessibleAsync(room.Accessible);

        await SetPriceAsync(room.RoomPrice);

        await SelectFeaturesAsync(room.Features.ToArray());

        return this;
    }

    public async Task<AdminRoomsPage> SaveAsync()
    {
        LoggerManager.Logger.Information(
            "Clicking 'Create' button");

        var createResponse = _page.WaitForResponseAsync(r =>
            r.Url.Contains("/api/room") &&
            r.Request.Method == "POST");

        await Create.ClickAsync();

        await createResponse;

        return new AdminRoomsPage(_page);
    }
}
