using BuildingBlock.Core.Domain.Rules.Abstractions;

namespace BuildingBlock.Core.Domain.Rules.Implementations;

public class StringCanNotBeEmptyOrWhiteSpaceRule : IBusinessRule
{
    private readonly string _name;
    private readonly string? _value;

    protected StringCanNotBeEmptyOrWhiteSpaceRule(string? value, string name)
    {
        _value = value;
        _name = name;
    }

    public bool IsBroken()
    {
        return string.IsNullOrEmpty(_value) || string.IsNullOrWhiteSpace(_value);
    }

    public string Message => $"{_name} can not be empty or contain only white spaces.";
}