using RBP.Business.Ui.Components;
using RBP.Business.Ui.Pages.Admin;
using RBP.Data.DTO.Booking;

namespace RBP.Business.Ui.Steps;

public sealed class BookingManagementSteps
{
    private readonly AdminRoomDetailsPage _roomDetailsPage;

    public BookingManagementSteps(AdminRoomDetailsPage roomDetailsPage)
    {
        _roomDetailsPage = roomDetailsPage;
    }

    public async Task<AdminRoomDetailsPage> UpdateBookingAsync(
        int roomId,
        string currentLastName,
        BookingRequest updatedValues)
    {
        await _roomDetailsPage.OpenAsync(roomId);

        BookingEditComponent editor =
            await _roomDetailsPage.OpenBookingEditAsync(currentLastName);

        await editor.FillAsync(updatedValues);

        return await editor.SaveAsync();
    }
}
