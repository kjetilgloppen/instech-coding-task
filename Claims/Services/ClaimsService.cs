using System.ComponentModel.DataAnnotations;
using Claims.Repositories;
using Claims.Validators;

namespace Claims.Services;

public class ClaimsService
{
    private readonly ClaimsRepository _claimsRepository;
    private readonly CoversRepository _coversRepository;

    public ClaimsService(ClaimsRepository claimsRepository, CoversRepository coversRepository)
    {
        _claimsRepository = claimsRepository;
        _coversRepository = coversRepository;
    }

    public async Task<IEnumerable<Claim>> GetAllAsync()
    {
        return await _claimsRepository.GetAllAsync();
    }

    public async Task<Claim?> GetAsync(string id)
    {
        return await _claimsRepository.GetAsync(id);
    }

    public async Task<Claim> CreateAsync(Claim claim)
    {
        await Validate(claim);
        await _claimsRepository.CreateAsync(claim);
        return claim;
    }

    public async Task DeleteAsync(string id)
    {
        await _claimsRepository.DeleteAsync(id);
    }

    private async Task Validate(Claim claim)
    {
        if (claim.CoverId == null)
        {
            throw new ValidationException("Cover Id cannot be null");
        }

        var cover = await _coversRepository.GetAsync(claim.CoverId);
        ClaimValidator.Validate(claim, cover);
    }
}
