using System.ComponentModel.DataAnnotations;

namespace Claims.Validators;

public class CoverValidator
{
    public static void Validate(Cover cover)
    {
        if (cover.StartDate < DateTime.UtcNow)
        {
            throw new ValidationException("Cover start date cannot be in the past");
        }

        if (cover.EndDate <= cover.StartDate)
        {
            throw new ValidationException("Cover end date cannot be before or same as the start date");
        }

        if ((cover.EndDate - cover.StartDate).TotalDays > 365)
        {
            throw new ValidationException("Total insurance period cannot exceed 1 year");
        }
    }
}
