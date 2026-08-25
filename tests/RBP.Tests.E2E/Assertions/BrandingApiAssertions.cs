using AwesomeAssertions;
using RBP.Data.DTO.Branding;
using RestfulBooker.Data.DTO.Common;

namespace RBP.Tests.E2E.Assertions;

public static class BrandingApiAssertions
{
    public static void ShouldIndicateSuccessfulUpdate(this ApiResponse<BrandingDto> response)
    {
        response.IsSuccessful.Should().BeTrue();
    }
}
