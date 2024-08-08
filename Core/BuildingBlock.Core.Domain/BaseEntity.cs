using BuildingBlock.Core.Domain.DomainEvents;

namespace BuildingBlock.Core.Domain;

public interface IBaseEntity<TKey>
{
    TKey Id { get; set; }
}

public interface IEntity : IBaseEntity<Guid>
{
}

public interface IAggregateRoot : IEntity
{
}

public abstract class Entity : IEntity
{
    public Guid Id { get; set; }
}

public abstract class AggregateRoot : Entity, IAggregateRoot
{
    public List<IDomainEvent> DomainEvents { get; } = [];

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        DomainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        DomainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        DomainEvents.Clear();
    }
}