using AwesomeAssertions;
using RBP.Business.Ui.Components;
using RBP.Business.Ui.Pages.Admin;
using RBP.Data.DTO.Room;
using RestfulBooker.Core.Logging;
using RestfulBooker.Data.DTO.Room;
using static Microsoft.Playwright.Assertions;

namespace RBP.Business.Ui.Steps;

public sealed class RoomManagementSteps
{
    private readonly AdminRoomsPage _adminRooms;

    public RoomManagementSteps(AdminRoomsPage adminRooms)
    {
        _adminRooms = adminRooms;
    }

    public async Task<AdminRoomDetailsPage> EditRoomAsync(
        string roomName,
        RoomCardDto room)
    {
        LoggerManager.Logger.Information(
            "User edits room '{RoomName}'",
            roomName);

        EditRoomComponent editor =
            await _adminRooms.OpenEditRoomAsync(roomName);

        await editor.FillAsync(room);

        return await editor.SaveAsync();
    }

    public async Task<AdminRoomsPage> CreateRoomAsync(RoomApiRequest room)
    {
        LoggerManager.Logger.Information(
            "User creates a new room '{RoomName}'",
            room.RoomName);

        await _adminRooms.CreateRoomForm.FillAsync(room);

        return await _adminRooms.CreateRoomForm.SaveAsync();
    }

    public async Task ShouldContainRoomAsync(string roomName)
    {
        await Expect(_adminRooms.RoomItem(roomName)).ToBeVisibleAsync();
    }

    public async Task ShouldMatchAsync(AdminRoomDetailsPage roomDetails, RoomCardDto expected)
    {
        RoomCardDto actual = await roomDetails.GetRoomSummaryAsync();

        actual.Type.Should().Be(expected.Type);

        actual.Description.Should().Be(expected.Description);

        actual.Price.Should().Be(expected.Price);

        actual.Image.Should().Be(expected.Image);

        actual.Features.Should()
            .BeEquivalentTo(expected.Features);
    }
}
