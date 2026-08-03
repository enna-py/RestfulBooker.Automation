using Microsoft.Playwright;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Core.Logging;

namespace RBP.Business.Ui.Pages.Admin;

public sealed class AdminLoginPage : BasePage
{
    private ILocator Username =>
        Page.Locator("#username");

    private ILocator Password =>
        Page.Locator("#password");

    private ILocator LoginButton =>
        Page.Locator("#doLogin");

    public AdminLoginPage(IPage page)
        : base(page)
    {
    }

    public async Task<AdminLoginPage> OpenAsync()
    {
        LoggerManager.Logger.Information(
            "Opening Admin login page");

        await Page.GotoAsync(
            $"{ConfigurationService.Current.Ui.BaseUrl}/admin");

        return this;
    }

    public async Task<AdminRoomsPage> LoginAsync()
    {
        LoggerManager.Logger.Information(
            "Logging into Admin panel");

        var credentials = ConfigurationService.Current.Credentials;

        await Username.FillAsync(credentials.Username);

        await Password.FillAsync(credentials.Password);

        await LoginButton.ClickAsync();

        await Page.WaitForURLAsync("**/admin/**");

        return new AdminRoomsPage(Page);
    }
}
