using BuildingBlock.Core.Domain.Rules.Implementations;
using BuildingBlock.Core.Domain.Shared.Utils;

namespace BuildingBlock.Core.Domain.ValueObjects.Abstractions;

public abstract class PositiveIntegerRange : ValueObject
{
    protected PositiveIntegerRange(int minValue, int maxValue)
    {
        CheckRule(new NumberMustBeGreaterThanZero(minValue, $"The minimum value of the {this.ToNormalizedName()}"));
        CheckRule(new NumberMustBeGreaterThan(maxValue, minValue, $"The maximum value of the {this.ToNormalizedName()}",
            "the minimum value"));

        MaxValue = maxValue;
        MinValue = minValue;
    }

    public int MinValue { get; }
    public int MaxValue { get; }

    protected override IEnumerable<object?> GetValues()
    {
        return new object?[] { MinValue, MaxValue };
    }
}