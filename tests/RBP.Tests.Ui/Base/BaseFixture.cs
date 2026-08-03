using Microsoft.Playwright;
using RBP.Business.Ui.Browser;
using RBP.Business.Ui.Pages;
using RBP.Business.Ui.Pages.Admin;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Core.Logging;

namespace RBP.Tests.Ui.Base;

public abstract class BaseFixture
{
    protected BrowserSession Browser = null!;
    protected IBrowserContext Context = null!;
    protected IPage Page = null!;

    [OneTimeSetUp]
    public async Task GlobalSetup()
    {
        ConfigurationService.Initialize();

        LoggerManager.Initialize();

        Browser = await BrowserFactory.CreateAsync(
            ConfigurationService.Current.Ui);

        LoggerManager.Logger.Information(
            "===== Test Run Started =====");

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
    public async Task Setup()
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
    }

    [TearDown]
    public async Task Cleanup()
    {
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

    protected async Task<AdminRoomsPage> LoginAsAdminAsync()
    {
        return await (await CreatePage<AdminLoginPage>().OpenAsync()).LoginAsync();
    }
}
