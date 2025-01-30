namespace Verone.AkashChat.Exceptions;

public sealed class AkashChatException(string message) : ApplicationException(message);