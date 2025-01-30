using System.Text.Json.Serialization;

namespace Verone.AkashChat.Data;

public sealed record AkashMessage(
    [property: JsonPropertyName("role")] string Role,
    [property: JsonPropertyName("content")] string Content);