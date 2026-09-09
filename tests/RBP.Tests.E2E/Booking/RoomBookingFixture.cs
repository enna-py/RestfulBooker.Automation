using RBP.Business.Ui.Pagesl;
using RBP.Business.Ui.Steps;
using RBP.Data.DTO.Booking;
using RBP.Tests.E2E.Base;
using RestfulBooker.Core.Constants;
using RestfulBooker.Data.Builders.Booking;

namespace RBP.Tests.E2E.Booking;

public class RoomBookingFixture : BaseFixture
{
    [Test]
    [Category(TestType.Smoke)]
    [Category(TestType.E2E)]
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

        BookingSteps bookingSteps = new(CreatePage<RoomDetailsPage>());

        var result = await bookingSteps.BookRoomAsync(requestModel);

        TrackBookingForCleanup(result.Booking.BookingId);

        await bookingSteps.ShouldHaveSuccessfulBookingAsync(result.Page);

        BookingDto actualBooking = await BookingApiClient.GetBookingAsync(result.Booking.BookingId);

        await bookingSteps.ShouldMatchAsync(actualBooking, result.Booking);
    }
}

