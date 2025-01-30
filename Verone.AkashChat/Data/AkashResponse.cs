using System.Text.Json.Serialization;

namespace Verone.AkashChat.Data;

public sealed record AkashResponse
{
    [JsonPropertyName("choices")] public List<AkashChoice> Choices { get; init; } = [];
}