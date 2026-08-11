using RestfulBooker.Api.Clients;
using RestfulBooker.Core.Authentication;
using RestfulBooker.Tests.Base;

namespace RestfulBooker.Tests.Smoke;

[TestFixture]
public class LoginTestsFixture : BaseFixture
{
    private AuthApiClient _authClient = null!;

    [SetUp]
    public override void Setup()
    {
        base.Setup();

        _authClient = new AuthApiClient(new AuthenticationState());
    }
}
