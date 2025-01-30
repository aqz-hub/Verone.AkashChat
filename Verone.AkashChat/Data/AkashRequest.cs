namespace Verone.AkashChat.Data;

public sealed record AkashRequest
{
    public AkashRequest(string model, string message)
    {
        Model = model;
        Messages = [new AkashMessage("User", message)];
    }
    
    public string Model { get; }
    public AkashMessage[] Messages { get; set; } = [];
}