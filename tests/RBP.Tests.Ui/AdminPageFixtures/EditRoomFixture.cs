using RBP.Business.Ui.Components;
using RBP.Business.Ui.Pages;
using RBP.Business.Ui.Pages.Admin;
using RBP.Data.DTO.Room;
using RBP.Tests.Ui.Assertions;
using RBP.Tests.Ui.Base;
using RBP.Tests.Ui.Builders.Room;

namespace RBP.Tests.Ui.AdminPageFixtures;
public class EditRoomFixture : BaseFixture
{
    [Test]
    public async Task EditRoomViaAdminPanelShouldUpdatePublicRoomData()
    {
        RoomCardDto expectedRoom =
            new EditRoomDataBuilder()
                .WithDescription("SAD")
                .WithPrice(150)
                .WithFeatures("TV", "WiFi", "Views")
                .Build();

        AdminRoomsPage adminRooms = await LoginAsAdminAsync();

        EditRoomComponent editor = await adminRooms.OpenEditRoomAsync(1);

        await editor.SetDescriptionAsync(expectedRoom.Description);
        await editor.SetPriceAsync(expectedRoom.Price);
        await editor.SelectFeaturesAsync(expectedRoom.Features.ToArray());
        await editor.SaveAsync();

        HomePage homePage = await CreatePage<HomePage>().OpenAsync();

        await homePage.UpdateRoomList();

        RoomCardDto actualRoom = await homePage.GetRoomAsync(1);

        actualRoom.ShouldMatch(expectedRoom);
    }
}
