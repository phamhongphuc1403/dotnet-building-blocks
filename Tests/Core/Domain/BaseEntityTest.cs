using BuildingBlock.Core.Domain;
using FluentAssertions;

namespace Tests.Core.Domain;

public class EntityTest
{
    public class TestEntity : Entity
    {
        public TestEntity()
        {
            UpdatedAt = DateTime.Now;
            UpdatedBy = "Test";
        }

        public List<TestEntity> TestEntities { get; set; } = [];
        public List<int> TestInts { get; set; } = [1, 2, 3];
        public List<string> TestStrings { get; set; } = [];
        public TestEntity ChildEntity { get; set; } = null!;
    }

    public class ResetUpdatedTimeStamp
    {
        public class ShouldResetUpdatedTimeStamp
        {
            [Fact]
            public void WhenInvoke()
            {
                // Arrange
                var entity = new TestEntity();

                // Act
                entity.ResetUpdatedTimeStamp();

                // Assert
                entity.UpdatedAt.Should().BeNull();
                entity.UpdatedBy.Should().BeNull();
            }
        }
    }
}

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