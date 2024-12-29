using BuildingBlock.Core.Domain.Rules.Abstractions;

namespace BuildingBlock.Core.Domain.Rules.Implementations;

public class StringMustNotBeEmptyOrWhiteSpace : IBusinessRule
{
    private readonly string _name;
    private readonly string? _value;


    public StringMustNotBeEmptyOrWhiteSpace(string? value, string name)
    {
        _value = value;
        _name = name;
    }

    public string? Message { get; private set; }

    public bool IsBroken()
    {
        if (!string.IsNullOrEmpty(_value) && !string.IsNullOrWhiteSpace(_value)) return false;

        Message = $"{_name} with value: '{_value}' can not be empty or contain only white spaces.";
        return true;
    }
}