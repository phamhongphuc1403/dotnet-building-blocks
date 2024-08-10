using BuildingBlock.Core.Domain.Specifications.Abstractions;
using FluentAssertions;

namespace Tests.Core.Domain.Specifications.Abstractions;

public class OrSpecificationTest
{
    public class ShouldReturnTrue
    {
        [Fact]
        public void WhenOneSpecificationIsTrue()
        {
            // Arrange
            var spec1 = new TestSpecification(true);
            var spec2 = new TestSpecification(false);
            var orSpec = new OrSpecification<TestEntity>(spec1, spec2);

            // Act
            var result = orSpec.ToExpression().Compile()(new TestEntity());

            // Assert
            result.Should().BeTrue();
        }
    }

    public class ShouldReturnFalse
    {
        [Fact]
        public void WhenBothSpecificationsIsFalse()
        {
            // Arrange
            var spec1 = new TestSpecification(false);
            var spec2 = new TestSpecification(false);
            var orSpec = new OrSpecification<TestEntity>(spec1, spec2);

            // Act
            var result = orSpec.ToExpression().Compile()(new TestEntity());

            // Assert
            result.Should().BeFalse();
        }
    }
}