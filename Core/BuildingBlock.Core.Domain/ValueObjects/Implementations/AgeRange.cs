using BuildingBlock.Core.Domain.ValueObjects.Abstractions;

namespace BuildingBlock.Core.Domain.ValueObjects.Implementations;

public class AgeRange : PositiveIntegerRange
{
    public AgeRange(int minValue, int maxValue) : base(minValue, maxValue)
    {
    }
}