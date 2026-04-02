using Newtonsoft.Json;

namespace fixedhitbox.Dtos.Aredl;

public sealed record AredlProfileResponse
{
    
    public required Guid Id { get; init; }
    public required string Username { get; set; }
    
    [JsonProperty("global_name")]
    public string GlobalName { get; set; } = string.Empty;
    public ulong DiscordId { get; init; }
    public string Description { get; set; } = string.Empty;
    public int? Country { get; set; } = null;
    
    public byte BanLevel { get; set; } = 0;
    public DateTime CreatedAt { get; set; }

}