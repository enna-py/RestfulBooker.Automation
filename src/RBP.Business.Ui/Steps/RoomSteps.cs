using RBP.Business.Ui.Components;
using RBP.Business.Ui.Pages.Admin;
using RBP.Data.DTO.Room;

namespace RBP.Business.Ui.Steps;

public sealed class RoomSteps
{
    private readonly AdminRoomsPage _roomsPage;

    public RoomSteps(AdminRoomsPage roomsPage)
    {
        _roomsPage = roomsPage;
    }

    public async Task EditRoomAsync(
        int roomId,
        RoomCardDto room)
    {
        EditRoomComponent editor =
            await _roomsPage.OpenEditRoomAsync(roomId);

        await editor.FillAsync(room);

        await editor.SaveAsync();
    }
}