using fixedhitbox.Application.DTOs;
using fixedhitbox.Shared;

namespace fixedhitbox.Application.Interfaces.Application.Aredl;

internal interface IStartLinkAredl
{
    internal Task<ResultData<PendingAredlLinkDto>> StartLinkAsync(
        ulong discordId, CancellationToken cancellationToken = default);
}