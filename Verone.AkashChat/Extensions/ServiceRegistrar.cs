using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Verone.AkashChat.Options;

namespace Verone.AkashChat.Extensions;

public static class AkashServiceRegistrar
{
    public static IServiceCollection AddAkashChat(this IServiceCollection services, Action<AkashChatOptions> options)
    {
        services.Configure(options);

        services.AddOptionsWithValidateOnStart<AkashChatOptions>();
        
        services.TryAddScoped<IAkashChatService, AkashChatService>();
        
        services.AddHttpClient(AkashChatDefaults.HttpClientName);

        return services;
    }
}