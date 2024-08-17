using BuildingBlock.Core.Domain.Rules.Abstractions;

namespace BuildingBlock.Core.Domain.Rules.Implementations;

public class StringMustContainOnlyLetters : IBusinessRule
{
    private readonly string _name;
    private readonly string _value;

    public StringMustContainOnlyLetters(string value, string name)
    {
        _value = value;
        _name = name;
    }

    public bool IsBroken()
    {
        if (_value.All(char.IsLetter)) return false;

        Message = $"{_name} must contain only letters";

        return true;
    }

    public string? Message { get; private set; }
}