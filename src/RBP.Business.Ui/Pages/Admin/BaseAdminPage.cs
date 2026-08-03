using Microsoft.Playwright;
using RestfulBooker.Core.Configuration;

namespace RBP.Business.Ui.Pages.Admin;

public abstract class BaseAdminPage : BasePage
{
    protected BaseAdminPage(IPage page)
        : base(page)
    {
    }

    protected async Task EnsureAuthorizedAsync()
    {
        await Page.GotoAsync(
            $"{ConfigurationService.Current.Ui.BaseUrl}/admin");

        if (await IsLoginPageAsync())
        {
            AdminLoginPage loginPage =
                new(Page);

            await loginPage.LoginAsync();
        }
    }

    private async Task<bool> IsLoginPageAsync()
    {
        return await Page
            .Locator("#username")
            .IsVisibleAsync();
    }
}