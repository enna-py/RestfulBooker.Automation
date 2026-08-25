using RBP.Business.Ui.Pages.Admin;
using static Microsoft.Playwright.Assertions;

namespace RBP.Tests.E2E.Assertions;

public static class AdminRoomsPageAssertions
{
    public static async Task ShouldContainRoom(this AdminRoomsPage page, string roomName)
    {
        await Expect(page.RoomItem(roomName)).ToBeVisibleAsync();
    }
}
