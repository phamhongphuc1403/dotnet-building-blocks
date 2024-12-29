using BuildingBlock.Core.Domain.Rules.Implementations;
using FluentAssertions;

namespace Tests.Core.Domain.Rules;

public class StringMustNotContainAnyWhiteSpaceTest
{
    public class ShouldReturnTrue
    {
        [Fact]
        public void WhenStringContainsWhiteSpace()
        {
            // Arrange
            var stringMustNotContainAnyWhiteSpace =
                new StringMustNotContainAnyWhiteSpace("String with white space", "string");

            // Act
            var result = stringMustNotContainAnyWhiteSpace.IsBroken();

            // Assert
            result.Should().BeTrue();
            stringMustNotContainAnyWhiteSpace.Message.Should()
                .Be("string with value: 'String with white space' can not contain any white space.");
        }
    }

    public class ShouldReturnFalse
    {
        [Fact]
        public void WhenStringDoesNotContainWhiteSpace()
        {
            // Arrange
            var stringMustNotContainAnyWhiteSpace =
                new StringMustNotContainAnyWhiteSpace("StringWithoutWhiteSpace", "string");

            // Act
            var result = stringMustNotContainAnyWhiteSpace.IsBroken();

            // Assert
            result.Should().BeFalse();
        }
    }
}