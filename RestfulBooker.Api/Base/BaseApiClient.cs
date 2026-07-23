using RestfulBooker.Api.Factories;
using RestfulBooker.Core.Exceptions;
using RestfulBooker.Core.Logging;
using RestfulBooker.Data.DTO.Common;
using RestSharp;
using System.Diagnostics;

namespace RestfulBooker.Api.Base;

public abstract class BaseApiClient
{
    protected readonly RestClient Client;

    protected BaseApiClient(string baseUrl)
    {
        Client = RestClientFactory.Create(baseUrl);
    }

    protected async Task<ApiResponse<TResponse>> GetAsync<TResponse>(
        string endpoint,
        bool validateResponse = true,
        CancellationToken cancellationToken = default)
    {
        RestRequest request = RequestFactory.CreateGet(endpoint);

        return await ExecuteAsync<TResponse>(
            request,
            validateResponse,
            cancellationToken);
    }

    protected async Task<ApiResponse<TResponse>> DeleteAsync<TResponse>(
        string endpoint,
        bool validateResponse = true,
        CancellationToken cancellationToken = default)
    {
        RestRequest request = RequestFactory.CreateDelete(endpoint);

        return await ExecuteAsync<TResponse>(
            request,
            validateResponse,
            cancellationToken);
    }

    protected async Task<ApiResponse<TResponse>> PostAsync<TRequest, TResponse>(
        string endpoint,
        TRequest body,
        bool validateResponse = true,
        CancellationToken cancellationToken = default)
        where TRequest : class
    {
        RestRequest request = RequestFactory.CreatePost(endpoint, body);
        Console.WriteLine($"Endpoint: {endpoint}");
        return await ExecuteAsync<TResponse>(
            request,
            validateResponse,
            cancellationToken);
    }

    protected async Task<ApiResponse<TResponse>> PutAsync<TRequest, TResponse>(
        string endpoint,
        TRequest body,
        bool validateResponse = true,
        CancellationToken cancellationToken = default)
        where TRequest : class
    {
        RestRequest request = RequestFactory.CreatePut(endpoint, body);

        return await ExecuteAsync<TResponse>(
            request,
            validateResponse,
            cancellationToken);
    }

    private async Task<ApiResponse<TResponse>> ExecuteAsync<TResponse>(
        RestRequest request,
        bool validateResponse,
        CancellationToken cancellationToken)
    {
        LogRequest(request);

        Stopwatch stopwatch = Stopwatch.StartNew();

        RestResponse<TResponse> response =
            await SendRequestAsync<TResponse>(
                request,
                cancellationToken);

        stopwatch.Stop();

        LogResponse(response, stopwatch.Elapsed);

        if (validateResponse)
        {
            ValidateResponse(response);
        }

        return CreateApiResponse(response, stopwatch.Elapsed);
    }

    private async Task<RestResponse<TResponse>> SendRequestAsync<TResponse>(
        RestRequest request,
        CancellationToken cancellationToken)
    {
        return await Client.ExecuteAsync<TResponse>(
            request,
            cancellationToken);
    }

    private static ApiResponse<TResponse> CreateApiResponse<TResponse>(
        RestResponse<TResponse> response,
        TimeSpan duration)
    {
        return new ApiResponse<TResponse>
        {
            Data = response.Data,
            Content = response.Content,
            StatusCode = response.StatusCode,
            IsSuccessful = response.IsSuccessful,
            Duration = duration,
            ErrorMessage = response.ErrorMessage
        };
    }

    private static void ValidateResponse(RestResponse response)
    {
        if (response.IsSuccessful)
        {
            return;
        }

        throw new ApiException(
            (int)response.StatusCode,
            response.ErrorMessage ??
            response.Content ??
            "Unknown API error.");
    }

    private static void LogRequest(RestRequest request)
    {
        LoggerManager.Logger.Information(
            "HTTP {Method} {Endpoint}",
            request.Method,
            request.Resource);
    }

    private static void LogResponse<T>(
        RestResponse<T> response,
        TimeSpan duration)
    {
        LoggerManager.Logger.Information(
            "HTTP {StatusCode} ({Elapsed} ms)",
            (int)response.StatusCode,
            duration.TotalMilliseconds);
    }
}