using Microsoft.Playwright;
using RBP.Business.Ui.Components;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Core.Logging;

namespace RBP.Business.Ui.Pages.Admin;

public sealed class AdminRoomsPage : BasePage
{
    public AdminRoomsPage(IPage page)
        : base(page)
    {
    }
    public async Task<AdminRoomsPage> OpenAsync()
    {
        await Page.GotoAsync(ConfigurationService.Current.Ui.BaseUrl + "/admin/rooms");

        return this;
    }
    private ILocator RoomItems =>
        Page.GetByTestId("roomlisting");

    private ILocator RoomItem(int roomId) =>
        RoomItems.Filter(new()
        {
            Has = Page.Locator($"#roomName{roomId + 100}")
        });

    public async Task<EditRoomComponent> OpenEditRoomAsync(int roomId)
    {
        LoggerManager.Logger.Information(
            "Opening edit form for room {RoomId}",
            roomId);

        await RoomItem(roomId).ClickAsync();

        await Page.GetByRole(AriaRole.Button, new() { Name = "Edit" }).ClickAsync();

        return new EditRoomComponent(Page);
    }
}