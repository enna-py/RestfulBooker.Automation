using RBP.Business.Ui.Pages;
using RBP.Business.Ui.Steps;
using RBP.Data.DTO.Booking;
using RBP.Tests.E2E.Assertions;
using RBP.Tests.E2E.Base;
using RestfulBooker.Api.Clients;
using RestfulBooker.Core.Logging;

namespace RBP.Tests.E2E.TC02;

public class RoomBookingFixture : BaseFixture
{
    [Test]
    [Category("Smoke")]
    [Category("E2E")]
    [Property("JiraKey", "RBP-8")]
    public async Task TC02_User_Should_Be_Able_To_Book_Room()
    {
        var checkIn = DateOnly.FromDateTime(DateTime.Today.AddDays(Random.Shared.Next(1, 180)));

        BookingRequest requestModel = new()
        {
            RoomId = 2,
            CheckIn = checkIn,
            CheckOut = checkIn.AddDays(Random.Shared.Next(1, 8)),
            Guest = new GuestDto
            {
                FirstName = "Test",
                LastName = "Test",
                Email = "Test@test.com",
                Phone = "123456789111"
            }
        };

        await AuthApiClient.LoginAsync();
        LoggerManager.Logger.Information(
            "After login: Authenticated={Auth}, Token={Token}",
            AuthState.IsAuthenticated,
            AuthState.Token);

        HomePage homePage = await CreatePage<HomePage>().OpenAsync();

        BookingSteps bookingSteps = new(homePage);

        var result = await bookingSteps.BookRoomAsync(requestModel);

        await result.Page.ShouldHaveSuccessfulBooking();

        BookingDto actualBooking = await BookingApiClient.GetBookingAsync(result.Booking.BookingId);

        actualBooking.ShouldMatch(result.Booking);
    }
}

