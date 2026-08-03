using System.Net;

namespace RestfulBooker.Data.DTO.Common;

public sealed class ApiResponse<T>
{
    public T? Data { get; init; }

    public string? Content { get; init; }

    public HttpStatusCode StatusCode { get; init; }

    public bool IsSuccessful { get; init; }

    public TimeSpan Duration { get; init; }

    public string? ErrorMessage { get; init; }
}