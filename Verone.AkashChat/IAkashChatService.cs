using Verone.AkashChat.Exceptions;

namespace Verone.AkashChat;

public interface IAkashChatService
{  
    /// <summary>
    /// Send message to the model
    /// </summary>
    /// <param name="message">Message</param>
    /// <param name="ct">Cancellation token</param>
    /// <returns>Response</returns>
    /// <exception cref="AkashChatException">Throws exception if the response is incorrect</exception>
    Task<string> SendMessageAsync(string message, CancellationToken ct = default);
}