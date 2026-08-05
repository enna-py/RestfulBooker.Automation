using RBP.Business.Ui.Pages.Admin;

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
        return _loginPage.LoginAsync();
    }
}