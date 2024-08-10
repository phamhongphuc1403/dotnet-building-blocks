using BuildingBlock.Core.Domain.Specifications.Implementations;
using FluentAssertions;

namespace Tests.Core.Domain.Specifications.Implementations;

public class EntityIdNotEqualSpecificationTest
{
    public class ShouldReturnTrue
    {
        [Fact]
        public void WhenEntityIdIsNotEqual()
        {
            // Arrange
            var entity = new TestEntity { Id = Guid.NewGuid() };
            var entityIdSpecification = new EntityIdNotEqualSpecification<TestEntity>(Guid.NewGuid());

            // Act
            var result = entityIdSpecification.ToExpression().Compile()(entity);

            // Assert
            result.Should().BeTrue();
        }
    }

    public class ShouldReturnFalse
    {
        [Fact]
        public void WhenEntityIdIsEqual()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new TestEntity { Id = id };
            var entityIdSpecification = new EntityIdNotEqualSpecification<TestEntity>(id);

            // Act
            var result = entityIdSpecification.ToExpression().Compile()(entity);

            // Assert
            result.Should().BeFalse();
        }
    }
}