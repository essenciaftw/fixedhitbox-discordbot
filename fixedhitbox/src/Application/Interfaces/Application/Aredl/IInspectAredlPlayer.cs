using fixedhitbox.Application.DTOs;
using fixedhitbox.Shared;

namespace fixedhitbox.Application.Interfaces.Application.Aredl;

internal interface IInspectAredlPlayer
{
    internal Task<ResultData<AredlProfileDto>> TrackProfile(
        ulong discordId, CancellationToken cancellationToken = default);
}