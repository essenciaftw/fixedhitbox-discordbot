using fixedhitbox.Application.DTOs;
using fixedhitbox.Shared;

namespace fixedhitbox.Application.Interfaces.Application.Aredl;

public interface ICancelLinkAredl
{
    internal Task<ResultData<PendingAredlLinkDto>> CancelLinkAsync(ulong discordId, CancellationToken cancelToken = default);
}