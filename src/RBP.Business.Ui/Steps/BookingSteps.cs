using RBP.Business.Ui.Pages;
using RBP.Business.Ui.Pagesl;
using RBP.Data.DTO.Booking;

namespace RBP.Business.Ui.Steps;

public sealed class BookingSteps
{
    private readonly HomePage _homePage;

    public BookingSteps(HomePage homePage)
    {
        _homePage = homePage;
    }

    public async Task<(RoomDetailsPage Page, BookingDto Booking)> BookRoomAsync(
        BookingRequest request)
    {
        await _homePage.FillBookingDatesAsync(
            request.CheckIn,
            request.CheckOut);

        RoomDetailsPage roomDetailsPage =
            await _homePage.OpenRoomAsync(request.RoomId);

        await roomDetailsPage.ReserveNowAsync();

        await roomDetailsPage.BookingForm.WaitUntilVisibleAsync();

        await roomDetailsPage.BookingForm.FillAsync(request);

        BookingDto booking = await roomDetailsPage.BookingForm.SubmitAsync();

        return (roomDetailsPage, booking);
    }
}