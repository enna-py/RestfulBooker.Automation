using AwesomeAssertions;
using RestfulBooker.Data.DTO.Common;
using System.Net;

namespace RestfulBooker.Core.Extensions;

public static class ApiResponseExtensions
{
    public static void ShouldBeSuccessful<T>(
        this ApiResponse<T> response)
    {
        response.Should().NotBeNull();

        response.IsSuccessful.Should().BeTrue();
    }

    public static void ShouldHaveStatus<T>(
        this ApiResponse<T> response,
        HttpStatusCode statusCode)
    {
        response.StatusCode.Should().Be(statusCode);
    }

    public static void ShouldHaveData<T>(
        this ApiResponse<T> response)
    {
        response.Data.Should().NotBeNull();
    }

    public static void ShouldBeOk<T>(
    this ApiResponse<T> response)
    {
        response.ShouldBeSuccessful();

        response.ShouldHaveStatus(HttpStatusCode.OK);
    }
}