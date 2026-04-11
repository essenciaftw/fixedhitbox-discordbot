using fixedhitbox.Application.DTOs;
using fixedhitbox.Application.Interfaces.Application.Aredl;
using fixedhitbox.Shared;

namespace fixedhitbox.Application.UseCases.Inspect;

internal sealed class InspectAredlPlayer : IInspectAredlPlayer
{
    public Task<ResultData<AredlProfileDto>> TrackProfile(
        ulong discordId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}