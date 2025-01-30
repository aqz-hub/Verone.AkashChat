using Microsoft.Extensions.Options;

namespace Verone.AkashChat.Options;

public sealed class AkashChatOptionsValidator : IValidateOptions<AkashChatOptions>
{
    public ValidateOptionsResult Validate(string? name, AkashChatOptions options)
    {
        if (string.IsNullOrEmpty(options.Url))
        {
            return ValidateOptionsResult.Fail("Base URL is not configured");
        }
        
        if (string.IsNullOrEmpty(options.Model))
        {
            return ValidateOptionsResult.Fail("Model name is not configured");
        }
        
        if (string.IsNullOrEmpty(options.Token))
        {
            return ValidateOptionsResult.Fail("JWT Token is not configured");
        }
        
        return ValidateOptionsResult.Success;
    }
}