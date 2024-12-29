using BuildingBlock.Core.Domain.Rules.Abstractions;

namespace BuildingBlock.Core.Domain.Rules.Implementations;

public class StringMustNotContainAnyWhiteSpace : IBusinessRule
{
    private readonly string _name;
    private readonly string _value;

    public StringMustNotContainAnyWhiteSpace(string value, string name)
    {
        _value = value;
        _name = name;
    }

    public string? Message { get; private set; }

    public bool IsBroken()
    {
        if (!_value.Contains(' ')) return false;

        Message = $"{_name} with value: '{_value}' can not contain any white space.";

        return true;
    }
}