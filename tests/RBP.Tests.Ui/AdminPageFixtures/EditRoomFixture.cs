using RBP.Business.Ui.Components;
using RBP.Business.Ui.Pages;
using RBP.Business.Ui.Pages.Admin;
using RBP.Business.Ui.Steps;
using RBP.Data.DTO.Room;
using RBP.Tests.Ui.Assertions;
using RBP.Tests.Ui.Base;
using RBP.Tests.Ui.Builders.Room;

namespace RBP.Tests.Ui.AdminPageFixtures;
public class EditRoomFixture : BaseFixture
{
    [Test]
    [Category("Regression")]
    [Category("UI")]
    [Property("JiraKey", "RBP-3")]
    public async Task Edit_Room_Via_Admin_Panel_Should_Update_Public_Room_Data()
    {
        RoomCardDto expectedRoom =
            new EditRoomDataBuilder()
                .WithDescription("SAD")
                .WithPrice(150)
                .WithFeatures("TV", "WiFi", "Views")
                .Build();

        AdminRoomsPage adminRooms = await LoginAsAdminAsync();

        RoomManagementSteps roomSteps = new(adminRooms);

        await roomSteps.EditRoomAsync(
            roomId: 1,
            room: expectedRoom);

        HomePage homePage = await CreatePage<HomePage>().OpenAsync();

        await homePage.UpdateRoomList();

        RoomCardDto actualRoom = await homePage.GetRoomAsync(1);

        actualRoom.ShouldMatch(expectedRoom);
    }
}
