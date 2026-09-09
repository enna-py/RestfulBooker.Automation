using AwesomeAssertions;
using RBP.Business.Ui.Pages;
using RBP.Business.Ui.Pagesl;
using RBP.Data.DTO.Booking;
using RestfulBooker.Core.Logging;
using static Microsoft.Playwright.Assertions;

namespace RBP.Business.Ui.Steps;

public sealed class BookingSteps
{
    private readonly HomePage? _homePage;
    private readonly RoomDetailsPage? _roomDetailsPage;

    public BookingSteps(HomePage homePage)
    {
        _homePage = homePage;
    }

    public BookingSteps(RoomDetailsPage roomDetailsPage)
    {
        _roomDetailsPage = roomDetailsPage;
    }

    public async Task<(RoomDetailsPage Page, BookingDto Booking)> BookRoomAsync(
        BookingRequest request)
    {
        LoggerManager.Logger.Information(
            "User books room {RoomId} for {CheckIn}-{CheckOut}",
            request.RoomId,
            request.CheckIn,
            request.CheckOut);

        RoomDetailsPage roomDetailsPage = _roomDetailsPage is not null
            ? await _roomDetailsPage.OpenAsync(request.RoomId, request.CheckIn, request.CheckOut)
            : await OpenRoomFromHomePageAsync(request);

        await roomDetailsPage.ReserveNowAsync();

        await roomDetailsPage.BookingForm.WaitUntilVisibleAsync();

        await roomDetailsPage.BookingForm.FillAsync(request);

        BookingDto booking = await roomDetailsPage.BookingForm.SubmitAsync();

        return (roomDetailsPage, booking);
    }

    private async Task<RoomDetailsPage> OpenRoomFromHomePageAsync(BookingRequest request)
    {
        await _homePage!.FillBookingDatesAsync(
            request.CheckIn,
            request.CheckOut);

        return await _homePage.OpenRoomAsync(request.RoomId);
    }

    public async Task ShouldHaveSuccessfulBookingAsync(RoomDetailsPage page)
    {
        LoggerManager.Logger.Information("Verify booking confirmation");

        await Expect(page.ConfirmationTitle).ToHaveTextAsync("Booking Confirmed");
        await Expect(page.ConfirmationMessage).ToHaveTextAsync("Your booking has been confirmed for the following dates:");
    }

    public Task ShouldMatchAsync(BookingDto actual, BookingDto expected)
    {
        actual.Should().BeEquivalentTo(expected, options => options.ComparingByMembers<BookingDto>());

        return Task.CompletedTask;
    }
}