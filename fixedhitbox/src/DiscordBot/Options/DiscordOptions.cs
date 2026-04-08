using fixedhitbox.DiscordBot.Options.Abstractions;

namespace fixedhitbox.DiscordBot.Options;

public sealed class DiscordOptions : IDcOptions
{
    public string Token { get; init; } = string.Empty;
    public ulong DebugGuildId { get; init; }
    
    public static string SectionName => "Discord";
}