using System.ComponentModel.DataAnnotations;
using Claims.Validators;
using Xunit;

namespace Claims.Tests;
public class ClaimValidatorTests
{
    [Fact]
    public void Test_Claim_With_No_Cover()
    {
        var claim = new Claim();
        Assert.Throws<ValidationException>(() => ClaimValidator.Validate(claim, null));
    }

    [Theory]
    [InlineData(100, 10, true)]
    [InlineData(10, 100, false)]
    [InlineData(100, -10, false)]
    public void Test_Claim_With_Cover(int durationInDays, int claimOffsetInDays, bool expectedSuccess)
    {
        var startDate = DateTime.UtcNow.AddDays(1);
        var endDate = startDate.AddDays(durationInDays);
        var claimDate = startDate.AddDays(claimOffsetInDays);

        var claim = new Claim
        {
            Created = claimDate
        };

        var cover = new Cover
        {
            StartDate = startDate,
            EndDate = endDate,
        };

        if (expectedSuccess)
        {
            // Not really needed, but makes it more explicit what is tested -> not throwing
            var exception = Record.Exception(() => ClaimValidator.Validate(claim, cover));
            Assert.Null(exception);
        }
        else
        {
            Assert.Throws<ValidationException>(() => ClaimValidator.Validate(claim, cover));
        }
    }
}
