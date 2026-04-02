using fixedhitbox.Dtos.Aredl;
using fixedhitbox.Services.Results;

namespace fixedhitbox.Services.Interfaces;

public interface IAredlApiService
{

    Task<AredlApiResult<AredlProfileResponse>> GetProfileAsync(
        ulong discordId,
        CancellationToken cancellationToken);
}