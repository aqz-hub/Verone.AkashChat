using System.Net.Http.Headers;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Verone.AkashChat.Data;
using Verone.AkashChat.Exceptions;
using Verone.AkashChat.Options;

namespace Verone.AkashChat;

public sealed class AkashChatService(
    IHttpClientFactory httpClientFactory,
    IOptions<AkashChatOptions> settings)
    : IAkashChatService
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };
    
    private readonly AkashChatOptions _settings = settings.Value;

    public async Task<string> SendMessageAsync(string message, CancellationToken ct = default)
    {
        var httpClient = httpClientFactory.CreateClient(AkashChatDefaults.HttpClientName);
        
        var content = new AkashRequest(_settings.Model, message);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(_settings.Url),
            Content = GetPayload(content)
        };
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        request.Headers.Add("Authorization", $"Bearer {_settings.Token}");

        var response = await httpClient.SendAsync(request, ct);

        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync(ct);
            var result = JsonSerializer.Deserialize<AkashResponse>(responseContent);
            if (result?.Choices is null || result.Choices.Count == 0)
                throw new AkashChatException($"Incorrect response from Akash chat: {responseContent}");

            return result.Choices.Last(x => x.Message.Role == "assistant").Message.Content;
        }

        var errorContent = await response.Content.ReadAsStringAsync(ct);
        throw new AkashChatException($"Error while getting response from chat: {errorContent}");
    }

    private StringContent GetPayload(AkashRequest request)
    {
        var jsonPayload = JsonSerializer.Serialize(request, _jsonSerializerOptions);
        return new StringContent(jsonPayload);
    }
}