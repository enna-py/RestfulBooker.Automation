using global::RBP.Business.Ui.Pages;
using RestfulBooker.Core.Configuration;
using static Microsoft.Playwright.Assertions;

namespace RBP.Tests.Ui.Assertions;

public static class HomePageAssertions
{
    public static async Task ShouldBeOpen(this HomePage page)
    {
        await Expect(page.PlaywrightPage).ToHaveURLAsync(
            ConfigurationService.Current.Ui.BaseUrl);
    }
}
