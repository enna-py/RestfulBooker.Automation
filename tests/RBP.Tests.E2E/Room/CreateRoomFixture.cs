using RBP.Business.Ui.Pages.Admin;
using RBP.Business.Ui.Steps;
using RBP.Tests.E2E.Assertions;
using RBP.Tests.E2E.Base;
using RestfulBooker.Data.Builders.Room;
using RestfulBooker.Data.DTO;
using RestfulBooker.Data.DTO.Room;
using RestfulBooker.Data.Enums;

namespace RBP.Tests.E2E.Room;

public class CreateRoomFixture : BaseFixture
{
    [Test]
    [Category("E2E")]
    [Category("Regression")]
    [Property("JiraKey", "RBP-11")]
    public async Task Room_Should_Be_Created_Via_Admin_Panel_And_Available_In_Api()
    {
        string roomName = $"TC05-{DateTime.UtcNow:HHmmssfff}";
        int price = Random.Shared.Next(600, 999);

        RoomApiRequest roomToCreate = new RoomApiRequestBuilder()
            .WithRoomName(roomName)
            .WithType(RoomType.Family.ToString())
            .WithPrice(price)
            .WithFeatures("TV", "Views")
            .Build();

        await AuthApiClient.LoginAsync();

        AdminLoginPage loginPage = await CreatePage<AdminLoginPage>().OpenAsync();

        AuthenticationSteps authenticationSteps = new(loginPage);

        AdminRoomsPage adminRooms = await authenticationSteps.LoginAsAdminAsync();

        adminRooms = await adminRooms.OpenAsync();

        RoomManagementSteps roomSteps = new(adminRooms);

        adminRooms = await roomSteps.CreateRoomAsync(roomToCreate);

        await adminRooms.ShouldContainRoom(roomName);

        IReadOnlyCollection<RoomDto> apiRooms = await RoomApiClient.GetRoomsAsync();

        RoomDto createdRoom = apiRooms.First(r =>
            r.Type == roomToCreate.Type &&
            r.RoomPrice == roomToCreate.RoomPrice);

        TrackRoomForCleanup(createdRoom.RoomId);

        createdRoom.ShouldMatch(roomToCreate);
    }
}
