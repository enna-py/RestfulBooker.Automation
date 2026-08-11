using RBP.Business.Ui.Pages;
using RBP.Business.Ui.Pages.Admin;
using RBP.Business.Ui.Steps;
using RBP.Data.DTO.Booking;
using RBP.Tests.E2E.Assertions;
using RBP.Tests.E2E.Base;

namespace RBP.Tests.E2E.TC03;

public class BookingVisibleInAdminPanelFixture : BaseFixture
{
    static readonly int _checkInOffsetDays = Random.Shared.Next(1, 15);

    BookingRequest requestModel = new()
    {
        RoomId = 1,

        CheckIn = DateOnly.FromDateTime(DateTime.Today.AddDays(_checkInOffsetDays)),

        CheckOut = DateOnly.FromDateTime(DateTime.Today.AddDays(_checkInOffsetDays + 1)),

        Guest = new GuestDto
        {
            FirstName = "Olenate",
            LastName = $"Pomazan{DateTime.UtcNow:HHmmssfff}",
            Email = "olenaa@test.com",
            Phone = "123456789111"
        }
    };

    [Test]
    [Category("Regression")]
    [Category("UI")]
    [Property("JiraKey", "RBP-9")]
    public async Task Booking_Should_Be_Visible_In_Admin_Panel()
    {
        HomePage homePage =
            await CreatePage<HomePage>()
                .OpenAsync();

        BookingSteps bookingSteps =
            new(homePage);

        var result = await bookingSteps.BookRoomAsync(requestModel);

        await result.Page.ShouldHaveSuccessfulBooking();

        AuthenticationSteps authenticationSteps =
            new(await CreatePage<AdminLoginPage>().OpenAsync());

        await authenticationSteps.LoginAsAdminAsync();

        AdminReportPage reportPage =
            await CreatePage<AdminReportPage>().OpenAsync();

        await reportPage.ShouldContainBookingEvent(
            requestModel.Guest,
            roomNumber: requestModel.RoomId + 100);

        AdminMessagesPage messagesPage =
            await CreatePage<AdminMessagesPage>().OpenAsync();

        await messagesPage.ShouldContainBookingNotification(requestModel.Guest);
    }
}
