using RBP.Business.Ui.Pages.Admin;
using RBP.Business.Ui.Steps;
using RBP.Data.Builders.Room;
using RBP.Data.DTO.Room;
using RBP.Tests.E2E.Assertions;
using RBP.Tests.E2E.Base;
using RestfulBooker.Data.Builders.Room;
using RestfulBooker.Data.DTO;
using RestfulBooker.Data.DTO.Room;

namespace RBP.Tests.E2E.Room;

public class EditRoomFixture : BaseFixture
{
    [Test]
    [Category("Regression")]
    [Category("UI")]
    [Property("JiraKey", "RBP-13")]
    public async Task Edit_Room_Via_Admin_Panel_Should_Update_Admin_Room_List()
    {
        string roomName = $"TC07-{DateTime.UtcNow:HHmmssfff}";

        RoomApiRequest roomRequest = new RoomApiRequestBuilder()
            .WithRoomName(roomName)
            .Build();

        await AuthApiClient.LoginAsync();

        RoomDto createdRoom = await RoomApiClient.CreateRoomAsync(roomRequest);

        TrackRoomForCleanup(createdRoom.RoomId);

        RoomCardDto expectedRoom = new EditRoomDataBuilder()
            .WithDescription("Updated via TC07 automated test")
            .WithPrice(275)
            .WithFeatures("Radio", "Refreshments")
            .WithImage("/images/room2.jpg")
            .Build();

        AdminLoginPage loginPage = await CreatePage<AdminLoginPage>().OpenAsync();

        AuthenticationSteps authenticationSteps = new(loginPage);

        AdminRoomsPage adminRooms = await authenticationSteps.LoginAsAdminAsync();

        adminRooms = await adminRooms.OpenAsync();

        RoomManagementSteps roomSteps = new(adminRooms);

        AdminRoomDetailsPage roomDetails = await roomSteps.EditRoomAsync(roomName, expectedRoom);

        RoomCardDto adminDisplayedRoom = await roomDetails.GetRoomSummaryAsync();

        adminDisplayedRoom.ShouldMatch(expectedRoom);
    }
}
