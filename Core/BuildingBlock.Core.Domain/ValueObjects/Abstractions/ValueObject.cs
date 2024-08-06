using BuildingBlock.Core.Domain.Exceptions;
using BuildingBlock.Core.Domain.Rules.Abstractions;

namespace BuildingBlock.Core.Domain.ValueObjects.Abstractions;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public bool Equals(ValueObject? other)
    {
        return other is not null && EqualsTo(other);
    }

    protected abstract IEnumerable<object?> GetValues();

    private bool EqualsTo(ValueObject other)
    {
        return GetValues().SequenceEqual(other.GetValues());
    }

    public static void CheckRule(IBusinessRule rule)
    {
        if (rule.IsBroken()) throw new ValidationException(rule.Message!);
    }
}