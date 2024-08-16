namespace BuildingBlock.Core.Domain.Rules.Implementations;

public class NumberMustBeGreaterThanZero : NumberMustBeGreaterThan
{
    public NumberMustBeGreaterThanZero(int value) : base(value, 0)
    {
    }

    public NumberMustBeGreaterThanZero(double value) : base(value, 0)
    {
    }

    public NumberMustBeGreaterThanZero(int value, string? valueName = null) : base(value, 0, valueName)
    {
    }

    public NumberMustBeGreaterThanZero(double value, string? valueName = null) : base(value, 0, valueName)
    {
    }
}