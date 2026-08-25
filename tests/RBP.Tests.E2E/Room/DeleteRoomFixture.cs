using RBP.Business.Ui.Pages;
using RBP.Data.DTO.Room;
using RBP.Tests.E2E.Assertions;
using RBP.Tests.E2E.Base;
using RestfulBooker.Data.Builders.Room;
using RestfulBooker.Data.DTO;
using RestfulBooker.Data.DTO.Common;
using RestfulBooker.Data.DTO.Room;

namespace RBP.Tests.E2E.Room;

public class DeleteRoomFixture : BaseFixture
{
    [Test]
    [Category("E2E")]
    [Category("Regression")]
    [Property("JiraKey", "RBP-12")]
    public async Task Deleted_Room_Should_No_Longer_Be_Available()
    {
        await AuthApiClient.LoginAsync();

        HomePage homePage = await CreatePage<HomePage>().OpenAsync();

        await homePage.UpdateRoomList();

        int visibleRoomCountBeforeChange = (await homePage.GetRoomsAsync()).Count;

        RoomApiRequest roomToDelete = new RoomApiRequestBuilder().Build();

        RoomDto createdRoom = await RoomApiClient.CreateRoomAsync(roomToDelete);

        ApiResponse<object> deleteResponse =
            await RoomApiClient.DeleteRoomAsync(createdRoom.RoomId);

        deleteResponse.ShouldIndicateSuccessfulDeletion();

        IReadOnlyCollection<RoomDto> apiRooms = await RoomApiClient.GetRoomsAsync();

        apiRooms.ShouldNotContainRoom(createdRoom.RoomId);

        await homePage.UpdateRoomList();

        IReadOnlyCollection<RoomCardDto> uiRoomsAfterChange = await homePage.GetRoomsAsync();

        uiRoomsAfterChange.ShouldHaveRoomCount(visibleRoomCountBeforeChange);
    }
}
