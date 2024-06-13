using Claims.Helpers;
using Xunit;

namespace Claims.Tests;
public class PremiumCalculationTests
{
    [Theory]
    [InlineData(0, CoverType.Yacht, 0)]
    [InlineData(10, CoverType.Yacht, 10 * 1250 * 1.1)]
    [InlineData(10, CoverType.PassengerShip, 10 * 1250 * 1.2)]
    [InlineData(100, CoverType.Yacht, (30 * 1250 * 1.1) + (70 * 1250 * 1.1 * 0.95))]
    [InlineData(100, CoverType.PassengerShip, (30 * 1250 * 1.2) + (70 * 1250 * 1.2 * 0.98))]
    [InlineData(200, CoverType.Yacht, (30 * 1250 * 1.1) + (150 * 1250 * 1.1 * 0.95) + (20 * 1250 * 1.1 * 0.92))]
    [InlineData(200, CoverType.PassengerShip, (30 * 1250 * 1.2) + (150 * 1250 * 1.2 * 0.98) + (20 * 1250 * 1.2 * 0.97))]
    public void Calculate_Premium(int durationInDays, CoverType coverType, decimal expectedPremium)
    {
        var startDate = DateTime.UtcNow;
        var endDate = startDate.AddDays(durationInDays);

        var premium = CoversHelper.ComputePremium(startDate, endDate, coverType);
        Assert.Equal(expectedPremium, premium);
    }
}
