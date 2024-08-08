using FluentAssertions;

namespace Tests.Core.Domain;

public class AggregateRootTest
{
    public class AddDomainEvent
    {
        public class ShouldAddDomainEvent
        {
            [Fact]
            public void WhenInvoke()
            {
                // Arrange
                var aggregateRoot = new TestAggregateRoot();
                var domainEvent = new TestDomainEvent();

                // Act
                aggregateRoot.AddDomainEvent(domainEvent);

                // Assert
                aggregateRoot.DomainEvents.Should().Contain(domainEvent);
                aggregateRoot.DomainEvents.Should().HaveCount(1);
            }
        }
    }

    public class RemoveDomainEvent
    {
        public class ShouldRemoveDomainEvent
        {
            [Fact]
            public void WhenThatDomainEventIsExist()
            {
                // Arrange
                var aggregateRoot = new TestAggregateRoot();
                var domainEvent = new TestDomainEvent();
                aggregateRoot.AddDomainEvent(domainEvent);

                // Act
                aggregateRoot.RemoveDomainEvent(domainEvent);

                // Assert
                aggregateRoot.DomainEvents.Should().NotContain(domainEvent);
                aggregateRoot.DomainEvents.Should().HaveCount(0);
            }
        }

        public class ShouldDoNothing
        {
            [Fact]
            public void WhenThatDomainEventIsNotExist()
            {
                // Arrange
                var aggregateRoot = new TestAggregateRoot();
                var domainEvent = new TestDomainEvent();

                // Act
                aggregateRoot.RemoveDomainEvent(domainEvent);

                // Assert
                aggregateRoot.DomainEvents.Should().NotContain(domainEvent);
                aggregateRoot.DomainEvents.Should().HaveCount(0);
            }
        }
    }

    public class ClearDomainEvent
    {
        public class ShouldRemoveAllDomainEvents
        {
            [Fact]
            public void WhenInvoke()
            {
                // Arrange
                var aggregateRoot = new TestAggregateRoot();
                var domainEvent = new TestDomainEvent();
                aggregateRoot.AddDomainEvent(domainEvent);
                aggregateRoot.AddDomainEvent(domainEvent);
                aggregateRoot.AddDomainEvent(domainEvent);
                aggregateRoot.AddDomainEvent(domainEvent);

                // Act
                aggregateRoot.ClearDomainEvents();

                // Assert
                aggregateRoot.DomainEvents.Should().NotContain(domainEvent);
                aggregateRoot.DomainEvents.Should().HaveCount(0);
            }
        }
    }
}