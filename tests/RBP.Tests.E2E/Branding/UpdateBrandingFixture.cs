using AwesomeAssertions;
using RBP.Business.Ui.Pages;
using RBP.Data.Builders.Branding;
using RBP.Data.DTO.Branding;
using RBP.Tests.E2E.Assertions;
using RBP.Tests.E2E.Base;
using RestfulBooker.Core.Constants;
using RestfulBooker.Data.DTO.Common;

namespace RBP.Tests.E2E.Branding;

[NonParallelizable]
public class UpdateBrandingFixture : BaseFixture
{
    [Test]
    [Category(TestType.E2E)]
    [Category(TestType.Regression)]
    [Property("JiraKey", "RBP-16")]
    public async Task Branding_Should_Be_Updated_Via_Api_And_Displayed_On_Public_Site()
    {
        await AuthApiClient.LoginAsync();

        BrandingDto originalBranding = await BrandingApiClient.GetBrandingAsync();

        RegisterCleanupAction(() =>
            BrandingApiClient.UpdateBrandingAsync(originalBranding));

        BrandingDto updatedBranding = new BrandingBuilder()
            .WithLogoUrl(originalBranding.LogoUrl)
            .Build();

        ApiResponse<BrandingDto> updateResponse =
            await BrandingApiClient.UpdateBrandingAsync(updatedBranding);

        updateResponse.IsSuccessful.Should().BeTrue();

        HomePage homePage = await CreatePage<HomePage>().OpenAsync();

        await homePage.ShouldDisplayBranding(updatedBranding);
    }
}
