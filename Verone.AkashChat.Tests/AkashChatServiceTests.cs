using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Shouldly;
using Verone.AkashChat.Data;
using Verone.AkashChat.Exceptions;
using Verone.AkashChat.Options;

namespace Verone.AkashChat.Tests;

public sealed class AkashChatServiceTests
{
    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;
    private readonly IAkashChatService _service;
    
    public AkashChatServiceTests()
    {
        _httpMessageHandlerMock = new Mock<HttpMessageHandler>();
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        httpClientFactoryMock.Setup(x => x.CreateClient(AkashChatDefaults.HttpClientName))
            .Returns(new HttpClient(_httpMessageHandlerMock.Object));
        
        var options = new Mock<IOptions<AkashChatOptions>>();
        options.Setup(x => x.Value).Returns(new AkashChatOptions());
        options.Object.Value.Model = AkashChatDefaults.Model;
        options.Object.Value.Url = AkashChatDefaults.Url;
        
        _service = new AkashChatService(httpClientFactoryMock.Object, options.Object);
    }

    [Fact]
    public async Task SendMessage_ValidResponse_ShouldReturnExpectedMessage()
    {
        const string expectedMessage = "Test response message";
        var responseJson = JsonSerializer.Serialize(new AkashResponse
        {
            Choices =
            [
                new AkashChoice
                {
                    Message = new AkashMessage("assistant", expectedMessage)
                }
            ]
        });

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseJson)
            });
        
        var result = await _service.SendMessageAsync("Test request message");
        
        result.ShouldBe(expectedMessage);
    }

    [Fact]
    public async Task SendMessage_ResponseIsEmpty_AkashChatException()
    {
        var responseJson = JsonSerializer.Serialize(new AkashResponse
        {
            Choices = []
        });

        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(responseJson)
            });
        
        await Should.ThrowAsync<AkashChatException>(() => _service.SendMessageAsync("Test request message"));
    }

    [Fact]
    public async Task SendMessage_FailedToGetResponse_AkashChatException()
    {
        const string errorMessage = "Test error response";
        _httpMessageHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>()
            )
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(errorMessage)
            });

        await Should.ThrowAsync<AkashChatException>(() => _service.SendMessageAsync("Test request message"));
    }
}