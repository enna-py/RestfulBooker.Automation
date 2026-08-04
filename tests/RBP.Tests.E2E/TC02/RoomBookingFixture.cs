using RBP.Business.Ui.Pages;
using RBP.Business.Ui.Pagesl;
using RBP.Core.Assertion;
using RBP.Data.DTO.Booking;
using RBP.Tests.E2E.Base;
using RestfulBooker.Api.Clients;
using RestfulBooker.Core.Authentication;
using RestfulBooker.Core.Logging;

namespace RBP.Tests.E2E.TC02;

public class RoomBookingFixture : BaseFixture
{
    BookingRequest requestModel = new()
    {
        RoomId = 3,

        CheckIn = DateOnly.FromDateTime(DateTime.Today.AddDays(85)),

        CheckOut = DateOnly.FromDateTime(DateTime.Today.AddDays(86)),

        Guest = new GuestDto
        {
            FirstName = "Olenate",
            LastName = "Pomazan",
            Email = "olenaa@test.com",
            Phone = "123456789111"
        }
    };

    [Test]
    [Category("Smoke")]
    [Category("E2E")]
    [Property("JiraKey", "RBP-2")]
    public async Task User_Should_Be_Able_To_Book_Room()
    {
        await AuthApiClient.LoginAsync();

        LoggerManager.Logger.Information(
            "Authenticated = {Auth}, token = {Token}",
            TokenProvider.IsAuthenticated,
            TokenProvider.Token);

        HomePage homePage =
            await CreatePage<HomePage>()
                .OpenAsync();

        await homePage.FillBookingDatesAsync(requestModel.CheckIn, requestModel.CheckOut);

        RoomDetailsPage roomDetailsPage =
            await homePage.OpenRoomAsync(3);

        await roomDetailsPage.ReserveNowAsync();

        await roomDetailsPage.BookingForm.WaitUntilVisibleAsync();

        await roomDetailsPage.BookingForm.FillAsync(requestModel);

        await roomDetailsPage.BookingForm.SubmitAsync();

        await roomDetailsPage.ShouldHaveSuccessfulBooking();

        IReadOnlyCollection<BookingDto> bookings =
            await BookingApiClient.GetBookingsByRoomAsync(requestModel.RoomId);

        bookings.ShouldContainBooking(requestModel);
    }
}

