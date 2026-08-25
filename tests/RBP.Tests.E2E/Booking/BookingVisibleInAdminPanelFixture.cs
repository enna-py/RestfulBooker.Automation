using RBP.Business.Ui.Pages.Admin;
using RBP.Business.Ui.Pagesl;
using RBP.Business.Ui.Steps;
using RBP.Data.DTO.Booking;
using RBP.Tests.E2E.Assertions;
using RBP.Tests.E2E.Base;
using RestfulBooker.Data.Builders.Booking;
using RestfulBooker.Data.Builders.Room;
using RestfulBooker.Data.DTO;
using RestfulBooker.Data.DTO.Room;

namespace RBP.Tests.E2E.Booking;

public class BookingVisibleInAdminPanelFixture : BaseFixture
{
    [Test]
    [Category("Regression")]
    [Category("UI")]
    [Property("JiraKey", "RBP-9")]
    public async Task Booking_Should_Be_Visible_In_Admin_Panel()
    {
        int roomNumber = Random.Shared.Next(500, 1000);

        RoomApiRequest roomRequest = new RoomApiRequestBuilder()
            .WithRoomName(roomNumber.ToString())
            .Build();

        await AuthApiClient.LoginAsync();

        RoomDto createdRoom = await RoomApiClient.CreateRoomAsync(roomRequest);

        TrackRoomForCleanup(createdRoom.RoomId);

        int checkInOffsetDays = Random.Shared.Next(1, 15);

        DateOnly checkIn = DateOnly.FromDateTime(DateTime.Today.AddDays(checkInOffsetDays));

        BookingRequest requestModel = new BookingRequestBuilder()
            .WithRoomId(createdRoom.RoomId)
            .WithDates(checkIn, checkIn.AddDays(1))
            .WithGuestName("Olenate", $"Pomazan{DateTime.UtcNow:HHmmssfff}")
            .WithEmail("olenaa@test.com")
            .WithPhone("123456789111")
            .Build();

        BookingSteps bookingSteps =
            new(CreatePage<RoomDetailsPage>());

        var result = await bookingSteps.BookRoomAsync(requestModel);

        await result.Page.ShouldHaveSuccessfulBooking();

        AuthenticationSteps authenticationSteps =
            new(await CreatePage<AdminLoginPage>().OpenAsync());

        await authenticationSteps.LoginAsAdminAsync();

        AdminReportPage reportPage =
            await CreatePage<AdminReportPage>().OpenAsync();

        await reportPage.ShouldContainBookingEvent(
            requestModel.Guest,
            roomNumber: roomNumber);

        AdminMessagesPage messagesPage =
            await CreatePage<AdminMessagesPage>().OpenAsync();

        await messagesPage.ShouldContainBookingNotification(requestModel.Guest);
    }
}
