using DSharpPlus;
using fixedhitbox.DiscordBot.Events.Updates;
using fixedhitbox.DiscordBot.Interactions.Accounts.Aredl;

namespace fixedhitbox.DiscordBot.Events;

internal static class DiscordEvents
{
    
    public static async Task RegisterAll(DiscordClientBuilder builder)
    {
        try
        {
            await OnBotConnecting.UpdateAsync(builder);
            
            var interaction = new LinkAccountInteraction();
            await interaction.RegisterAsync(builder);
        }
        catch (Exception ex)
        {
            await Task.FromException(ex);
        }
    }
}