namespace Verone.AkashChat.Options;

public sealed class AkashChatOptions
{
    /// <summary>
    /// Base URL for Chat API
    /// </summary>
    public string Url { get; set; } = null!;
    
    /// <summary>
    /// Model name
    /// </summary>
    public string Model { get; set; } = null!;
    
    /// <summary>
    /// JWT Token for authorization
    /// </summary>
    public string Token { get; set; } = null!;
}