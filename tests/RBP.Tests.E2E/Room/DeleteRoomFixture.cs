using AwesomeAssertions;
using RBP.Business.Ui.Pages;
using RBP.Business.Ui.Steps;
using RBP.Tests.E2E.Base;
using RestfulBooker.Core.Constants;
using RestfulBooker.Data.Builders.Room;
using RestfulBooker.Data.DTO;
using RestfulBooker.Data.DTO.Common;
using RestfulBooker.Data.DTO.Room;

namespace RBP.Tests.E2E.Room;

public class DeleteRoomFixture : BaseFixture
{
    [Test]
    [Category(TestType.E2E)]
    [Category(TestType.Regression)]
    [Property("JiraKey", "RBP-12")]
    public async Task Deleted_Room_Should_No_Longer_Be_Available()
    {
        await AuthApiClient.LoginAsync();

        HomePage homePage = await CreatePage<HomePage>().OpenAsync();

        RoomListSteps roomListSteps = new(homePage);

        await roomListSteps.RefreshRoomListAsync();

        int visibleRoomCountBeforeChange = (await roomListSteps.GetRoomsAsync()).Count;

        RoomApiRequest roomToDelete = new RoomApiRequestBuilder().Build();

        RoomDto createdRoom = await RoomApiClient.CreateRoomAsync(roomToDelete);

        ApiResponse<object> deleteResponse =
            await RoomApiClient.DeleteRoomAsync(createdRoom.RoomId);

        deleteResponse.IsSuccessful.Should().BeTrue();

        IReadOnlyCollection<RoomDto> apiRooms = await RoomApiClient.GetRoomsAsync();

        apiRooms.Should().NotContain(r => r.RoomId == createdRoom.RoomId);

        await roomListSteps.RefreshRoomListAsync();

        await roomListSteps.ShouldHaveRoomCountAsync(visibleRoomCountBeforeChange);
    }
}
