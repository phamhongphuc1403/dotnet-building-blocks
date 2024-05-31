using BuildingBlock.Core.Domain.Rules.Implementations;
using BuildingBlock.Core.Domain.ValueObjects.Abstractions;

namespace BuildingBlock.Core.Domain.ValueObjects.Implementations;

public sealed class EmailAddress : ValueObject
{
    public EmailAddress(string value)
    {
        CheckRule(new EmailAddressMustFollowPattern(value));
        Value = value;
    }

    public string Value { get; }

    protected override IEnumerable<object?> GetValues()
    {
        yield return Value;
    }
}