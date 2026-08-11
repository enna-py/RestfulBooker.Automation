using RestfulBooker.Api.Clients;
using RestfulBooker.Core.Authentication;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Core.Logging;

namespace RestfulBooker.Tests.Base;

public abstract class BaseFixture
{
    protected AuthApiClient AuthApiClient;

    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        ConfigurationService.Initialize();

        LoggerManager.Initialize();

        LoggerManager.Logger.Information("===== Test Run Started =====");

        await OnGlobalSetupAsync();
    }

    protected virtual Task OnGlobalSetupAsync()
    {
        return Task.CompletedTask;
    }

    [OneTimeTearDown]
    public async Task GlobalCleanup()
    {
        await OnGlobalCleanupAsync();

        LoggerManager.Logger.Information("===== Test Run Finished =====");

        LoggerManager.Dispose();
    }

    protected virtual Task OnGlobalCleanupAsync()
    {
        return Task.CompletedTask;
    }

    [SetUp]
    public virtual void Setup()
    {
        LoggerManager.Logger.Information(
            $"Starting test: {TestContext.CurrentContext.Test.Name}");
        AuthApiClient = new AuthApiClient(new AuthenticationState());
    }

    [TearDown]
    public virtual void Cleanup()
    {
        LoggerManager.Logger.Information(
            $"Finished test: {TestContext.CurrentContext.Test.Name}");
    }
}