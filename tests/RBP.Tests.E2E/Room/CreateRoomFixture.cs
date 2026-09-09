using AwesomeAssertions;
using RBP.Business.Ui.Pages.Admin;
using RBP.Business.Ui.Steps;
using RBP.Tests.E2E.Base;
using RestfulBooker.Core.Constants;
using RestfulBooker.Data.Builders.Room;
using RestfulBooker.Data.DTO;
using RestfulBooker.Data.DTO.Room;
using RestfulBooker.Data.Enums;

namespace RBP.Tests.E2E.Room;

public class CreateRoomFixture : BaseFixture
{
    [Test]
    [Category(TestType.E2E)]
    [Category(TestType.Regression)]
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

        await roomSteps.ShouldContainRoomAsync(roomName);

        IReadOnlyCollection<RoomDto> apiRooms = await RoomApiClient.GetRoomsAsync();

        RoomDto createdRoom = apiRooms.First(r =>
            r.Type == roomToCreate.Type &&
            r.RoomPrice == roomToCreate.RoomPrice);

        TrackRoomForCleanup(createdRoom.RoomId);

        ShouldMatch(createdRoom, roomToCreate);
    }

    // API-level integrity check: the room the API actually stored matches what was
    // requested. Not a UI outcome, so it doesn't belong on RoomManagementSteps (which only
    // ever touches Pages/Components) - kept as a private helper, its only consumer.
    private static void ShouldMatch(RoomDto actual, RoomApiRequest expected)
    {
        actual.Type.Should().Be(expected.Type);

        actual.Accessible.Should().Be(expected.Accessible);

        actual.RoomPrice.Should().Be(expected.RoomPrice);

        actual.Features.Should().BeEquivalentTo(expected.Features);
    }
}
