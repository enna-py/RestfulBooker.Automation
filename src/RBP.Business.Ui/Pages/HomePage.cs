using Microsoft.Playwright;
using RBP.Business.Ui.Components;
using RBP.Business.Ui.Pagesl;
using RBP.Data.DTO.Room;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Core.Logging;

namespace RBP.Business.Ui.Pages;

public sealed class HomePage : BasePage
{
    private ILocator BookNowButton(int roomId) => Page.Locator("#rooms a.btn-primary").Nth(roomId - 1);

    public ILocator RoomCards =>
    Page.Locator(".room-card");
    private ILocator CheckInInput =>
    Page.Locator("label:text('Check In')")
        .Locator("xpath=following::input[1]");
    private ILocator CheckOutInput =>
    Page.Locator("label:text('Check Out')")
        .Locator("xpath=following::input[1]");
    private ILocator CheckAvailability => Page.Locator("#booking .btn");

    public ILocator ContactConfirmationMessage =>
        Page.GetByText("Thanks for getting in touch");

    private ILocator ContactInfoCard =>
        Page.Locator(".card-body").Filter(new() { HasText = "Contact Information" });

    public ILocator BrandingTitle =>
        Page.Locator("section.hero h1");

    public ILocator BrandingDescription =>
        Page.Locator("section.hero p.lead");

    public ILocator BrandingAddress =>
        ContactInfoCard.Locator("h5:text-is('Address') + p");

    public ILocator BrandingPhone =>
        ContactInfoCard.Locator("h5:text-is('Phone') + p");

    public ILocator BrandingEmail =>
        ContactInfoCard.Locator("h5:text-is('Email') + p");

    public ILocator BrandingDirections =>
        ContactInfoCard.Locator("h4:text-is('Getting Here') + p");

    public BookingFormComponent BookingForm { get; }

    public ContactFormComponent ContactForm { get; }

    public HomePage(IPage page)
    : base(page)
    {
        BookingForm = new BookingFormComponent(page);
        ContactForm = new ContactFormComponent(page);
    }

    public async Task<HomePage> OpenAsync()
    {
        LoggerManager.Logger.Information(
            "Opening Home page");

        await Page.GotoAsync(ConfigurationService.Current.Ui.BaseUrl);

        return this;
    }

    public async Task<HomePage> ReloadAsync()
    {
        LoggerManager.Logger.Information(
            "Reloading Home page");

        await Page.ReloadAsync();

        return this;
    }

    public async Task<HomePage> UpdateRoomList()
    {
        LoggerManager.Logger.Information(
            "Updating room list on Home Page...");

        await CheckAvailability.ScrollIntoViewIfNeededAsync();

        await CheckAvailability.IsVisibleAsync();

        await CheckAvailability.ClickAsync();

        await Page.WaitForLoadStateAsync(LoadState.NetworkIdle);

        return this;
    }

    public async Task FillBookingDatesAsync(DateOnly checkIn, DateOnly checkOut)
    {
        LoggerManager.Logger.Information(
            "Entering booking dates");

        string checkInValue =
            checkIn.ToString("dd/MM/yyyy");

        string checkOutValue =
            checkOut.ToString("dd/MM/yyyy");

        await CheckInInput.ScrollIntoViewIfNeededAsync();

        await CheckInInput.ClearAsync();
        await CheckInInput.FillAsync(checkInValue);

        await CheckOutInput.ClearAsync();
        await CheckOutInput.FillAsync(checkOutValue);
    }

    public async Task<RoomDetailsPage> OpenRoomAsync(int roomId)
    {
        await UpdateRoomList();

        LoggerManager.Logger.Information(
            "Opening room {RoomId}",
            roomId);

        await BookNowButton(roomId).ScrollIntoViewIfNeededAsync();

        await BookNowButton(roomId).ClickAsync();

        return new RoomDetailsPage(Page);
    }

    public async Task<RoomCardDto> GetRoomAsync(int roomId)
    {
        IReadOnlyCollection<RoomCardDto> rooms =
            await GetRoomsAsync();

        return rooms.Single(x => x.Id == roomId);
    }

    public async Task<IReadOnlyCollection<RoomCardDto>> GetRoomsAsync()
    {
        await RoomCards.First.ScrollIntoViewIfNeededAsync();

        IReadOnlyList<ILocator> cards =
            await RoomCards.AllAsync();

        List<RoomCardDto> result = [];

        foreach (ILocator card in cards)
        {
            RoomCardComponent roomCard = new(card);

            result.Add(await roomCard.GetDataAsync());
        }

        return result;
    }
}
