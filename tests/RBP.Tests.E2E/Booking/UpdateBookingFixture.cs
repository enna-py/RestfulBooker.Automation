using RBP.Business.Ui.Pages.Admin;
using RBP.Business.Ui.Steps;
using RBP.Data.DTO.Booking;
using RBP.Tests.E2E.Base;
using RestfulBooker.Core.Constants;
using RestfulBooker.Data.Builders.Booking;
using RestfulBooker.Data.Builders.Room;
using RestfulBooker.Data.DTO;
using RestfulBooker.Data.DTO.Common;
using RestfulBooker.Data.DTO.Room;

namespace RBP.Tests.E2E.Booking;

public class UpdateBookingFixture : BaseFixture
{
    [Test]
    [Category(TestType.E2E)]
    [Category(TestType.Regression)]
    [Property("JiraKey", "RBP-14")]
    public async Task Booking_Should_Be_Updated_Via_Admin_Panel_And_Reflected_In_Api()
    {
        await AuthApiClient.LoginAsync();

        RoomApiRequest roomRequest = new RoomApiRequestBuilder()
            .WithRoomName($"TC08-{DateTime.UtcNow:HHmmssfff}")
            .Build();

        RoomDto createdRoom = await RoomApiClient.CreateRoomAsync(roomRequest);

        TrackRoomForCleanup(createdRoom.RoomId);

        DateOnly checkIn =
            DateOnly.FromDateTime(DateTime.Today.AddDays(Random.Shared.Next(200, 400)));

        BookingRequest originalBooking = new BookingRequestBuilder()
            .WithRoomId(createdRoom.RoomId)
            .WithGuestName("TC08", $"Guest{DateTime.UtcNow:HHmmssfff}")
            .WithDates(checkIn, checkIn.AddDays(3))
            .Build();

        ApiResponse<BookingCreationResponse> createResponse =
            await BookingApiClient.CreateBookingAsync(originalBooking);

        int bookingId = createResponse.Data!.BookingId;

        TrackBookingForCleanup(bookingId);

        BookingRequest updatedValues = new BookingRequestBuilder()
            .WithRoomId(createdRoom.RoomId)
            .WithGuestName("TC08Updated", "GuestUpdated")
            .WithDates(checkIn.AddDays(5), checkIn.AddDays(8))
            .Build();

        AdminLoginPage loginPage = await CreatePage<AdminLoginPage>().OpenAsync();

        AuthenticationSteps authenticationSteps = new(loginPage);

        await authenticationSteps.LoginAsAdminAsync();

        BookingManagementSteps bookingManagementSteps =
            new(CreatePage<AdminRoomDetailsPage>());

        await bookingManagementSteps.UpdateBookingAsync(
            createdRoom.RoomId,
            originalBooking.Guest.LastName,
            updatedValues);

        BookingDto actualBooking = await BookingApiClient.GetBookingAsync(bookingId);

        await bookingManagementSteps.ShouldMatchAsync(actualBooking, updatedValues, bookingId);
    }
}
