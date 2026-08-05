using RBP.Business.Ui.Components;
using RBP.Business.Ui.Pages.Admin;
using RBP.Data.DTO.Room;

namespace RBP.Business.Ui.Steps;

public sealed class RoomManagementSteps
{
    private readonly AdminRoomsPage _adminRooms;

    public RoomManagementSteps(AdminRoomsPage adminRooms)
    {
        _adminRooms = adminRooms;
    }

    public async Task EditRoomAsync(
        int roomId,
        RoomCardDto room)
    {
        EditRoomComponent editor =
            await _adminRooms.OpenEditRoomAsync(roomId);

        await editor.FillAsync(room);

        await editor.SaveAsync();
    }
}
