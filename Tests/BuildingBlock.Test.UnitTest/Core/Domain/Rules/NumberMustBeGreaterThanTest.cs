using BuildingBlock.Core.Domain.Rules.Implementations;
using FluentAssertions;

namespace Tests.Core.Domain.Rules;

public class NumberMustBeGreaterThanTest
{
    public class ShouldReturnFalse
    {
        public class WhenValueIsLargerThanCompareValue
        {
            [Fact]
            public void CaseInteger()
            {
                // Arrange
                var numberMustLargerThanZero = new NumberMustBeGreaterThan(1, 0);

                // Act
                var result = numberMustLargerThanZero.IsBroken();

                // Assert
                result.Should().BeFalse();
                numberMustLargerThanZero.Message.Should().BeNull();
            }

            [Fact]
            public void CaseDouble()
            {
                // Arrange
                var numberMustLargerThanZero = new NumberMustBeGreaterThan(1.1, 0.1);

                // Act
                var result = numberMustLargerThanZero.IsBroken();

                // Assert
                result.Should().BeFalse();
                numberMustLargerThanZero.Message.Should().BeNull();
            }
        }
    }

    public class ShouldReturnTrue
    {
        public class WhenValueIsNotLargerThanCompareValue
        {
            [Fact]
            public void CaseInteger()
            {
                // Arrange
                var numberMustLargerThanZero = new NumberMustBeGreaterThan(1, 1, "value", "compare value");

                // Act
                var result = numberMustLargerThanZero.IsBroken();

                // Assert
                result.Should().BeTrue();
                numberMustLargerThanZero.Message.Should().Be("value: 1 must be greater than compare value: 1.");
            }

            [Fact]
            public void CaseDouble()
            {
                // Arrange
                var numberMustLargerThanZero = new NumberMustBeGreaterThan(0.111111111111, 1.111111111111);

                // Act
                var result = numberMustLargerThanZero.IsBroken();

                // Assert
                result.Should().BeTrue();
                numberMustLargerThanZero.Message.Should().Be("0,111111111111 must be greater than 1,111111111111.");
            }
        }
    }
}