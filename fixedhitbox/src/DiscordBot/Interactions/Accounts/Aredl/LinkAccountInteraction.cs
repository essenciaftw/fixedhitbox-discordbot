using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using fixedhitbox.Application.DTOs;
using fixedhitbox.Application.Interfaces.Application.Aredl;
using fixedhitbox.DiscordBot.Utils;
using fixedhitbox.Domain.Enums;
using fixedhitbox.HostedService;
using fixedhitbox.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace fixedhitbox.DiscordBot.Interactions.Accounts.Aredl;

internal class LinkAccountInteraction
{
    internal Task RegisterAsync(DiscordClientBuilder builder)
    {
        builder.ConfigureEventHandlers(bot =>
        {
            bot.HandleComponentInteractionCreated(async (_, ev) =>
            {
                var cancelLinkAredl = BotServices.Provider
                    .GetRequiredService<ICancelLinkAredl>();
                
                var locale = ev.Interaction.Locale;

                switch (ev.Id)
                {
                    case "cancel_link":
                    {
                        var result = await cancelLinkAredl.CancelLinkAsync(ev.User.Id);
                        await HandleCancellationKeys(result, locale, ev);
                    } 
                        
                        break;
                }
            });
        });
        
        return Task.CompletedTask;
    }

    private static async Task HandleCancellationKeys(
        ResultData<PendingAredlLinkDto> result,
        string? locale,
        ComponentInteractionCreatedEventArgs ev)
    {
        switch (result.Status)
        {

            case EResultStatus.CacheExpired:
                await ev.Interaction.CreateResponseAsync(
                    DiscordInteractionResponseType.UpdateMessage,
                    new DiscordInteractionResponseBuilder()
                        .WithContent(BotLocalizer.Get("Aredl_Interaction_ConfirmLink_CacheExpired",
                            locale)));
                break;

            case EResultStatus.AlreadyLinked:
                await ev.Interaction.CreateResponseAsync(
                    DiscordInteractionResponseType.UpdateMessage,
                    new DiscordInteractionResponseBuilder()
                        .WithContent(BotLocalizer.Get("Aredl_Interaction_CancelLink_AlreadyLinked",
                            locale)));
                break;

            case EResultStatus.PendingConfirmation:
                await ev.Interaction.CreateResponseAsync(
                    DiscordInteractionResponseType.UpdateMessage,
                    new DiscordInteractionResponseBuilder()
                        .WithContent(BotLocalizer.Get("Aredl_Interaction_CancelLink_Success", 
                            locale)));
                break;
        }
    }
}