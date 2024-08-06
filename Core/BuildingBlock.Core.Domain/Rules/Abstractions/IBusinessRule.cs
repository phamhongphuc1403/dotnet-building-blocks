namespace BuildingBlock.Core.Domain.Rules.Abstractions;

public interface IBusinessRule
{
    string? Message { get; }
    bool IsBroken();
}