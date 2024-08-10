using BuildingBlock.Core.Domain.Specifications.Implementations;
using FluentAssertions;

namespace Tests.Core.Domain.Specifications.Implementations;

public class EntityIdSpecificationTest
{
    public class ShouldReturnTrue
    {
        [Fact]
        public void WhenEntityIdIsEqual()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new TestEntity { Id = id };
            var entityIdSpecification = new EntityIdSpecification<TestEntity>(id);

            // Act
            var result = entityIdSpecification.ToExpression().Compile()(entity);

            // Assert
            result.Should().BeTrue();
        }
    }

    public class ShouldReturnFalse
    {
        [Fact]
        public void WhenEntityIdIsNotEqual()
        {
            // Arrange
            var entity = new TestEntity { Id = Guid.NewGuid() };
            var entityIdSpecification = new EntityIdSpecification<TestEntity>(Guid.NewGuid());

            // Act
            var result = entityIdSpecification.ToExpression().Compile()(entity);

            // Assert
            result.Should().BeFalse();
        }
    }
}