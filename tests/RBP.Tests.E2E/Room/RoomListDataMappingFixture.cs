using RBP.Business.Ui.Pages;
using RBP.Business.Ui.Steps;
using RBP.Tests.E2E.Base;
using RestfulBooker.Api.Clients;
using RestfulBooker.Core.Constants;
using RestfulBooker.Data.DTO;

namespace RBP.Tests.E2E.Room;

public class RoomListDataMappingFixture : BaseFixture
{
    [Test]
    [Category(TestType.Smoke)]
    [Category(TestType.E2E)]
    [Property("JiraKey", "RBP-7")]
    public async Task Room_List_Should_Match_Api_Data()
    {
        IReadOnlyCollection<RoomDto> apiRooms = await RoomApiClient.GetRoomsAsync();

        DateOnly checkIn = DateOnly.FromDateTime(DateTime.Today.AddDays(Random.Shared.Next(1, 15)));

        HomePage homePage = await CreatePage<HomePage>().OpenAsync();

        RoomListSteps roomListSteps = new(homePage);

        await roomListSteps.RefreshRoomListAsync(checkIn, checkIn.AddDays(1));

        await roomListSteps.ShouldMatchApiRoomsAsync(apiRooms);
    }
}