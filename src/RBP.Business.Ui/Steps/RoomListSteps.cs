using AwesomeAssertions;
using RBP.Business.Ui.Pages;
using RBP.Data.DTO.Room;
using RestfulBooker.Data.DTO;

namespace RBP.Business.Ui.Steps;

public sealed class RoomListSteps
{
    private readonly HomePage _homePage;

    public RoomListSteps(HomePage homePage)
    {
        _homePage = homePage;
    }

    public Task RefreshRoomListAsync()
    {
        return _homePage.UpdateRoomList();
    }

    public async Task RefreshRoomListAsync(DateOnly checkIn, DateOnly checkOut)
    {
        await _homePage.FillBookingDatesAsync(checkIn, checkOut);

        await _homePage.UpdateRoomList();
    }

    public Task<IReadOnlyCollection<RoomCardDto>> GetRoomsAsync()
    {
        return _homePage.GetRoomsAsync();
    }

    public async Task ShouldMatchApiRoomsAsync(IReadOnlyCollection<RoomDto> apiRooms)
    {
        IReadOnlyCollection<RoomCardDto> uiRooms = await GetRoomsAsync();

        uiRooms.Should().NotBeEmpty();

        foreach (RoomCardDto uiRoom in uiRooms)
        {
            RoomDto apiRoom = apiRooms.Should()
                .ContainSingle(x => x.RoomId == uiRoom.Id)
                .Subject;

            uiRoom.Type.Should().Be(apiRoom.Type);

            uiRoom.Description.Should().Be(apiRoom.Description);

            uiRoom.Image.Should().Be(apiRoom.Image);

            uiRoom.Price.Should().Be(apiRoom.RoomPrice);

            uiRoom.Features.Should()
                .BeEquivalentTo(apiRoom.Features);
        }
    }

    public async Task ShouldHaveRoomCountAsync(int expectedCount)
    {
        IReadOnlyCollection<RoomCardDto> uiRooms = await GetRoomsAsync();

        uiRooms.Should().HaveCount(expectedCount);
    }
}
