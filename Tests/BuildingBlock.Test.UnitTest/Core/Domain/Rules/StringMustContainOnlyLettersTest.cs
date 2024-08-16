using BuildingBlock.Core.Domain.Rules.Implementations;
using FluentAssertions;

namespace Tests.Core.Domain.Rules;

public class StringMustContainOnlyLettersTest
{
    public class ShouldReturnTrue
    {
        [Fact]
        public void WhenStringContainsWhiteSpace()
        {
            // Arrange
            var stringMustContainOnlyLetters = new StringMustContainOnlyLetters("String with white space");

            // Act
            var result = stringMustContainOnlyLetters.IsBroken();

            // Assert
            result.Should().BeTrue();
            stringMustContainOnlyLetters.Message.Should().Be("String must only contain only letters");
        }

        [Fact]
        public void WhenStringContainsNumber()
        {
            // Arrange
            var stringMustContainOnlyLetters = new StringMustContainOnlyLetters("String with number 1");

            // Act
            var result = stringMustContainOnlyLetters.IsBroken();

            // Assert
            result.Should().BeTrue();
            stringMustContainOnlyLetters.Message.Should().Be("String must only contain only letters");
        }

        [Fact]
        public void WhenStringContainsSpecialCharacter()
        {
            // Arrange
            var stringMustContainOnlyLetters = new StringMustContainOnlyLetters("String with special character !");

            // Act
            var result = stringMustContainOnlyLetters.IsBroken();

            // Assert
            result.Should().BeTrue();
            stringMustContainOnlyLetters.Message.Should().Be("String must only contain only letters");
        }
    }

    public class ShouldReturnFalse
    {
        [Fact]
        public void WhenStringContainsOnlyLetters()
        {
            // Arrange
            var stringMustContainOnlyLetters = new StringMustContainOnlyLetters("StringWithOnlyLetters");

            // Act
            var result = stringMustContainOnlyLetters.IsBroken();

            // Assert
            result.Should().BeFalse();
            stringMustContainOnlyLetters.Message.Should().BeNull();
        }
    }
}