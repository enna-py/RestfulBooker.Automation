using Microsoft.Playwright;
using RBP.Business.Api.Clients;
using RBP.Business.Ui.Browser;
using RBP.Business.Ui.Pages;
using RestfulBooker.Api.Clients;
using RestfulBooker.Core.Authentication;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Core.Logging;

namespace RBP.Tests.E2E.Base;

public abstract class BaseFixture
{
    protected BrowserSession Browser = null!;
    protected AuthApiClient AuthApiClient = null!;
    protected BookingApiClient BookingApiClient = null!;
    protected RoomApiClient RoomApiClient = null!;
    protected IBrowserContext Context = null!;
    protected IPage Page = null!;

    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        ConfigurationService.Initialize();

        LoggerManager.Initialize();

        LoggerManager.Logger.Information(
            "===== Test Run Started =====");

        Browser = await BrowserFactory.CreateAsync(
            ConfigurationService.Current.Ui);

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

        await Browser.DisposeAsync();

        LoggerManager.Logger.Information(
            "===== Test Run Finished =====");

        LoggerManager.Dispose();
    }

    protected virtual Task OnGlobalCleanupAsync()
    {
        return Task.CompletedTask;
    }

    [SetUp]
    public async virtual Task Setup()
    {
        LoggerManager.Logger.Information(
        $"Starting test: {TestContext.CurrentContext.Test.Name}");

        Context = await Browser.Browser.NewContextAsync(
            new BrowserNewContextOptions
            {
                ViewportSize = new ViewportSize
                {
                    Width = 1920,
                    Height = 1080
                }
            });

        Page = await Context.NewPageAsync();
        AuthApiClient = new AuthApiClient();
        BookingApiClient = new BookingApiClient();
        RoomApiClient = new RoomApiClient();
    }

    [TearDown]
    public async Task Cleanup()
    {
        await Context.CloseAsync();

        LoggerManager.Logger.Information(
            $"Finished test: {TestContext.CurrentContext.Test.Name}");
    
        TokenProvider.SignOut();
    }

    protected TPage CreatePage<TPage>()
    where TPage : BasePage
    {
        return (TPage)Activator.CreateInstance(
            typeof(TPage),
            Page)!;
    }
}