using fixedhitbox.Application.DTOs;
using fixedhitbox.Shared.Results.Cache;

namespace fixedhitbox.Application.Interfaces.Infra.Aredl;

public interface IAredlCache
{
    
    ResultCacheData<PendingAredlLinkDto> TryGetAny(ulong discordId);
    ResultCacheData<AredlProfileDto> GetProfile(ulong discordId);
    
    void SetProfile(ulong discordId, AredlProfileDto profile);
    void SetPendingLink(ulong discordId, PendingAredlLinkDto pending);
    void SetLinkedUser(ulong discordId, PendingAredlLinkDto linked);
    void SetNotFound(ulong discordId);
    
    void RemovePending(ulong discordId);
    void RemoveLinked(ulong discordId);
    void RemoveNotFound(ulong discordId);
    
}