using AwesomeAssertions;
using RBP.Business.Ui.Components;
using RBP.Business.Ui.Pages.Admin;
using RBP.Data.DTO.Booking;
using RestfulBooker.Core.Logging;

namespace RBP.Business.Ui.Steps;

public sealed class BookingManagementSteps
{
    private readonly AdminRoomDetailsPage _roomDetailsPage;

    public BookingManagementSteps(AdminRoomDetailsPage roomDetailsPage)
    {
        _roomDetailsPage = roomDetailsPage;
    }

    public async Task<AdminRoomDetailsPage> UpdateBookingAsync(
        int roomId,
        string currentLastName,
        BookingRequest updatedValues)
    {
        LoggerManager.Logger.Information(
            "User updates booking for room {RoomId}",
            roomId);

        await _roomDetailsPage.OpenAsync(roomId);

        BookingEditComponent editor =
            await _roomDetailsPage.OpenBookingEditAsync(currentLastName);

        await editor.FillAsync(updatedValues);

        return await editor.SaveAsync();
    }

    public Task ShouldMatchAsync(BookingDto actual, BookingRequest expected, int bookingId)
    {
        BookingDto expectedBooking = BuildExpectedBooking(expected, bookingId);

        actual.Should().BeEquivalentTo(expectedBooking, options => options.ComparingByMembers<BookingDto>());

        return Task.CompletedTask;
    }

    private static BookingDto BuildExpectedBooking(BookingRequest expected, int bookingId)
    {
        return new BookingDto
        {
            BookingId = bookingId,
            RoomId = expected.RoomId,
            FirstName = expected.Guest.FirstName,
            LastName = expected.Guest.LastName,
            DepositPaid = true,
            BookingDates = new BookingDatesDto
            {
                CheckIn = expected.CheckIn,
                CheckOut = expected.CheckOut
            }
        };
    }
}
