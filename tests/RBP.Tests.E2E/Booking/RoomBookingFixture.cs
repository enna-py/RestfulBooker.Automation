using RBP.Business.Ui.Pagesl;
using RBP.Business.Ui.Steps;
using RBP.Data.DTO.Booking;
using RBP.Tests.E2E.Assertions;
using RBP.Tests.E2E.Base;
using RestfulBooker.Core.Logging;
using RestfulBooker.Data.Builders.Booking;

namespace RBP.Tests.E2E.Booking;

public class RoomBookingFixture : BaseFixture
{
    [Test]
    [Category("Smoke")]
    [Category("E2E")]
    [Property("JiraKey", "RBP-8")]
    public async Task TC02_User_Should_Be_Able_To_Book_Room()
    {
        DateOnly checkIn = DateOnly.FromDateTime(DateTime.Today);

        BookingRequest requestModel = new BookingRequestBuilder()
            .WithDates(checkIn, checkIn.AddDays(1))
            .WithGuestName("Test", "Test")
            .WithEmail("Test@test.com")
            .WithPhone("123456789111")
            .Build();

        await AuthApiClient.LoginAsync();

        LoggerManager.Logger.Information(
            "After login: Authenticated={Auth}, Token={Token}",
            AuthState.IsAuthenticated,
            AuthState.Token);

        BookingSteps bookingSteps = new(CreatePage<RoomDetailsPage>());

        var result = await bookingSteps.BookRoomAsync(requestModel);

        TrackBookingForCleanup(result.Booking.BookingId);

        await result.Page.ShouldHaveSuccessfulBooking();

        BookingDto actualBooking = await BookingApiClient.GetBookingAsync(result.Booking.BookingId);

        actualBooking.ShouldMatch(result.Booking);
    }
}

