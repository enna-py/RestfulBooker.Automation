using RestfulBooker.Api.Clients;
using RestfulBooker.Core.Authentication;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Tests.Base;

namespace RestfulBooker.Tests.API;

public abstract class BaseApiFixture : BaseFixture
{
    protected AuthApiClient AuthClient { get; private set; } = null!;

    protected override async Task OnGlobalSetupAsync()
    {
        ConfigurationService.Initialize();

        AuthClient = new AuthApiClient(new AuthenticationState());

        await AuthClient.LoginAsync();
    }
}