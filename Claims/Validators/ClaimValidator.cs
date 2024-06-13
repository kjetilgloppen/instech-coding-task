using System.ComponentModel.DataAnnotations;

namespace Claims.Validators;

public class ClaimValidator
{
    private const decimal MaxDamageCost = 100000;

    public static void Validate(Claim claim, Cover? cover)
    {
        if (cover == null)
        {
            throw new ValidationException("No related Cover found for the new Claim");
        }

        if (claim.Created < cover.StartDate || claim.Created > cover.EndDate)
        {
            throw new ValidationException("Created date must be within the period of the related Cover");
        }

        if (claim.DamageCost > MaxDamageCost)
        {
            throw new ValidationException($"DamageCost cannot exceed {MaxDamageCost}");
        }
    }
}
