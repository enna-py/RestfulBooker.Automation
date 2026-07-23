using RestfulBooker.Core.Authentication;
using RestSharp;

namespace RestfulBooker.Api.Factories;

public static class RequestFactory
{
    private static RestRequest CreateRequest(
    string endpoint,
    Method method)
    {
        RestRequest request = new(endpoint, method);

        AddDefaultHeaders(request);

        AddAuthorization(request);

        return request;
    }

    private static void AddDefaultHeaders(RestRequest request)
    {
        request.AddHeader("Accept", "application/json");
    }

    public static RestRequest CreateGet(string endpoint)
    {
        return CreateRequest(endpoint, Method.Get);
    }

    public static RestRequest CreateDelete(string endpoint)
    {
        return CreateRequest(endpoint, Method.Delete);
    }

    public static RestRequest CreatePost<T>(string endpoint,T body) where T : class
    {
        RestRequest request = CreateRequest(endpoint, Method.Post);

        request.AddJsonBody(body);

        return request;
    }

    public static RestRequest CreatePut<T>(string endpoint, T body) where T : class
    {
        RestRequest request = CreateRequest(endpoint, Method.Put);

        request.AddJsonBody(body);

        return request;
    }

    private static void AddAuthorization(RestRequest request)
    {
        if (!TokenProvider.IsAuthenticated)
        {
            return;
        }

        request.AddHeader(
            "Authorization",
            $"Bearer {TokenProvider.Token}");
    }
}