using RBP.Data.DTO.Message;
using RestfulBooker.Api.Base;
using RestfulBooker.Api.Endpoints;
using RestfulBooker.Core.Authentication;
using RestfulBooker.Core.Configuration;
using RestfulBooker.Core.Exceptions;
using RestfulBooker.Data.DTO.Common;

namespace RBP.Business.Api.Clients;

public sealed class MessageApiClient : BaseApiClient
{
    public MessageApiClient(AuthenticationState authState)
        : base(ConfigurationService.Current.Api.MessageUrl, authState)
    {
    }

    public async Task<IReadOnlyCollection<MessageListItemDto>> GetMessagesAsync()
    {
        ApiResponse<MessageListResponse> response =
            await GetAsync<MessageListResponse>(MessageEndpoints.Messages);

        return response.Data?.Messages ?? [];
    }

    public async Task<MessageDto> GetMessageAsync(int messageId)
    {
        ApiResponse<MessageDto> response =
            await GetAsync<MessageDto>(MessageEndpoints.ById(messageId));

        if (response.Data is null)
        {
            throw new ApiException(
                (int)response.StatusCode,
                $"Message '{messageId}' was not returned.");
        }

        return response.Data;
    }

    public async Task<ApiResponse<object>> DeleteMessageAsync(
        int messageId,
        bool validateResponse = true)
    {
        return await DeleteAsync<object>(
            MessageEndpoints.ById(messageId),
            validateResponse);
    }
}
