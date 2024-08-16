using BuildingBlock.Core.Domain.Exceptions;
using BuildingBlock.Core.Domain.ValueObjects.Implementations;
using FluentAssertions;

namespace Tests.Core.Domain.ValueObjects.Implementations;

public class AgeRangeTest
{
    public class ShouldThrowValidationException
    {
        [Fact]
        public void WhenMinimumValueIsSmallerThanZero()
        {
            // Arrange
            const int minValue = -1;
            const int maxValue = 1;

            // Act
            var action = () => { _ = new AgeRange(minValue, maxValue); };

            // Assert
            action.Should().Throw<ValidationException>()
                .WithMessage("The minimum value of the age range: -1 must be greater than 0.");
        }

        [Fact]
        public void WhenMinimumValueIsGreaterThanMaximumValue()
        {
            // Arrange
            const int minValue = 2;
            const int maxValue = 1;

            // Act
            var action = () => { _ = new AgeRange(minValue, maxValue); };

            // Assert
            action.Should().Throw<ValidationException>()
                .WithMessage("The maximum value of the age range: 1 must be greater than the minimum value: 2.");
        }
    }

    public class ShouldCreateNewAgeRangeInstance
    {
        [Fact]
        public void WhenMaximumValueIsGreaterThanMinimumValue()
        {
            // Arrange
            const int minValue = 1;
            const int maxValue = 2;

            // Act
            var ageRange = new AgeRange(minValue, maxValue);

            // Assert
            ageRange.MinValue.Should().Be(minValue);
            ageRange.MaxValue.Should().Be(maxValue);
        }
    }
}