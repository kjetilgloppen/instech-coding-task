using System.ComponentModel.DataAnnotations;
using Claims.Validators;
using Xunit;

namespace Claims.Tests;
public class CoverValidatorTests
{
    [Theory]
    [InlineData(1, true)]
    [InlineData(100, true)]
    [InlineData(400, false)]
    [InlineData(-10, false)]
    public void Test_Cover(int durationInDays, bool expectedSuccess)
    {
        var startDate = DateTime.UtcNow.AddDays(1);
        var cover = new Cover
        {
            StartDate = startDate,
            EndDate = startDate.AddDays(durationInDays),
        };

        if (expectedSuccess)
        {
            // Not really needed, but makes it more explicit what is tested -> not throwing
            var exception = Record.Exception(() => CoverValidator.Validate(cover));
            Assert.Null(exception);
        }
        else
        {
            Assert.Throws<ValidationException>(() => CoverValidator.Validate(cover));
        }
    }
}
