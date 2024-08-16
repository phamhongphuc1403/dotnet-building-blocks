using BuildingBlock.Core.Domain.Rules.Implementations;
using FluentAssertions;

namespace Tests.Core.Domain.Rules;

public class NumberMustBeGreaterThanZeroTest
{
    public class ShouldReturnTrue
    {
        public class WhenValueIsSmallerThanZero
        {
            [Fact]
            public void CaseInteger()
            {
                // Arrange
                var rule = new NumberMustBeGreaterThanZero(-1);

                // Act
                var result = rule.IsBroken();

                // Assert
                result.Should().BeTrue();
                rule.Message.Should().Be("-1 must be greater than 0.");
            }

            [Fact]
            public void CaseDouble()
            {
                // Arrange
                var rule = new NumberMustBeGreaterThanZero(-1.1, "value");

                // Act
                var result = rule.IsBroken();

                // Assert
                result.Should().BeTrue();
                rule.Message.Should().Be("value: -1,1 must be greater than 0.");
            }
        }
    }

    public class ShouldReturnFalse
    {
        public class WhenValueIsLargerThanZero
        {
            [Fact]
            public void CaseInteger()
            {
                // Arrange
                var rule = new NumberMustBeGreaterThanZero(1, "value");

                // Act
                var result = rule.IsBroken();

                // Assert
                result.Should().BeFalse();
                rule.Message.Should().BeNull();
            }

            [Fact]
            public void CaseDouble()
            {
                // Arrange
                var rule = new NumberMustBeGreaterThanZero(1.1);

                // Act
                var result = rule.IsBroken();

                // Assert
                result.Should().BeFalse();
                rule.Message.Should().BeNull();
            }
        }
    }
}