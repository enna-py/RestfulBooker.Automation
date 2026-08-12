using RBP.Business.Api.Clients;
using RestfulBooker.Api.Clients;
using RestfulBooker.Core.Authentication;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Tests.Base;

namespace RestfulBooker.Tests.API;

public abstract class BaseApiFixture : BaseFixture
{
    protected AuthenticationState AuthState { get; private set; } = null!;
    protected AuthApiClient AuthClient { get; private set; } = null!;
    protected BookingApiClient BookingApiClient { get; private set; } = null!;

    protected override async Task OnGlobalSetupAsync()
    {
        ConfigurationService.Initialize();

        AuthState = new AuthenticationState();
        AuthClient = new AuthApiClient(AuthState);
        BookingApiClient = new BookingApiClient(AuthState);

        await AuthClient.LoginAsync();
    }
}