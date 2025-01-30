using System.Text.Json.Serialization;

namespace Verone.AkashChat.Data;

public sealed record AkashChoice
{
    [JsonPropertyName("message")] public AkashMessage Message { get; init; } = null!;
}