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
            var stringMustContainOnlyLetters = new StringMustContainOnlyLetters("String with white space", "key");

            // Act
            var result = stringMustContainOnlyLetters.IsBroken();

            // Assert
            result.Should().BeTrue();
            stringMustContainOnlyLetters.Message.Should().Be("key must contain only letters");
        }

        [Fact]
        public void WhenStringContainsNumber()
        {
            // Arrange
            var stringMustContainOnlyLetters = new StringMustContainOnlyLetters("String with number 1", "key");

            // Act
            var result = stringMustContainOnlyLetters.IsBroken();

            // Assert
            result.Should().BeTrue();
            stringMustContainOnlyLetters.Message.Should().Be("key must contain only letters");
        }

        [Fact]
        public void WhenStringContainsSpecialCharacter()
        {
            // Arrange
            var stringMustContainOnlyLetters =
                new StringMustContainOnlyLetters("String with special character !", "key");

            // Act
            var result = stringMustContainOnlyLetters.IsBroken();

            // Assert
            result.Should().BeTrue();
            stringMustContainOnlyLetters.Message.Should().Be("key must contain only letters");
        }
    }

    public class ShouldReturnFalse
    {
        [Fact]
        public void WhenStringContainsOnlyLetters()
        {
            // Arrange
            var stringMustContainOnlyLetters = new StringMustContainOnlyLetters("StringWithOnlyLetters", "key");

            // Act
            var result = stringMustContainOnlyLetters.IsBroken();

            // Assert
            result.Should().BeFalse();
            stringMustContainOnlyLetters.Message.Should().BeNull();
        }
    }
}