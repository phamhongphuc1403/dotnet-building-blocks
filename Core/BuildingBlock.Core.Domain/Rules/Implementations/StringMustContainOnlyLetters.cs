using BuildingBlock.Core.Domain.Rules.Abstractions;

namespace BuildingBlock.Core.Domain.Rules.Implementations;

public class StringMustContainOnlyLetters : IBusinessRule
{
    private readonly string _value;

    public StringMustContainOnlyLetters(string value)
    {
        _value = value;
    }

    public bool IsBroken()
    {
        if (_value.All(char.IsLetter)) return false;

        Message = "String must only contain only letters";

        return true;
    }

    public string? Message { get; private set; }
}