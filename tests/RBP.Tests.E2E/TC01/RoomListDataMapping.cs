using RBP.Business.Ui.Pages;
using RBP.Data.DTO.Room;
using RBP.Tests.E2E.Assertions;
using RBP.Tests.E2E.Base;
using RestfulBooker.Api.Clients;
using RestfulBooker.Data.DTO;

namespace RBP.Tests.E2E.TC01;

public class RoomListDataMapping : BaseFixture
{
    [Test]
    [Category("Smoke")]
    [Category("E2E")]
    [Property("JiraKey", "RBP-1")]
    public async Task Room_List_Should_Match_Api_Data()
    {
        IReadOnlyCollection<RoomDto> apiRooms = await RoomApiClient.GetRoomsAsync();

        HomePage homePage = await CreatePage<HomePage>().OpenAsync();

        IReadOnlyCollection<RoomCardDto> uiRooms = await homePage.GetRoomsAsync();

        uiRooms.ShouldMatchApiRooms(apiRooms);
    }
}