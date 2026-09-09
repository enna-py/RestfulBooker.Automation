using RBP.Business.Ui.Pages.Admin;
using RestfulBooker.Core.Logging;

namespace RBP.Business.Ui.Steps;

public sealed class AuthenticationSteps
{
    private readonly AdminLoginPage _loginPage;

    public AuthenticationSteps(AdminLoginPage loginPage)
    {
        _loginPage = loginPage;
    }

    public Task<AdminRoomsPage> LoginAsAdminAsync()
    {
        LoggerManager.Logger.Information(
            "User logs in as administrator");

        return _loginPage.LoginAsync();
    }
}