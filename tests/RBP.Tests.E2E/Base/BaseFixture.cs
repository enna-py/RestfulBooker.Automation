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
    protected AuthenticationState AuthState = null!;
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
        AuthState = new AuthenticationState();
        AuthApiClient = new AuthApiClient(AuthState);
        BookingApiClient = new BookingApiClient(AuthState);
        RoomApiClient = new RoomApiClient(AuthState);
    }

    [TearDown]
    public async Task Cleanup()
    {
        await SaveScreenshotOnFailureAsync();

        await Context.CloseAsync();

        LoggerManager.Logger.Information(
            $"Finished test: {TestContext.CurrentContext.Test.Name}");
    }

    protected TPage CreatePage<TPage>()
    where TPage : BasePage
    {
        return (TPage)Activator.CreateInstance(
            typeof(TPage),
            Page)!;
    }

    private async Task SaveScreenshotOnFailureAsync()
    {
        if (TestContext.CurrentContext.Result.Outcome.Status != NUnit.Framework.Interfaces.TestStatus.Failed)
        {
            return;
        }

        string screenshotsDirectory = Path.Combine(
            TestContext.CurrentContext.WorkDirectory,
            "Screenshots");

        Directory.CreateDirectory(screenshotsDirectory);

        string fileName =
            $"{TestContext.CurrentContext.Test.Name}_{DateTime.Now:yyyyMMdd_HHmmss}.png";

        string filePath = Path.Combine(screenshotsDirectory, fileName);

        await Page.ScreenshotAsync(new()
        {
            Path = filePath,
            FullPage = true
        });

        TestContext.AddTestAttachment(filePath, "Failure screenshot");

        LoggerManager.Logger.Error(
            "Failure screenshot saved to {Path}",
            filePath);
    }
}