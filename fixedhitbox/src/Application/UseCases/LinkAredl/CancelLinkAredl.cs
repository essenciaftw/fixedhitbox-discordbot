using fixedhitbox.Application.DTOs;
using fixedhitbox.Application.Interfaces.Application.Aredl;
using fixedhitbox.Application.Interfaces.Infra.Aredl;
using fixedhitbox.Application.Mappers.Aredl;
using fixedhitbox.Domain.Enums;
using fixedhitbox.Domain.Repositories;
using fixedhitbox.Shared;
using fixedhitbox.Shared.Results.Cache;

namespace fixedhitbox.Application.UseCases.LinkAredl;

internal sealed class CancelLinkAredl(
    IUserRepository userRepository,
    IAredlCache aredlCache) : ICancelLinkAredl
{
    public async Task<ResultData<PendingAredlLinkDto>> CancelLinkAsync(
        ulong discordId, CancellationToken cancellationToken = default)
    {
        var cacheKeys = aredlCache.TryGetAny(discordId);
        var resultCache = ReturnCacheResultIfFound(cacheKeys, out var isSatisfied);
        
        switch (isSatisfied)
        {
            case false when resultCache.Status == EResultStatus.UnexpectedError:
            case true:
                return resultCache;
        }
        
        var existingUser = await userRepository
            .GetByDiscordIdAsync(discordId, cancellationToken);

        if (existingUser is not null)
        {
            var dto = AredlProfileMapper.MapFromEntity(existingUser);
            
            if (!dto.Success) return ResultData<PendingAredlLinkDto>
                .UnexpectedError(dto.Error ?? "[CancelLinkAredl] AredlProfileMapper could not map an existing user.");
            
            aredlCache.SetLinkedUser(discordId, dto.Value!);
            return ResultData<PendingAredlLinkDto>.AlreadyLinked(dto.Value!);
        }

        return ResultData<PendingAredlLinkDto>.CacheExpired();
    }

    private ResultData<PendingAredlLinkDto> ReturnCacheResultIfFound(
            ResultCacheData<PendingAredlLinkDto> cache,
            out bool isSatisfied)
        {
            if (cache.Status == ECacheResultStatus.CacheExpired)
            {
                isSatisfied = false;
                return ResultData<PendingAredlLinkDto>.CacheExpired();
            }

            switch (cache.Status)
            {
                case ECacheResultStatus.AlreadyLinked:
                    isSatisfied = true;
                    return ResultData<PendingAredlLinkDto>.AlreadyLinked(cache.Data!);

                case ECacheResultStatus.PendingConfirmation:
                    isSatisfied = true;
                    return ResultData<PendingAredlLinkDto>.PendingConfirmation(cache.Data!);

                case ECacheResultStatus.NotFound:
                    isSatisfied = true;
                    return ResultData<PendingAredlLinkDto>.NotFound();

                case ECacheResultStatus.UnexpectedError:
                    isSatisfied = false;
                    return ResultData<PendingAredlLinkDto>.UnexpectedError(
                        "Something went wrong with AredlCache service response.");
                default:
                    isSatisfied = false;
                    return default!;
            }
        }
    }