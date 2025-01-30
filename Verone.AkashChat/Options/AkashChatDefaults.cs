namespace Verone.AkashChat.Options;

public static class AkashChatDefaults
{
    /// <summary>
    /// Default base URL for Chat API
    /// </summary>
    public const string Url = "https://chatapi.akash.network/api/v1/chat/completions";
    
    /// <summary>
    /// Default model name
    /// </summary>
    public const string Model = "DeepSeek-R1";

    /// <summary>
    /// Default Http client name for Akash Chat
    /// </summary>
    public const string HttpClientName = "akach-chat-http-client";
}