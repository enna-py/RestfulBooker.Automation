using RestfulBooker.Api.Base;
using RestfulBooker.Api.Endpoints;
using RestfulBooker.Core.Authentication;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Core.Logging;
using RestfulBooker.Data.DTO.Auth;
using RestfulBooker.Data.DTO.Common;
using RestfulBooker.Data.Responses.Auth;
using RestSharp;
using System.Net;
using System.Security.Authentication;

namespace RestfulBooker.Api.Clients;

public sealed class AuthApiClient : BaseApiClient
{
    public AuthApiClient(AuthenticationState authState)
        : base(ConfigurationService.Current.Api.AuthUrl, authState)
    {
    }
    public async Task LoginAsync(LoginRequest request)
    {
        RestResponse response =
            await PostRawAsync(
                AuthEndpoints.Login,
                request);

        var cookieHeader = response.Headers.FirstOrDefault(
            h => string.Equals(
                h.Name?.ToString(),
                "Set-Cookie",
                StringComparison.OrdinalIgnoreCase));

        if (cookieHeader is null)
        {
            throw new AuthenticationException(
                "Authentication cookie was not returned.");
        }

        string cookie = cookieHeader.Value!.ToString()!;

        string token = cookie
            .Substring(cookie.IndexOf('=') + 1)
            .Split(';')[0];

        AuthState.Token = token;
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
            AuthState.Token = string.Empty;
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