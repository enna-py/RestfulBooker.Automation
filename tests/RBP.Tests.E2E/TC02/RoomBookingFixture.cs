using AwesomeAssertions;
using RBP.Business.Ui.Pages;
using RBP.Business.Ui.Pagesl;
using RBP.Business.Ui.Steps;
using RBP.Data.DTO.Booking;
using RBP.Tests.E2E.Assertions;
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

        CheckIn = DateOnly.FromDateTime(DateTime.Today.AddDays(92)),

        CheckOut = DateOnly.FromDateTime(DateTime.Today.AddDays(93)),

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
    [Property("JiraKey", "RBP-8")]
    public async Task TC02_User_Should_Be_Able_To_Book_Room()
    {
        await AuthApiClient.LoginAsync();
        LoggerManager.Logger.Information(
            "After login: Authenticated={Auth}, Token={Token}",
            TokenProvider.IsAuthenticated,
            TokenProvider.Token);

        HomePage homePage =
            await CreatePage<HomePage>()
                .OpenAsync();

        BookingSteps bookingSteps =
            new(homePage);

        var result = await bookingSteps.BookRoomAsync(requestModel);

        await result.Page.ShouldHaveSuccessfulBooking();

        BookingDto actualBooking = await BookingApiClient.GetBookingAsync(result.Booking.BookingId);

        actualBooking.ShouldMatch(result.Booking);
    }
}

