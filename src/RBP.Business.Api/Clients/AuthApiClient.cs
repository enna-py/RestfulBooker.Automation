using RestfulBooker.Api.Base;
using RestfulBooker.Api.Endpoints;
using RestfulBooker.Core.Authentication;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Data.DTO.Auth;
using RestfulBooker.Data.DTO.Common;
using RestfulBooker.Data.Responses.Auth;
using RestSharp;

namespace RestfulBooker.Api.Clients;

public sealed class AuthApiClient : BaseApiClient
{
    public AuthApiClient()
        : base(ConfigurationService.Current.Api.AuthUrl)
    {
    }

    public async Task LoginAsync(LoginRequest request)
    {
        RestResponse response =
            await PostRawAsync(
                AuthEndpoints.Login,
                request);

        var cookieHeader = response.Headers
            .FirstOrDefault(h => h.Name?.ToString() == "Set-Cookie");

        if (cookieHeader != null)
        {
            string cookie = cookieHeader.Value!.ToString()!;

            string token = cookie
                .Split(';')[0]
                .Replace("token=", "");

            TokenProvider.Authenticate(token);
        }
    }

    public Task LoginAsync()
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