using BuildingBlock.Core.Domain.Specifications.Abstractions;
using FluentAssertions;

namespace Tests.Core.Domain.Specifications.Abstractions;

public class AndSpecificationTest
{
    public class ShouldReturnTrue
    {
        [Fact]
        public void WhenBothSpecificationsIsTrue()
        {
            // Arrange
            var spec1 = new TestSpecification(true);
            var spec2 = new TestSpecification(true);
            var andSpec = new AndSpecification<TestEntity>(spec1, spec2);

            // Act
            var result = andSpec.ToExpression().Compile()(new TestEntity());

            // Assert
            result.Should().BeTrue();
        }
    }

    public class ShouldReturnFalse
    {
        [Fact]
        public void WhenOneSpecificationIsFalse()
        {
            // Arrange
            var spec1 = new TestSpecification(true);
            var spec2 = new TestSpecification(false);
            var andSpec = new AndSpecification<TestEntity>(spec1, spec2);

            // Act
            var result = andSpec.ToExpression().Compile()(new TestEntity());

            // Assert
            result.Should().BeFalse();
        }
    }
}