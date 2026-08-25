using RBP.Business.Ui.Pages;
using RBP.Data.DTO.Room;
using RBP.Tests.E2E.Assertions;
using RBP.Tests.E2E.Base;
using RestfulBooker.Api.Clients;
using RestfulBooker.Data.DTO;

namespace RBP.Tests.E2E.Room;

public class RoomListDataMappingFixture : BaseFixture
{
    [Test]
    [Category("Smoke")]
    [Category("E2E")]
    [Property("JiraKey", "RBP-7")]
    public async Task Room_List_Should_Match_Api_Data()
    {
        IReadOnlyCollection<RoomDto> apiRooms = await RoomApiClient.GetRoomsAsync();

        DateOnly checkIn = DateOnly.FromDateTime(DateTime.Today.AddDays(Random.Shared.Next(1, 15)));

        HomePage homePage = await CreatePage<HomePage>().OpenAsync();

        await homePage.FillBookingDatesAsync(checkIn, checkIn.AddDays(1));

        await homePage.UpdateRoomList();

        IReadOnlyCollection<RoomCardDto> uiRooms = await homePage.GetRoomsAsync();

        uiRooms.ShouldMatchApiRooms(apiRooms);
    }
}