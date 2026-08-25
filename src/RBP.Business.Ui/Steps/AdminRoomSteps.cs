using RBP.Business.Ui.Components;
using RBP.Business.Ui.Pages.Admin;
using RBP.Data.DTO.Room;
using RestfulBooker.Data.DTO.Room;

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
        EditRoomComponent editor =
            await _adminRooms.OpenEditRoomAsync(roomName);

        await editor.FillAsync(room);

        return await editor.SaveAsync();
    }

    public async Task<AdminRoomsPage> CreateRoomAsync(RoomApiRequest room)
    {
        await _adminRooms.CreateRoomForm.FillAsync(room);

        return await _adminRooms.CreateRoomForm.SaveAsync();
    }
}
