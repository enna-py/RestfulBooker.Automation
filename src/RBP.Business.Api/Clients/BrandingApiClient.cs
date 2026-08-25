using RBP.Data.DTO.Branding;
using RestfulBooker.Api.Base;
using RestfulBooker.Api.Endpoints;
using RestfulBooker.Core.Authentication;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Core.Exceptions;
using RestfulBooker.Data.DTO.Common;

namespace RBP.Business.Api.Clients;

public sealed class BrandingApiClient : BaseApiClient
{
    public BrandingApiClient(AuthenticationState authState)
        : base(ConfigurationService.Current.Api.BrandingUrl, authState)
    {
    }

    public async Task<BrandingDto> GetBrandingAsync()
    {
        ApiResponse<BrandingDto> response =
            await GetAsync<BrandingDto>(BrandingEndpoints.Branding);

        if (response.Data is null)
        {
            throw new ApiException(
                (int)response.StatusCode,
                "Branding was not returned.");
        }

        return response.Data;
    }

    public async Task<ApiResponse<BrandingDto>> UpdateBrandingAsync(
        BrandingDto request,
        bool validateResponse = true)
    {
        return await PutAsync<BrandingDto, BrandingDto>(
            BrandingEndpoints.Branding,
            request,
            validateResponse);
    }
}
