using Verone.AkashChat.Exceptions;

namespace Verone.AkashChat;

public interface IAkashChatService
{  
    /// <summary>
    /// Send message to the model
    /// </summary>
    /// <param name="message">Message</param>
    /// <returns>Response</returns>
    /// <exception cref="AkashChatException">Throws exception if the response is incorrect</exception>
    Task<string> SendMessage(string message);
}