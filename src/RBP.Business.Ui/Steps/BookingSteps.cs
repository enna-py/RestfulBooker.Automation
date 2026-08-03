using RBP.Business.Ui.Pages;
using RBP.Business.Ui.Pagesl;
using RBP.Data.DTO.Booking;

namespace RBP.Business.Ui.Steps;

public static class BookingSteps
{
    public static async Task<BookingResult> BookRoom(
     this HomePage homePage,
     BookingRequest request)
    {
        RoomDetailsPage room =
            await homePage.OpenRoomAsync(request.RoomId);

        await room.ReserveNowAsync();

        await room.BookingForm.FillAsync(request);

        await room.BookingForm.SubmitAsync();

        return new BookingResult(
            await room.GetConfirmationTextAsync(),
            await room.IsSuccessAsync());
    }
}