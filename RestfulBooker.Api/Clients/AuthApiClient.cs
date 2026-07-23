using RestfulBooker.Api.Base;
using RestfulBooker.Api.Endpoints;
using RestfulBooker.Core.Authentication;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Data.DTO.Auth;
using RestfulBooker.Data.DTO.Common;
using RestfulBooker.Data.Responses.Auth;

namespace RestfulBooker.Api.Clients;

public sealed class AuthApiClient : BaseApiClient
{
    public AuthApiClient()
        : base(ConfigurationService.Current.Api.AuthUrl)
    {
    }

    public async Task<ApiResponse<LoginResponse>> LoginAsync(LoginRequest request)
    {
        ApiResponse<LoginResponse> response =
            await PostAsync<LoginRequest, LoginResponse>(
                AuthEndpoints.Login,
                request);

        if (response.IsSuccessful &&
            response.Data is not null)
        {
            TokenProvider.Authenticate(response.Data.Token);
        }

        return response;
    }

    public Task<ApiResponse<LoginResponse>> LoginAsync()
    {
        CredentialsSettings credentials =
            ConfigurationService.Current.Credentials;

        return LoginAsync(new LoginRequest
        {
            Username = credentials.Username,
            Password = credentials.Password
        });
    }

    public async Task<ApiResponse<object>> LogoutAsync(TokenRequest request)
    {
        ApiResponse<object> response =
            await PostAsync<TokenRequest, object>(
                AuthEndpoints.Logout,
                request);

        if (response.IsSuccessful)
        {
            TokenProvider.SignOut();
        }

        return response;
    }

    public Task<ApiResponse<LoginResponse>> ValidateAsync(TokenRequest request)
    {
        return PostAsync<TokenRequest, LoginResponse>(
            AuthEndpoints.Validate,
            request);
    }
}