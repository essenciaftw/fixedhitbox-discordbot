using fixedhitbox.Domain.Domain_DTOs;
using fixedhitbox.Shared;

namespace fixedhitbox.Application.Interfaces.Infra.Aredl;

public interface ILinkAredlService
{
    Task<ResultData<PendingAredlLink>> StartLinkAsync(
        ulong discordId, CancellationToken cancellationToken = default);
    
    Task<ResultData<PendingAredlLink>> ConfirmLinkAsync(
        ulong discordId, CancellationToken cancellationToken = default);
    
    Task<ResultData<PendingAredlLink>> CancelLinkAsync(
        ulong discordId, CancellationToken cancellationToken = default);
}