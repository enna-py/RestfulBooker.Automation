using RBP.Business.Ui.Pages;
using RBP.Data.DTO.Branding;
using static Microsoft.Playwright.Assertions;

namespace RBP.Tests.E2E.Assertions;

public static class HomePageAssertions
{
    public static async Task ShouldDisplayBranding(this HomePage page, BrandingDto expected)
    {
        // The public site's server-side branding fetch revalidates on a fixed
        // ~30s clock, independent of how many times the client reloads - an
        // update can land just after a revalidation (near-instant) or just
        // before one (up to the full window). Poll well past the observed
        // worst case (~29s) rather than a short fixed number of attempts.
        const int maxAttempts = 15;
        const int delayBetweenAttemptsMs = 2500;

        string expectedAddress = string.Join(", ",
            expected.Address.Line1,
            expected.Address.Line2,
            expected.Address.PostTown,
            expected.Address.County,
            expected.Address.PostCode);

        for (int attempt = 1; attempt <= maxAttempts; attempt++)
        {
            try
            {
                await Expect(page.BrandingTitle).ToContainTextAsync(expected.Name, new() { Timeout = 2000 });

                await Expect(page.BrandingDescription).ToHaveTextAsync(expected.Description, new() { Timeout = 2000 });

                await Expect(page.BrandingPhone).ToHaveTextAsync(expected.Contact.Phone, new() { Timeout = 2000 });

                await Expect(page.BrandingEmail).ToHaveTextAsync(expected.Contact.Email, new() { Timeout = 2000 });

                await Expect(page.BrandingDirections).ToHaveTextAsync(expected.Directions, new() { Timeout = 2000 });

                await Expect(page.BrandingAddress).ToHaveTextAsync(expectedAddress, new() { Timeout = 2000 });

                return;
            }
            catch (Exception) when (attempt < maxAttempts)
            {
                await Task.Delay(delayBetweenAttemptsMs);

                await page.ReloadAsync();
            }
        }
    }
}
