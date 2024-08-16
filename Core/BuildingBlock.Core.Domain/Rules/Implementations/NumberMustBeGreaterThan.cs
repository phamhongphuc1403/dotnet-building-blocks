using BuildingBlock.Core.Domain.Rules.Abstractions;
using BuildingBlock.Core.Domain.Shared.Utils;

namespace BuildingBlock.Core.Domain.Rules.Implementations;

public class NumberMustBeGreaterThan : IBusinessRule
{
    private readonly object _compareValue;
    private readonly string? _compareValueName;
    private readonly object _value;
    private readonly string? _valueName;

    public NumberMustBeGreaterThan(double value, double compareValue)
    {
        _compareValue = compareValue;
        _value = value;
    }

    public NumberMustBeGreaterThan(int value, int compareValue)
    {
        _compareValue = compareValue;
        _value = value;
    }

    public NumberMustBeGreaterThan(int value, int compareValue, string? valueName = null,
        string? compareValueName = null) : this(value, compareValue)
    {
        _valueName = valueName;
        _compareValueName = compareValueName;
    }

    public NumberMustBeGreaterThan(double value, double compareValue, string? valueName = null,
        string? compareValueName = null) : this(value, compareValue)
    {
        _valueName = valueName;
        _compareValueName = compareValueName;
    }

    public string? Message { get; private set; }


    public bool IsBroken()
    {
        if (Convert.ToDouble(_value) > Convert.ToDouble(_compareValue)) return false;

        var valueName = _valueName is not null ? $"{_valueName}: " : string.Empty;
        var compareValueName = _compareValueName is not null ? $"{_compareValueName}: " : string.Empty;

        Message =
            $"{valueName}{DoubleUtility.ToString(Convert.ToDouble(_value))} must be greater than {compareValueName}{DoubleUtility.ToString(Convert.ToDouble(_compareValue))}.";

        return true;
    }
}