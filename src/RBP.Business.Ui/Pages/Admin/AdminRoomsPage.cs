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
        CreateRoomForm = new CreateRoomComponent(page);
    }

    public CreateRoomComponent CreateRoomForm { get; }

    public async Task<AdminRoomsPage> OpenAsync()
    {
        await Page.GotoAsync(ConfigurationService.Current.Ui.BaseUrl + "/admin/rooms");

        return this;
    }
    private ILocator RoomItems =>
        Page.GetByTestId("roomlisting");

    public ILocator RoomItem(string roomName) =>
        RoomItems.Filter(new()
        {
            Has = Page.Locator($"#roomName{roomName}")
        });

    public async Task<EditRoomComponent> OpenEditRoomAsync(string roomName)
    {
        LoggerManager.Logger.Information(
            "Opening edit form for room '{RoomName}'",
            roomName);

        await RoomItem(roomName).ClickAsync();

        return await new AdminRoomDetailsPage(Page).OpenRoomEditAsync();
    }
}