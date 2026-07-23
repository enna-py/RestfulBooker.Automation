using RestSharp;

namespace RestfulBooker.Api.Factories;

internal static class RestClientFactory
{
    public static RestClient Create(string baseUrl)
    {
        RestClientOptions options = new()
        {
            BaseUrl = new Uri(baseUrl),
            ThrowOnAnyError = false,
            Timeout = TimeSpan.FromSeconds(30)
        };

        return new RestClient(options);
    }
}